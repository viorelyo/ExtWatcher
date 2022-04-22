using AnalyzeServiceGrpc.Metrics;

namespace AnalyzeServiceGrpc.Services
{
    public class HttpRequestMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly Action<RequestProfilerModel> _requestResponseHandler;

        private readonly IMetricsRegistry _metricsRegistry;

        public HttpRequestMiddleware(RequestDelegate next, /*Action<RequestProfilerModel> requestResponseHandler,*/ IMetricsRegistry metricRegistry)
        {
            _next = next;
            //_requestResponseHandler = requestResponseHandler;

            _metricsRegistry = metricRegistry;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Excluding Prometheus metrics middleware from statistics
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("metrics"))
            {
                await _next(context);
                return;
            }

            var model = new RequestProfilerModel
            {
                RequestTime = DateTime.Now,
                Context = context,
                Request = await FormatRequest(context)
            };
            
            using (_metricsRegistry.HistogramGrpcCallsDuration())
            {
                await _next(context);

                model.ResponseTime = DateTime.Now;
            }

            //_requestResponseHandler(model);
        }

        private async Task<string> FormatRequest(HttpContext context)
        {
            HttpRequest request = context.Request;

            return $"Http Request Information: {Environment.NewLine}" +
                        $"Schema: {request.Scheme} {Environment.NewLine}" +
                        $"Method: {request.Method} {Environment.NewLine}" +
                        $"Host: {request.Host} {Environment.NewLine}" +
                        $"Path: {request.Path} {Environment.NewLine}";
        }

        public class RequestProfilerModel
        {
            public DateTime RequestTime { get; set; }
            public HttpContext Context { get; set; }
            public string Request { get; set; }
            public DateTime ResponseTime { get; set; }
        }
    }
}
