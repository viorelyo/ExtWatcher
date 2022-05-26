using AnalyzeServiceGrpc.Models;
using AnalyzeServiceGrpc.Services;
using AnalyzeServiceRest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AnalyzeServiceRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyzerController : ControllerBase
    {
        private readonly ILogger<AnalyzerController> _logger;
        private readonly IConfiguration _config;

        private readonly IAnalysisFileService _analysisFileService;

        private readonly string _uploadDirPath;

        public AnalyzerController(ILogger<AnalyzerController> logger, IConfiguration config, IAnalysisFileService analysisFileService)
        {
            _logger = logger;
            _config = config;

            _analysisFileService = analysisFileService;

            _uploadDirPath = _config["UploadDirPath"];
        }

        [HttpGet("analyzeResult")]
        // Alternative: DTO: [FromQuery] GetPersonQueryObject request
        public async Task<ActionResult<AnalyzeFileResponse>> GetAnalyzeResultByFileMetadata(string fileHash)
        {
            var analysisResult = await _analysisFileService.GetAnalysisFileByHashAsync(fileHash);
            if (analysisResult == null)
            {
                return NotFound();
            }

            return new AnalyzeFileResponse { IsMalicious = analysisResult.IsMalicious };
        }

        [DisableRequestSizeLimit]
        [HttpPost("submitFile")]
        public async Task<ActionResult<AnalyzeFileResponse>> UploadFile(IFormFile file)
        {
            var tmpId = Path.GetRandomFileName();
            var tmpUploadPath = Path.Combine(_uploadDirPath, tmpId);
            Directory.CreateDirectory(tmpUploadPath);

            var filePath = Path.Combine(tmpUploadPath, "data.bin");

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            // TODO analyze file here

            using var md5 = MD5.Create();
            stream.Position = 0;
            var fileHash = BitConverter.ToString(await md5.ComputeHashAsync(stream)).Replace("-", "");

            bool isMalicious = true;
            AnalyzeFileResponse analyzeFileResponse = new AnalyzeFileResponse { IsMalicious = isMalicious };

            await _analysisFileService.AddAnalysisFileAsync(
                new AnalysisFile
                {
                    Hash = fileHash,
                    Name = file.FileName,
                    Size = (ulong)file.Length,
                    IsMalicious = isMalicious,
                    InsertTime = DateTime.Now,

                    // TODO rest of fields
                });

            return analyzeFileResponse;
        }
    }
}