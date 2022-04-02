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

        public async Task<AnalysisFile> GetAnalysisFileByHashAsync(string hash)
        {
            var res = await _elasticClient.GetAsync(new DocumentPath<AnalysisFile>(hash));
            return res.Source;
        }

        public async Task<IEnumerable<AnalysisFile>> GetAnalysisFilesAsync()
        {
            var res = await _elasticClient.SearchAsync<AnalysisFile>(f => f.From(0).Size(1000).MatchAll()); // TODO the hell?
            return res.Documents;
        }
    }
}
