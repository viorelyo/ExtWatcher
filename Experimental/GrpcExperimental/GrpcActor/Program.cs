using Google.Protobuf;
using Grpc.Net.Client;
using GrpcActor;
using System.Net;

var handler = new Http3Handler(new HttpClientHandler());

var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions() { HttpHandler = handler });
var client = new Analyzer.AnalyzerClient(channel);

Console.WriteLine("Starting uploading");
var call = client.UploadFile();

Console.WriteLine("Uploding file metadata");
await call.RequestStream.WriteAsync(
    new AnalyzeFileRequest
    {
        Metadata = new AnalyzeFileMetadata
        {
            FileName = "test.dat"
        }
    });

var buffer = new byte[1024 * 32];
await using var readStream = File.OpenRead("test.dat");

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