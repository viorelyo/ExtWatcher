using AnalyzeServiceGrpc.Models;
using Nest;

namespace AnalyzeServiceGrpc.Services
{
    public class AnalysisFileElasticSearchService : IAnalysisFileService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;

        public AnalysisFileElasticSearchService(IElasticClient elasticClient, ILogger<AnalysisFileElasticSearchService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task AddAnalysisFileAsync(AnalysisFile analysisFile)
        {
            await _elasticClient.IndexDocumentAsync(analysisFile);
        }

        public async Task<AnalysisFile?> GetAnalysisFileByHashAsync(string hash)
        {
            var res = await _elasticClient.SearchAsync<AnalysisFile>(
                s => s.Query(
                    q => q.Match(
                        p => p
                        .Field("hash")
                        .Query(hash))));

            if (!res.Documents.Any())
            {
                return null;
            }

            return res.Documents.First();
        }
    }
}
