using AnalyzeServiceGrpc;
using AnalyzeServiceGrpc.Models;
using Grpc.Core;
using System.Security.Cryptography;

namespace AnalyzeServiceGrpc.Services
{
    public class AnalyzerService : Analyzer.AnalyzerBase
    {
        private readonly ILogger<AnalyzerService> _logger;
        private readonly IConfiguration _config;

        private readonly IAnalysisFileService _analysisFileService;

        private readonly string _uploadDirPath;

        public AnalyzerService(ILogger<AnalyzerService> logger, IConfiguration config, IAnalysisFileService analysisFileService)
        {
            _logger = logger;
            _config = config;

            _analysisFileService = analysisFileService;

            _uploadDirPath = _config["UploadDirPath"];
        }

        public override async Task<AnalyzeFileResponse> GetAnalyzeResultByFileMetadata(AnalyzeFileMetadata request, ServerCallContext context)
        {
            var analysisResult = await _analysisFileService.GetAnalysisFileByHashAsync(request.Hash);
            if (analysisResult == null)
            {
                throw new RpcException(new Status(StatusCode.Unavailable, "AnalyzeResult not found for given hash"));
            }

            return new AnalyzeFileResponse { IsMalicious = analysisResult.IsMalicious };
        }

        public override async Task<AnalyzeFileResponse> UploadFile(IAsyncStreamReader<AnalyzeFileRequest> requestStream, ServerCallContext context)
        {
            var tmpId = Path.GetRandomFileName();
            var tmpUploadPath = Path.Combine(_uploadDirPath, tmpId);
            Directory.CreateDirectory(tmpUploadPath);

            await using var writeStream = File.Create(Path.Combine(tmpUploadPath, "data.bin"));

            AnalyzeFileMetadata? fileMetadata = null;

            await foreach (var msg in requestStream.ReadAllAsync())
            {
                if (msg.Metadata != null)
                {
                    // TODO
                    fileMetadata = msg.Metadata;
                    //await File.WriteAllTextAsync(Path.Combine(tmpUploadPath, "metadata.json"), fileMetadata.ToString());
                }

                if (msg.Data != null)
                {
                    await writeStream.WriteAsync(msg.Data.Memory);
                }
            }

            // TODO analyze file here

            using var md5 = MD5.Create();
            writeStream.Position = 0;
            var fileHash = BitConverter.ToString(await md5.ComputeHashAsync(writeStream)).Replace("-", "");

            bool isMalicious = true;
            AnalyzeFileResponse analyzeFileResponse = new AnalyzeFileResponse { IsMalicious = isMalicious };

            if (fileMetadata == null)
            {
                _logger.LogError("Got null fileMetadata :O");
                return analyzeFileResponse;
            }

            await _analysisFileService.AddAnalysisFileAsync(
                new AnalysisFile
                {
                    Hash = fileHash,
                    Name = fileMetadata.FileName,
                    Size = fileMetadata.Size,
                    IsMalicious = isMalicious,
                    InsertTime = DateTime.Now,
                    
                    // TODO rest of fields
                });

            return analyzeFileResponse;
        }
    }
}