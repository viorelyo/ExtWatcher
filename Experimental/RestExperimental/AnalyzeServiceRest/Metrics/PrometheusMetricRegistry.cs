using Prometheus;

namespace AnalyzeServiceGrpc.Metrics
{
    public class PrometheusMetricsRegistry : IMetricsRegistry
    {
        private static readonly Histogram HttpCallsDuration = Prometheus.Metrics.CreateHistogram("http_call_duration_milliseconds", "Histogram of http calls processing durations.",
            new HistogramConfiguration
            {
                Buckets = new[] { 1.0, 2.0, 4.0, 8.0, 16.0, 32.0, 64.0, 128.0, 256.0, 512.0, 1024.0, 2048.0, 4096.0, 8192.0, 16364.0, 32728.0, 65456.0, 130912.0 },
                LabelNames = new[] { "method", "endpoint" }
            });

        public IMetricTimer HistogramHttpCallsDuration(string method, string endpoint) => new PrometheusTimer(HttpCallsDuration.WithLabels(method, endpoint));
    }
}
