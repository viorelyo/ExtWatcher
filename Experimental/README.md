# GrpcExperimental
Migrating AnalyzeServer to microservice architecture using gPRC framework over HTTP/3 (QUIC). Project with experimental purpose.

## Overview
* **AnalyzeServiceGrpc** - grpc microservice
* **GrpcActor** - client for minimal communication testing

## Links
- [HTTP/3 configuration on Windows 11](./win-http3-config.md)


### AnalyzeServiceGrpc
* First application start, installs a self-signed certificate for TLS coomunication into Trusted-Root-Certificate-Store


## TODO
- [ ] benchmarking framework (REST vs gRPC over HTTP/3)