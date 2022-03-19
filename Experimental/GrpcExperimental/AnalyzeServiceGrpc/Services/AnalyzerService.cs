using AnalyzeServiceGrpc;
using Grpc.Core;

namespace AnalyzeServiceGrpc.Services
{
    public class AnalyzerService : Analyzer.AnalyzerBase
    {
        private readonly ILogger<AnalyzerService> _logger;
        private readonly IConfiguration _config;

        private readonly string _uploadDirPath;

        public AnalyzerService(ILogger<AnalyzerService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            _uploadDirPath = _config["UploadDirPath"];
        }

        public override async Task<AnalyzeFileResponse> UploadFile(IAsyncStreamReader<AnalyzeFileRequest> requestStream, ServerCallContext context)
        {
            var tmpId = Path.GetRandomFileName();
            var tmpUploadPath = Path.Combine(_uploadDirPath, tmpId);
            Directory.CreateDirectory(tmpUploadPath);

            await using var writeStream = File.Create(Path.Combine(tmpUploadPath, "data.bin"));

            await foreach (var msg in requestStream.ReadAllAsync())
            {
                if (msg.Metadata != null)
                {
                    _logger.LogWarning("writing metadata");
                    await File.WriteAllTextAsync(Path.Combine(tmpUploadPath, "metadata.json"), msg.Metadata.ToString());
                    // TODO
                }

                if (msg.Data != null)
                {
                    await writeStream.WriteAsync(msg.Data.Memory);
                }
            }

            return new AnalyzeFileResponse { IsMalicious = false };
        }
    }
}