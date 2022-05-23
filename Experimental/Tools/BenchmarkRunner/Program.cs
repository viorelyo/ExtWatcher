﻿using System.Collections.Concurrent;
using System.Diagnostics;

if (args.Length == 0)
{
    ShowHelp();
    return;
}

string processPath = args[0];
int instances = Int32.Parse(args[1]);
int fileSizeMB = 1;
bool createTestFile = args.Length > 2;

if (createTestFile)
{
    fileSizeMB = Int32.Parse(args[2]);
}

Console.WriteLine($"Launching [{instances}] instances of: [{processPath}]");

var resultsBag = new ConcurrentBag<double>();

int timedOut = 0;
int failed = 0;

Parallel.For(0, instances, new ParallelOptions { MaxDegreeOfParallelism = int.MaxValue }, index => 
{
    string testFileName = $"test.dat";

    try
    {
        if (createTestFile)
        {
            testFileName = $"test-{index}.dat";
            CreateDummyFile(testFileName, fileSizeMB * 1024 * 1024);
        }

        Process process = new Process();
        process.StartInfo.FileName = processPath;
        process.StartInfo.Arguments = testFileName;
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
    }
    catch { }
    finally
    {
        if (File.Exists(testFileName))
        {
            File.Delete(testFileName);
        }
    }
});

if (resultsBag.Count == 0)
{
    return;
}

double totalTime = 0;
foreach (var result in resultsBag)
{
    totalTime += result;
}

var averageTime = totalTime / resultsBag.Count;

Console.WriteLine($"Timed out: [{timedOut}]");
Console.WriteLine($"Failed: [{failed}]");
Console.WriteLine($"Average time: [{averageTime}]");
Console.WriteLine($"Best time: [{resultsBag.Min()}]");
Console.WriteLine($"Worst time: [{resultsBag.Max()}]");


void CreateDummyFile(string fileName, long length)
{
    byte[] data = new byte[length];
    Random rng = new Random();
    rng.NextBytes(data);
    File.WriteAllBytes(fileName, data);
}

void ShowHelp()
{
    Console.WriteLine("\t1\t- Process Path");
    Console.WriteLine("\t2\t- Instance number");
    Console.WriteLine("\t3\t- (optional) Size in MB for test file / otherwise test.dat is used");
}