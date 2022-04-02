using AnalyzeServiceGrpc;
using AnalyzeServiceGrpc.Models;
using Grpc.Core;

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

        public override async Task<AnalyzeFileResponse> UploadFile(IAsyncStreamReader<AnalyzeFileRequest> requestStream, ServerCallContext context)
        {
            var tmpId = Path.GetRandomFileName();
            var tmpUploadPath = Path.Combine(_uploadDirPath, tmpId);
            Directory.CreateDirectory(tmpUploadPath);

            await using var writeStream = File.Create(Path.Combine(tmpUploadPath, "data.bin"));

            string fileNameMetadata = "";

            await foreach (var msg in requestStream.ReadAllAsync())
            {
                if (msg.Metadata != null)
                {
                    _logger.LogWarning("writing metadata");

                    fileNameMetadata = msg.Metadata.ToString();
                    await File.WriteAllTextAsync(Path.Combine(tmpUploadPath, "metadata.json"), fileNameMetadata);
                    // TODO
                }

                if (msg.Data != null)
                {
                    await writeStream.WriteAsync(msg.Data.Memory);
                }
            }

            await _analysisFileService.AddAnalysisFileAsync(
                new AnalysisFile
                {
                    Hash = "random01",
                    Name = fileNameMetadata,
                    InsertTime = DateTime.Now,
                    // TODO rest of fields
                });

            return new AnalyzeFileResponse { IsMalicious = false };
        }
    }
}