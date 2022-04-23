using AnalyzeServiceGrpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using Prometheus;
using Elasticsearch.Net;
using Nest;
using static AnalyzeServiceGrpc.Services.HttpRequestMiddleware;
using AnalyzeServiceGrpc.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    // Add grpc middleware
    options.Interceptors.Add<ServiceInterceptor>();
});

// Add elasticsearch service
builder.Services.AddSingleton<IAnalysisFileService, AnalysisFileElasticSearchService>();
builder.Services.AddElasticSearch(builder.Configuration);

// Add custom prometheus metrics
builder.Services.AddTransient<IMetricsRegistry, PrometheusMetricsRegistry>();

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        //listenOptions.Protocols = HttpProtocols.Http3;
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
        listenOptions.UseHttps();
    });
});

var app = builder.Build();

// Add asp.net (http) middleware
app.UseMiddleware<HttpRequestMiddleware>();

//Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
//{
//    Console.WriteLine(requestProfilerModel.Request);
//};
//app.UseMiddleware<RequestResponseLoggingMiddleware>(requestResponseHandler);


// Configure prometheus metrics
app.UseMetricServer();
app.UseHttpMetrics();
app.UseGrpcMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGrpcService<AnalyzerService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client");

app.Run();