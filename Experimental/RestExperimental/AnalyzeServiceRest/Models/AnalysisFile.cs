namespace AnalyzeServiceGrpc.Models
{
    public class AnalysisFile
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ulong Size { get; set; }
        public DateTime InsertTime { get; set; }
        public ulong AnalysisTime { get; set; }
        public bool IsMalicious { get; set; }
    }
}
