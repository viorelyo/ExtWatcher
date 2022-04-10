using RestActor.Core;

string BASE_URL = "https://localhost:7117/api/Analyzer/";

var client = new HttpClient();
client.BaseAddress = new Uri(BASE_URL);

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