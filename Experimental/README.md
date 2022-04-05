# GrpcExperimental
Migrating AnalyzeServer to microservice architecture using gRPC framework over HTTP/3 (QUIC). Project with experimental purpose.

## Overview
* **AnalyzeServiceGrpc** - grpc microservice
* **GrpcActor** - client for minimal communication testing

## Dependencies
* **Prometheus 2.34.0**
* **Grafana 8.4.4.0**
* **Elasticsearch 7.13.0**

## Links
#### gRPC over QUIC
- [HTTP/3 configuration on Windows 11](./win-http3-config.md)
- [gRPC performance tips](https://docs.microsoft.com/en-us/aspnet/core/grpc/performance?view=aspnetcore-6.0)
- [gRPC best practices](https://github.com/grpc/grpc-dotnet/tree/master/examples#uploader)
- [error handling](https://github.com/avinassh/grpc-errors/blob/master/csharp/Hello/HelloServer/Program.cs)
- [file uploading](https://www.vinsguru.com/grpc-file-upload-client-streaming/)
- [grpc over quic - research miniproj](https://github.com/jswilley/QUIC)
- [ghz / hey / ab - benchmarking tools](https://dev.to/hiisi13/easy-ways-to-load-test-a-grpc-service-1dm3)
- [grpc in asp.net and grafana](https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/application-performance-management)
- [Upload/Download performance with gRPC](https://github.com/grpc/grpc-dotnet/issues/1186)

#### Grafana & Prometheus
- [example for custom prometheus metrics asp.net grpc](https://github.com/Expense-Tracker-Team/portfolio-manager)
- [dotnet-monitor](https://devblogs.microsoft.com/dotnet/introducing-dotnet-monitor/)
- [configure grafana, prometheus](https://dotnetos.org/blog/2021-11-22-dotnet-monitor-grafana/)
- [asp.net core with prometheus](https://www.olivercoding.com/2018-07-22-prometheus-dotnetcore/)
- [collect metrics](https://docs.microsoft.com/en-us/dotnet/core/diagnostics/metrics-collection)
- [explaination of prometheus.metric types](https://aevitas.medium.com/expose-asp-net-core-metrics-with-prometheus-15e3356415f4)
- [asp.net with grafana](https://sachabarbs.wordpress.com/2020/02/07/setting-up-prometheus-and-grafana-monitoring/)
- [full metrics stack configuration in docker](https://medium.com/@niteshsinghal85/track-api-usage-with-prometheus-and-grafana-in-asp-net-core-cfdf03346b4)
- [.net 6 metris and tracing opentelemetry](https://www.meziantou.net/monitoring-a-dotnet-application-using-opentelemetry.htm)
- [grafana cloud through grafana agent](https://grafana.com/blog/2021/02/11/instrumenting-a-.net-web-api-using-opentelemetry-tempo-and-grafana-cloud/)

#### ElasticSearch
- [Basic commands NEST](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest-getting-started.html)
- [CRUD example ElasticSearch ASP.NET](https://vscode.dev/github/msfullstackdev/aspnetcore-elasticsearch-crud-azure/blob/master/CS.Repository/GenericRepository.cs)
- [Docummentation CRUD NEST](https://social.technet.microsoft.com/wiki/contents/articles/35095.crud-operation-in-elasticsearch-using-c-and-nest.aspx)


### AnalyzeServiceGrpc
* First application start, installs a self-signed certificate for TLS coomunication into Trusted-Root-Certificate-Store


### Utils
- `fsutil file createnew test.dat 1048576`


## TODO
- [ ] Test if FileStream class reads entire file in memory, or can fill memory with chunks
- [ ] Get rid of warning - overriden urls 
	* [link](https://stackoverflow.com/questions/58090842/configurekestrel-conflict-with-appsettings)
	* [link](https://stackoverflow.com/questions/51738893/removing-kestrel-binding-warning)
- [ ] benchmarking framework (REST vs gRPC over HTTP/3)
	* [link](https://www.grpc.io/docs/guides/benchmarking/)
- [ ] Exception handling
- [ ] Custom metrics for prometheus
- [ ] dotnet-monitor
- [ ] IDEA: first check by filehash, then upload pdf