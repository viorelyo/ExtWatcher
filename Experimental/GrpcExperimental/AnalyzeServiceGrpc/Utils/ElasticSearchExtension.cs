using AnalyzeServiceGrpc.Models;
using Nest;

namespace AnalyzeServiceGrpc.Services
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration config)
        {
            var url = config["ElasticSearch:url"];
            var defaultIndex = config["ElasticSearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            settings.EnableHttpCompression();

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            //CreateIndex(client, defaultIndex);
        }

        // TODO check if required
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<AnalysisFile>(x => x.AutoMap())
                );
        }
    }
}
