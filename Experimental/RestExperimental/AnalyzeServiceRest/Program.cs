using AnalyzeServiceGrpc.Metrics;
using AnalyzeServiceGrpc.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Add elasticsearch service
builder.Services.AddSingleton<IAnalysisFileService, AnalysisFileElasticSearchService>();
builder.Services.AddElasticSearch(builder.Configuration);

// Add custom prometheus metrics
builder.Services.AddTransient<IMetricsRegistry, PrometheusMetricsRegistry>();

var app = builder.Build();

// Add asp.net (http) middleware
app.UseMiddleware<HttpRequestMiddleware>();

// Configure prometheus metrics
app.UseMetricServer();
app.UseHttpMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Configure swagger
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
