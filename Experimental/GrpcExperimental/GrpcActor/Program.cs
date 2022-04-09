using Grpc.Net.Client;
using GrpcActor;
using GrpcActor.Core;
using GrpcActor.Extensions;

var handler = new Http3Handler(new HttpClientHandler());

var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions() { HttpHandler = handler });
var client = new Analyzer.AnalyzerClient(channel);

var analyzeCore = new RemoteAnalyzeCore(client);

try
{
    var isMalicious = await analyzeCore.ScanFileAsync("test.dat");
    Console.WriteLine($"ScanResult: {isMalicious}");
}
catch (AnalysisException ex)
{
    Console.WriteLine(ex);
}

Console.ReadKey();