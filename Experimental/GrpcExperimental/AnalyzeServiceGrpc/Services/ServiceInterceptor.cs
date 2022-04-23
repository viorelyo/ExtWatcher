using AnalyzeServiceGrpc.Metrics;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace AnalyzeServiceGrpc.Services
{
    public class ServiceInterceptor : Interceptor
    {
        private readonly IMetricsRegistry _metricsRegistry;

        public ServiceInterceptor(IMetricsRegistry metricsRegistry)
        {
            _metricsRegistry = metricsRegistry;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request, 
            ServerCallContext context, 
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            using (_metricsRegistry.HistogramGrpcCallsDuration(context.Method))
            {
                return await continuation(request, context);
            }
        }

        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream, 
            ServerCallContext context, 
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            using (_metricsRegistry.HistogramGrpcCallsDuration(context.Method))
            {
                return await base.ClientStreamingServerHandler(requestStream, context, continuation);
            }
        }
    }
}
