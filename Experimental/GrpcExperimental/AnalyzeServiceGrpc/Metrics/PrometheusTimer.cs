using Prometheus;
using System.Diagnostics;

namespace AnalyzeServiceGrpc.Metrics
{
    public class PrometheusTimer : IMetricTimer
    {
        private readonly Stopwatch stopwatch = Stopwatch.StartNew();
        private readonly Action<double> observeDurationAction;

        public PrometheusTimer(IObserver observer) => this.observeDurationAction = duration => observer.Observe(duration);

        public PrometheusTimer(IGauge gauge) => this.observeDurationAction = duration => gauge.Set(duration);

        public PrometheusTimer(ICounter counter) => this.observeDurationAction = duration => counter.Inc(duration);

        public TimeSpan ObserveDuration()
        {
            TimeSpan duration = this.stopwatch.Elapsed;
            this.observeDurationAction.Invoke(duration.TotalMilliseconds);

            return duration;
        }

        public void Dispose() => this.ObserveDuration();
    }
}
