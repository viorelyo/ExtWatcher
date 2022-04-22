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

        private static readonly Histogram GrpcCallsDuration = Prometheus.Metrics.CreateHistogram("grpc_call_duration_milliseconds", "Histogram of gRPC calls processing durations.", 
            new HistogramConfiguration
            { 
                Buckets = new[] { 5.0, 10.0, 25.0, 50.0, 100.0, 200.0, 500.0, 800.0, 1000.0, 1200.0, 1400.0, 1600.0, 1800.0, 2000.0 }
            });

        //public void CountGrpcCalls(string method, string statusCode) => GrpcCallsCount.WithLabels(method, statusCode).Inc();

        //public void CountSuccessGrpcCalls(string method) => GrpcSuccessCallsCount.WithLabels(method).Inc();

        //public void CountFailedGrpcCalls(string method) => GrpcFailedCallsCount.WithLabels(method).Inc();

        public IMetricTimer HistogramGrpcCallsDuration() => new PrometheusTimer(GrpcCallsDuration);
    }
}
