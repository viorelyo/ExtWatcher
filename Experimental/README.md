# GrpcExperimental
Migrating AnalyzeServer to microservice architecture using gRPC framework over HTTP/3 (QUIC). Project with experimental purpose.

## Overview
* **AnalyzeServiceGrpc** - grpc microservice
* **GrpcActor** - client for minimal communication testing

## Links
- [HTTP/3 configuration on Windows 11](./win-http3-config.md)
- [gRPC performance tips](https://docs.microsoft.com/en-us/aspnet/core/grpc/performance?view=aspnetcore-6.0)
- [gRPC best practices](https://github.com/grpc/grpc-dotnet/tree/master/examples#uploader)
- [error handling](https://github.com/avinassh/grpc-errors/blob/master/csharp/Hello/HelloServer/Program.cs)
- [file uploading](https://www.vinsguru.com/grpc-file-upload-client-streaming/)


### AnalyzeServiceGrpc
* First application start, installs a self-signed certificate for TLS coomunication into Trusted-Root-Certificate-Store


## TODO
- [ ] Test if FileStream class reads entire file in memory, or can fill memory with chunks
- [ ] Get rid of warning - overriden urls 
	* [link](https://stackoverflow.com/questions/58090842/configurekestrel-conflict-with-appsettings)
	* [link](https://stackoverflow.com/questions/51738893/removing-kestrel-binding-warning)
- [ ] benchmarking framework (REST vs gRPC over HTTP/3)
	* [link](https://www.grpc.io/docs/guides/benchmarking/)
