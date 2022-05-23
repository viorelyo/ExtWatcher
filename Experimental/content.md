# Dissertation plan


## Title
- Bachelor: `Cloud Based Malicious PDF Detection Using Machine Learning`
- Master: `Computation offloading of Cloud based Malicious PDF Detection service using gRPC framework over HTTP/3. Performance evaluation of RESTful API versus gRPC microservice`

## Content
- elasticsearch storage
- metrics (prometheus) + grafana
- grpc over http/3 (quic) benchmarking 
- grpc over http/2 
- rest over http/1.1 benchmarking

### Performance testing
    * Load testing
    * Stress testing
    * Endurance testing
    * Resources utilization (CPU / Memory)
    * Custom scenarios:
        * network switching (testing advantages of QUIC protocol - fast connection reestablishment)
        * large files transfer

## Links
- [performance comparing between rest and grpc](https://stackoverflow.com/questions/44877606/is-grpchttp-2-faster-than-rest-with-http-2)
- [http history timeline](https://habr.com/ru/post/438810/)
- [ghz / hey / ab - benchmarking tools](https://dev.to/hiisi13/easy-ways-to-load-test-a-grpc-service-1dm3)
- [grpc over quic - research miniproj](https://github.com/jswilley/QUIC)
- [performance comparing between rust and grpc](https://stackoverflow.com/questions/44877606/is-grpchttp-2-faster-than-rest-with-http-2)
- [grpc in asp.net and grafana](https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/application-performance-management)
- [http history timeline](https://habr.com/ru/post/438810/)
- [grpc advantages explained](https://habr.com/ru/company/nix/blog/594391/)
- [rest migration to grpc](https://habr.com/ru/company/otus/blog/547478/)
- [asp.net grpc diagrams](https://habr.com/ru/company/piter/blog/596621/)
- [rest vs grpc](https://habr.com/ru/post/565020/)
- [grpc in .net](https://habr.com/ru/company/mindbox/blog/597183/)

- [clean code](https://gist.github.com/wojteklu/73c6914cc446146b8b533c0988cf8d29)