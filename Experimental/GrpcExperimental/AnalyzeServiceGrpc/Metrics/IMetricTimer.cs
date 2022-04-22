namespace AnalyzeServiceGrpc.Metrics
{
    public interface IMetricTimer : IDisposable
    {
        /// <summary>
        /// Observes the duration (in seconds) and returns the observed value.
        /// </summary>
        TimeSpan ObserveDuration();
    }
}
