using Google.Protobuf;
using Grpc.Core;
using System.Security.Cryptography;

namespace GrpcActor.Core
{
    public class RemoteAnalyzeCore
    {
        private readonly Analyzer.AnalyzerClient _client;
        private readonly CallOptions _callOptions;

        private string? _filePath;
        private AnalyzeFileMetadata? _analyzeFileMetadata;

        public RemoteAnalyzeCore(Analyzer.AnalyzerClient client)
        {
            _client = client;
            _callOptions = new CallOptions(deadline: DateTime.UtcNow.AddSeconds(30));
        }

        public async Task<bool> ScanFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new AnalysisException("Invalid input");
            }

            if (!File.Exists(filePath))
            {
                throw new AnalysisException("Invalid file path");
            }

            _filePath = filePath;
            try
            {
                PrepareMetadata();
            }
            catch (Exception ex)
            {
                throw new AnalysisException("Could not prepare metadata", ex);
            }

            try
            {
                return CheckScanCache();
            }
            catch (AnalysisException)
            {
                try
                {
                    return await SubmitFileAndAnalyzeAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new AnalysisException("Could not scan the file", ex);
                }
            }
        }

        private void PrepareMetadata()
        {
            using var md5 = MD5.Create();

            using var readStream = File.OpenRead(_filePath);
            var fileHash = BitConverter.ToString(md5.ComputeHash(readStream)).Replace("-", "");

            _analyzeFileMetadata = new AnalyzeFileMetadata
            {
                Hash = fileHash,
                FileName = Path.GetFileName(_filePath),
                Size = (ulong)readStream.Length
            };
        }

        private bool CheckScanCache()
        {
            try
            {
                var analyzeResult = _client.GetAnalyzeResultByFileMetadata(_analyzeFileMetadata, _callOptions);
                //Console.WriteLine(analyzeResult.ToString());
                return analyzeResult.IsMalicious;
            }
            catch (RpcException ex)
            {
                //Console.WriteLine(ex.Status);
                throw new AnalysisException("Could not obtain an result");
            }
        }

        private async Task<bool> SubmitFileAndAnalyzeAsync()
        {
            //Console.WriteLine("Starting uploading");

            await using var readStream = File.OpenRead(_filePath);

            //Console.WriteLine("Uploding file metadata");

            var call = _client.UploadFile(_callOptions);

            await call.RequestStream.WriteAsync(
                new AnalyzeFileRequest
                {
                    Metadata = _analyzeFileMetadata
                });

            var buffer = new byte[1024 * 32];

            while (true)
            {
                var count = await readStream.ReadAsync(buffer);
                if (count == 0)
                {
                    break;
                }

                //Console.WriteLine($"Uploading chunk: {count}");
                await call.RequestStream.WriteAsync(
                    new AnalyzeFileRequest
                    {
                        Data = UnsafeByteOperations.UnsafeWrap(buffer.AsMemory(0, count))
                    });
            }

            await call.RequestStream.CompleteAsync();
            //Console.WriteLine("Request completed");

            var response = await call;
            return response.IsMalicious;
        }
    }
}
