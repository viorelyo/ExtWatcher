namespace AnalyzeServiceGrpc.Models
{
    public class AnalysisFile
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public UInt64 Size { get; set; }
        public string AnalysisResult { get; set; }
        public DateTime InsertTime { get; set; }
        public UInt64 AnalysisTime { get; set; }
    }
}
