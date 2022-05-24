# Performance Testing Results

## Hardware
CPU: 2 processors, 1 core per processor (2 cores) (Intel(R) Core(TM) i7-9850H CPU @ 2.60GHz, 2592 Mhz, 1 Core(s), 1 Logical Processor(s))
RAM: 2 GB
Windows Version: 10.0.22000 Build 22000 (Microsoft Windows 11 Enterprise Evaluation)

## Resource utilization
- elasticSearch (OpenJDK)- 1320 MB (CPU: 0.5% ->  25%)
- prometheus (CPU: 1.5%)
- grafana-server : (CPU: 0.8%)

## Setup
- Updates stopped, antimalware stopped
- Clean ElasticSearch DB
- Prometheus scraping interval: 2s

## Metrics:
- `rate(process_cpu_seconds_total{job="restexperimental"}[1m])`
  [PromQL Details](https://mopitz.medium.com/understanding-prometheus-rate-function-15e93e44ae61)

- `process_num_threads{}`

- `process_private_memory_bytes{}`

- `sum(increase(http_call_duration_milliseconds_bucket{endpoint="/api/Analyzer/analyzeResult"}[$__interval])) by (le)`
  [PromQL Explaination](https://medium.com/@hedgic/monitoring-and-load-testing-asp-net-core-application-94256c9d0be7)

## 1. Rest
1. 100 instances, 1 MB binary file, new file
```
Timed out: [0]
Failed: [0]
Average time: [3842.5047330000007]
Best time: [2279.0776]
Worst time: [5731.0215]

Average nr. of threads: [24.264150943396228]
Max nr. of threads: [29]
```

2. 