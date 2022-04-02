using AnalyzeServiceGrpc.Models;

namespace AnalyzeServiceGrpc.Services
{
    public interface IAnalysisFileService
    {
        Task<AnalysisFile> GetAnalysisFileByHashAsync(string hash);
        Task<IEnumerable<AnalysisFile>> GetAnalysisFilesAsync();
        Task AddAnalysisFileAsync(AnalysisFile analysisFile);

        // TODO add rest of the methods
    }
}
