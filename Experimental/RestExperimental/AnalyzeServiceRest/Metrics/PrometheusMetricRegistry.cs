using Prometheus;

namespace AnalyzeServiceGrpc.Metrics
{
    public class PrometheusMetricsRegistry : IMetricsRegistry
    {
        private static readonly Histogram HttpCallsDuration = Prometheus.Metrics.CreateHistogram("http_call_duration_milliseconds", "Histogram of http calls processing durations.",
            new HistogramConfiguration
            {
                Buckets = new[] { 5.0, 10.0, 25.0, 50.0, 100.0, 200.0, 500.0, 800.0, 1000.0, 1200.0, 1400.0, 1600.0, 1800.0, 2000.0, 2300.0, 2600.0, 2900.0, 3100.0, 3400.0, 3800.0, 4200.0, 4600.0, 5000.0, 5500.0, 5800.0, 6200.0 },
                LabelNames = new[] { "method", "endpoint" }
            });

        public IMetricTimer HistogramHttpCallsDuration(string method, string endpoint) => new PrometheusTimer(HttpCallsDuration.WithLabels(method, endpoint));
    }
}
