namespace AnalyzeServiceGrpc.Metrics
{
    public interface IMetricsRegistry
    {
        IMetricTimer HistogramHttpCallsDuration(string method, string endpoint);
    }
}
