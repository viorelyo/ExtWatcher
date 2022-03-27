using AnalyzeServiceGrpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
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