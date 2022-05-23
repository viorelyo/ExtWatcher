using RestActor.Core;

string BASE_URL = "https://localhost:7117/api/Analyzer/";

using (var client = new HttpClient())
{
    client.BaseAddress = new Uri(BASE_URL);

    var analyzeCore = new RemoteAnalyzeCore(client);

    try
    {
        var isMalicious = await analyzeCore.ScanFileAsync(args[0]);
        //Console.WriteLine($"ScanResult: {isMalicious}");
        Environment.Exit(0);
    }
    catch (AnalysisException ex)
    {
        Console.WriteLine(ex);
        Environment.Exit(1);
    }
}

//Console.ReadKey();