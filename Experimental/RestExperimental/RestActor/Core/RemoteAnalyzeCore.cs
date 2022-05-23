using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RestActor.Core
{
    public class RemoteAnalyzeCore
    {
        private readonly HttpClient _client;

        private string? _filePath;

        public RemoteAnalyzeCore(HttpClient client)
        {
            _client = client;
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

        private bool CheckScanCache()
        {
            try
            {
                using var md5 = MD5.Create();

                using var readStream = File.OpenRead(_filePath);
                var fileHash = BitConverter.ToString(md5.ComputeHash(readStream)).Replace("-", "");

                var response = _client.GetAsync($"analyzeResult?fileHash={fileHash}").Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new AnalysisException("Could not obtain an result");
                }

                var rawResponse = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine($"RawResponse: {rawResponse}");

                try
                {
                    var jsonObject = JsonNode.Parse(rawResponse).AsObject();
                    bool scanResult = (bool)jsonObject["isMalicious"];
                    return scanResult;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new AnalysisException("Could not an result", ex);
            }
        }

        private async Task<bool> SubmitFileAndAnalyzeAsync()
        {
            //Console.WriteLine("Starting uploading");

            var fileContent = new ByteArrayContent(File.ReadAllBytes(_filePath));
            var multipartFormDataContent = new MultipartFormDataContent();
            multipartFormDataContent.Add(fileContent, "file", Path.GetFileName(_filePath));

            //Console.WriteLine("Uploding file");
            var response = await _client.PostAsync("submitFile", multipartFormDataContent);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AnalysisException("Could not obtain an result");
            }

            var rawResponse = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"RawResponse: {rawResponse}");

            try
            {
                var jsonObject = JsonNode.Parse(rawResponse).AsObject();
                bool scanResult = (bool)jsonObject["isMalicious"];
                return scanResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
