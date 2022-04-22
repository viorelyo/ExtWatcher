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

var app = builder.Build();

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
