using Prometheus;

namespace AnalyzeServiceGrpc.Metrics
{
    public class PrometheusMetricsRegistry : IMetricsRegistry
    {
        //private static readonly Counter GrpcCallsCount = Metrics.CreateCounter("grpc_calls_total", "Number of gRPC calls.", new CounterConfiguration
        //{
        //    LabelNames = new[] {
        //        "method",
        //        "status_code"
        //    }
        //});

        //private static readonly Counter GrpcSuccessCallsCount = Metrics.CreateCounter("grpc_success_calls_total", "Number of success gRPC calls.", new CounterConfiguration
        //{
        //    LabelNames = new[] {
        //        "method"
        //    }
        //});

        //private static readonly Counter GrpcFailedCallsCount = Metrics.CreateCounter("grpc_failed_calls_total", "Number of failed gRPC calls.", new CounterConfiguration
        //{
        //    LabelNames = new[] {
        //        "method"
        //    }
        //});


        //public void CountGrpcCalls(string method, string statusCode) => GrpcCallsCount.WithLabels(method, statusCode).Inc();

        //public void CountSuccessGrpcCalls(string method) => GrpcSuccessCallsCount.WithLabels(method).Inc();

        //public void CountFailedGrpcCalls(string method) => GrpcFailedCallsCount.WithLabels(method).Inc();

        private static readonly Histogram GrpcCallsDuration = Prometheus.Metrics.CreateHistogram("grpc_call_duration_milliseconds", "Histogram of gRPC calls processing durations.", 
            new HistogramConfiguration
            { 
                Buckets = new[] { 1.0, 2.0, 4.0, 8.0, 16.0, 32.0, 64.0, 128.0, 256.0, 512.0, 1024.0, 2048.0, 4096.0, 8192.0, 16364.0, 32728.0 },
                LabelNames = new[] { "method" }
            });

        private static readonly Histogram HttpCallsDuration = Prometheus.Metrics.CreateHistogram("http_call_duration_milliseconds", "Histogram of http calls processing durations.",
            new HistogramConfiguration
            {
                Buckets = new[] { 1.0, 2.0, 4.0, 8.0, 16.0, 32.0, 64.0, 128.0, 256.0, 512.0, 1024.0, 2048.0, 4096.0, 8192.0, 16364.0, 32728.0 },
                LabelNames = new[] { "method", "endpoint" }
            });

        public IMetricTimer HistogramGrpcCallsDuration(string method) => new PrometheusTimer(GrpcCallsDuration.WithLabels(method));

        public IMetricTimer HistogramHttpCallsDuration(string method, string endpoint) => new PrometheusTimer(HttpCallsDuration.WithLabels(method, endpoint));
    }
}
