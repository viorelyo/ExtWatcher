using System.Collections.Concurrent;
using System.Diagnostics;

void CreateDummyFile(string fileName, long length)
{
    byte[] data = new byte[length];
    Random rng = new Random();
    rng.NextBytes(data);
    File.WriteAllBytes(fileName, data);
}

string processPath = args[0];
int instances = Int32.Parse(args[1]);

Console.WriteLine($"Launching [{instances}] instances of: [{processPath}]");

var resultsBag = new ConcurrentBag<double>();

int timedOut = 0;
int failed = 0;

Parallel.For(0, instances, index => 
{
    string dummyFileName = $"test-{index}.dat";
    CreateDummyFile(dummyFileName, 1024 * 1024);   // TODO

    Process process = new Process();
    process.StartInfo.FileName = processPath;
    process.StartInfo.Arguments = dummyFileName;
    process.StartInfo.CreateNoWindow = true;
    process.Start();

    if (!process.WaitForExit(10000))
    {
        timedOut++;
        return;
    }

    if (process.ExitCode != 0)
    {
        failed++;
        return;
    }

    var runningTime = process.ExitTime - process.StartTime;
    resultsBag.Add(runningTime.TotalMilliseconds);

    if (File.Exists(dummyFileName))
    {
        File.Delete(dummyFileName);
    }
});

double totalTime = 0;
foreach (var result in resultsBag)
{
    totalTime += result;
}

var averageTime = totalTime / resultsBag.Count;

Console.WriteLine($"Timed out: [{timedOut}]");
Console.WriteLine($"Failed: [{failed}]");
Console.WriteLine($"Average time: [{averageTime}]");

Console.ReadKey();
