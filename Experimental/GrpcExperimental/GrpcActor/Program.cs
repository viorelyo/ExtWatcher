using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcActor;
using System.Net;
using System.Security.Cryptography;

var handler = new Http3Handler(new HttpClientHandler());

var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions() { HttpHandler = handler });
var client = new Analyzer.AnalyzerClient(channel);

using var md5 = MD5.Create();

await using var readStream = File.OpenRead("test.dat");
var fileHash = BitConverter.ToString(md5.ComputeHash(readStream)).Replace("-", "");

var analyzeFileMetadata = new AnalyzeFileMetadata
{
    Hash = fileHash,
    FileName = "test.dat",
    Size = (ulong)readStream.Length
};

try
{
    var analyzeResult = client.GetAnalyzeResultByFileMetadata(analyzeFileMetadata);
    Console.WriteLine(analyzeResult.ToString());
    return;
}
catch (RpcException ex)
{
    Console.WriteLine(ex.Status);
}

Console.WriteLine("Starting uploading");
readStream.Position = 0;

var call = client.UploadFile();

Console.WriteLine("Uploding file metadata");
await call.RequestStream.WriteAsync(
    new AnalyzeFileRequest
    {
        Metadata = analyzeFileMetadata
    });

var buffer = new byte[1024 * 32];

while (true)
{
    var count = await readStream.ReadAsync(buffer);
    if (count == 0)
    {
        break;
    }

    Console.WriteLine($"Uploading chunk: {count}");
    await call.RequestStream.WriteAsync(
        new AnalyzeFileRequest
        {
            Data = UnsafeByteOperations.UnsafeWrap(buffer.AsMemory(0, count))
        });
}

Console.WriteLine("Request completed");
await call.RequestStream.CompleteAsync();

var response = await call;
Console.WriteLine($"Status = {response.IsMalicious}");
Console.ReadKey();


// HTTP/3 enforcement
public class Http3Handler : DelegatingHandler
{
    public Http3Handler() { }
    public Http3Handler(HttpMessageHandler innerHandler) : base(innerHandler) { }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;

        return base.SendAsync(request, cancellationToken);
    }
}