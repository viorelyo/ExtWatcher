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
- Restarted server
- Clean ElasticSearch DB
- Clean uploads folder
- Prometheus scraping interval: 5s
- Clean prometheus resilient data (`/data`)


## Metrics:
- `rate(process_cpu_seconds_total{job="restexperimental"}[1m])`
  [PromQL Details](https://mopitz.medium.com/understanding-prometheus-rate-function-15e93e44ae61)

- `process_num_threads{}`

- `process_private_memory_bytes{}`

- `sum(increase(http_call_duration_milliseconds_bucket{endpoint="/api/Analyzer/analyzeResult"}[$__interval])) by (le)`
  [PromQL Explaination](https://medium.com/@hedgic/monitoring-and-load-testing-asp-net-core-application-94256c9d0be7)

- Find nr. of parallel requests

## 1. Rest
0. 1 instance, 1 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/t2RFfxsf7AWEuzfocCIkW4iJeGwOsSlu?orgId=1)
```
Timed out: [0]
Failed: [0]
Average time: [2472.4718]
Best time: [2472.4718]
Worst time: [2472.4718]

Average nr. of threads: [11]
Max nr. of threads: [11]
Max nr. of concurrent instances: [1]
```
- Nr of open handles: 565 -> 713 -> 697 (idle)
- CPU Utilization: 0.423% -> 1.68% -> 0.0852% (idle)
- Process memory working set: 52.5 -> 79.2 -> 72.7 (idle)
- Total nr of threads: 22 -> 29 -> 20 (idle)
- Request duration (AnalyzeResult): 256-512ms
- Request duration (SubmitFile): 64-128ms

1. 100 instances, 1 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/crmAKxPBqIuJA6wtOiiZOeg4qzLyP6Fl?orgId=1)
```
Timed out: [0]
Failed: [0]
Average time: [3670.240602999998]
Best time: [2258.4885]
Worst time: [5629.2239]

Average nr. of threads: [22.336170212765957]
Max nr. of threads: [31]
Max nr. of concurrent instances: [22]
```
- Nr of open handles: 567 -> 782 -> 730 (idle)
- CPU Utilization: 0.325% -> 6.48% -> 0.170% (idle)
- Process memory working set: 52.5 -> 95.6 -> 95.2 (idle)
- Total nr of threads: 22 -> 37 -> 20 (idle)
- Request duration (AnalyzeResult): 256-512 (4.02), 128-256 (5.11), 64-128 (1.02), 32-64 (4.09), 16-32 (7.13), 8-16 (16.35), 4-8 (45.98), 2-4 (18.40), 1-2, 0-1
- Request duration (SubmitFile): 512-1024 (5.02), 256-512 (7.16), 128-256 (16.35), 64-128 (29.64), 32-64 (29.64), 16-32 (14.31)
- HTTP Requests received: All (5.47)

2. 100 instances, 1 MB binary file, unknown file, min 50 threads - [dashboard](http://localhost:3000/dashboard/snapshot/RsGKAUb4k0BO6CY5HPCEhsowtk4A3iKb)
```
Timed out: [0]
Failed: [0]
Average time: [7156.089265000005]
Best time: [5416.3749]
Worst time: [9293.0532]

Average nr. of threads: [55.578947368421055]
Max nr. of threads: [60]
Max nr. of concurrent instances: [51]
```
- Nr of open handles: 573 -> 878 -> 698 (idle)
- CPU Utilization: 0.09% -> 7.77% -> 0.0568% (idle)
- Process memory working set: 53.8 -> 107 -> 106 (idle)
- Total nr of threads: 23 -> 32 -> 22 (idle)
- Request duration (AnalyzeResult): 512-1024 (10.25), 256-512 (16.08), 4-8 (32.75),
- Request duration (SubmitFile): 4096-8192 (15.17), 2048-4096 (24.33), 1024-2048 (25.00), 512-1024 (18.33)
- HTTP Requests received: All (9.80)

3. 1000 instances, 1 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/0b8D2fRRb0GPxdJf0ZrcdcRXjH8x70rg), [dashboard 2](http://localhost:3000/dashboard/snapshot/tsFyoNfG8o13a6unl5LdCpJu99LHKjXu)
```
Timed out: [0]
Failed: [0]
Average time: [6494.559096599999]
Best time: [2242.6382]
Worst time: [12868.0579]

Average nr. of threads: [43.750640478223744]
Max nr. of threads: [55]
Max nr. of concurrent instances: [48]
```
- Nr of open handles: 573 -> 874 -> 736 (idle)
- CPU Utilization: 0.09% -> 10.5% -> 0.0568% (idle)
- Process memory working set: 53.8 -> 155 -> 141 (idle)
- Total nr of threads: 23 -> 38 -> 22 (idle)
- Request duration (AnalyzeResult): 2-4 (367.88), 128-256 (126.99)  [10m]
- Request duration (SubmitFile): 256-512 (240.88), 512-1024 (209.64), 1024-2048 (36.28), 16-32 (136.06)  [10m]
- HTTP Requests received: All (Max - 13)  [20s]

4. 1000 instances, 1 MB binary file, unknown file, min 100 threads - [dashboard](http://localhost:3000/dashboard/snapshot/qsYSYhD7fm0r5EsCDAd25dcPzvzlWFXO), [dashboard 2](http://localhost:3000/dashboard/snapshot/qsYSYhD7fm0r5EsCDAd25dcPzvzlWFXO)
```
Timed out: [0]
Failed: [0]
Average time: [14759.474100499981]
Best time: [5482.0849]
Worst time: [28161.7709]

Average nr. of threads: [105.11703703703704]
Max nr. of threads: [111]
Max nr. of concurrent instances: [102]
```
- Nr of open handles: 573 -> 874 -> 736 (idle)
- CPU Utilization: 0.09% -> 10.5% -> 0.0568% (idle)
- Process memory working set: 53.8 -> 155 -> 141 (idle)
- Total nr of threads: 23 -> 38 -> 22 (idle)
- Request duration (AnalyzeResult): 1-2 (51.35), 2-4 (348.40), 128-256 (126.99), 4096-8192 (1.01)  [5m]
- Request duration (SubmitFile): 256-512 (160.14), 4096-8192 (68.49), 16-32 (73.52)  [5m]
- HTTP Requests received: All (Max - 13.9)  [20s]

==============================================================
0. 1 instance, 10 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/Ges8ElMLLH8V51NgrD35qOn9ZGr7YRHc)
```
Timed out: [0]
Failed: [0]
Average time: [2550.3527]
Best time: [2550.3527]
Worst time: [2550.3527]

Average nr. of threads: [12]
Max nr. of threads: [12]
Max nr. of concurrent instances: [1]
```
- Nr of open handles: 565 -> 724 -> 713 (idle)
- CPU Utilization: 0.228% -> 1.65% -> 0.0852% (idle)
- Process memory working set: 52.5 -> 79.5 -> 71.9 (idle)
- Total nr of threads: 22 -> 31 -> 22 (idle)
- Request duration (AnalyzeResult): 64-128
- Request duration (SubmitFile): 128-256

1. 100 instances, 10 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/und6sdnq6VfjBrcUpPLjEmFvx2he3I7q)
```
Timed out: [0]
Failed: [0]
Average time: [6005.020779]
Best time: [2861.9496]
Worst time: [11938.64]

Average nr. of threads: [22.293375394321767]
Max nr. of threads: [27]
Max nr. of concurrent instances: [18]
```
- Request duration (AnalyzeResult): 4-8 (45.04), 2-4 (18.44), 256-512 (3.00)
- Request duration (SubmitFile): 2048-4096 (33.77), 4096-8192 (5.12), 128-256 (2.15)
- HTTP Requests received: All (4.35)

2. 100 instances, 10 MB binary file, unknown file, min 50 threads - [dashboard](http://localhost:3000/dashboard/snapshot/7gJqFQNtY0icWgybu4h0XiJFTNTHKeRc)
```
Timed out: [0]
Failed: [0]
Average time: [18152.376238000004]
Best time: [8929.2471]
Worst time: [26585.0011]

Average nr. of threads: [56.957142857142856]
Max nr. of threads: [60]
Max nr. of concurrent instances: [51]
```
- Request duration (AnalyzeResult): 4096-8192 (5.15), 4-8 (43.29), 2-4 (16.49)
- Request duration (SubmitFile): 16.4s-32.7s (5.18), 8.2s-16.4s (42.44), 128-256 (3.11)
- HTTP Requests received: All (6.66)

3. 1000 instances, 10 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/J1U4Es7VSRdT9Q2xUA2Pybxwe3m1oB8H), [dashboard 2](http://localhost:3000/dashboard/snapshot/6E2H8opGM7aqft4A3aKz5li97bZQdfv5)
```
Timed out: [0]
Failed: [0]
Average time: [9725.942470100003]
Best time: [2849.0571]
Worst time: [18261.4604]

Average nr. of threads: [35.27206240858118]
Max nr. of threads: [44]
Max nr. of concurrent instances: [35]
```
- Request duration (AnalyzeResult): 1-2 (15.07), 2-4 (279.96), 256-512 (11.04)  [10m]
- Request duration (SubmitFile): 128-256 (28.13) 2s-4s (171.77), 8.2s-16.4s (7.03)  [10m]
- HTTP Requests received: All (Max - 6.3)  [20s]

4. 1000 instances, 10 MB binary file, unknown file, min 100 threads - 
CPU + RAM overloaded - prometheus didn't catch up gathering the metrics -> unuseful results


==============================================================
0. 1 instance, 100 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/fiY6IdGwTSZiI5YMwxX1wUteLbHvCcGP)
```
Timed out: [0]
Failed: [0]
Average time: [4159.4952]
Best time: [4159.4952]
Worst time: [4159.4952]

Average nr. of threads: [12]
Max nr. of threads: [12]
Max nr. of concurrent instances: [1]
```
- Request duration (AnalyzeResult): 32-64
- Request duration (SubmitFile): 1s-2s

1. 100 instances, 100 MB binary file, unknown file
CPU + RAM overloaded - prometheus gaps, most upload requests took more than 2m (max from bucket)

2. 50 instances, 100 MB binary file, unknown file - [dashboard](http://localhost:3000/dashboard/snapshot/M1AD62KfPh7EcHUROXAunxdQKVFetFUx), [dashboard 2](http://localhost:3000/dashboard/snapshot/dCN300l6vB3fEl4NANTRamMi42PfMA2u)
```
Timed out: [27]
Failed: [0]
Average time: [21616.339634782613]
Best time: [8723.1008]
Worst time: [38422.5353]

Average nr. of threads: [25.711409395973153]
Max nr. of threads: [37]
Max nr. of concurrent instances: [27]
```
- Request duration (AnalyzeResult): 512-1s (4.08), 4-8 (12.24), 2-4 (2.04)
- Request duration (SubmitFile): 1.1m-2.2 (18.2), 4.1s-8.2s (1.01)
- HTTP Requests received: All (0.734)

==============================================================
0. 1 instance, known file - [dashboard](http://localhost:3000/dashboard/snapshot/E9g8RJgTDXhrqRIlTxnvMi6eA4s7Ahat)
```
Timed out: [0]
Failed: [0]
Average time: [2250.7568]
Best time: [2250.7568]
Worst time: [2250.7568]

Average nr. of threads: [11]
Max nr. of threads: [11]
Max nr. of concurrent instances: [1]
```
- Request duration (AnalyzeResult): 128-256

1. 100 instances, known file - [dashboard](http://localhost:3000/dashboard/snapshot/K7S2U3nzW3XK8nyS7zdBtz0Clap7qMGH), [dashboard 2](http://localhost:3000/dashboard/snapshot/J8Wr7jBzvznC36ooF0nxaPtzID03k0VP)
```
Timed out: [0]
Failed: [0]
Average time: [2552.598176]
Best time: [2199.3512]
Worst time: [3416.0346]

Average nr. of threads: [17.525862068965516]
Max nr. of threads: [23]
Max nr. of concurrent instances: [14]
```
- Request duration (AnalyzeResult): 256-512 (3.00), 4-8 (67.21), 2-4 (11.21)
- HTTP Requests received: All (4.20)

2. 100 instances, known file, min 50 threads - [dashboard](http://localhost:3000/dashboard/snapshot/Y7ltLIpSrGypW5gm7ACos8BppXfTL1XG)
```
Timed out: [0]
Failed: [0]
Average time: [7438.561425]
Best time: [3953.5481]
Worst time: [8444.3599]

Average nr. of threads: [60.348837209302324]
Max nr. of threads: [61]
Max nr. of concurrent instances: [53]
```
- Request duration (AnalyzeResult): 64-128 (16.65), 512-1s (4), 2-4 (6.25)
- HTTP Requests received: All (3.29)

3. 1000 instances, known file - [dashboard](http://localhost:3000/dashboard/snapshot/ZO2u7kjF8lfyOfP413zw4YJTz52DeWxe), [dashboard 2](http://localhost:3000/dashboard/snapshot/Zh6UwMtIi3NUdL7ps4ZGx5Sj9SbR5XbP)
```
Timed out: [0]
Failed: [0]
Average time: [5182.830944000008]
Best time: [2195.3547]
Worst time: [8475.0894]

Average nr. of threads: [41.877376425855516]
Max nr. of threads: [60]
Max nr. of concurrent instances: [52]
```
- Request duration (AnalyzeResult): 256-512 (28.07), 4-8 (253.71), 2-4 (206.58)
- HTTP Requests received: All (7.28)

4. 1000 instances, known file, min 100 threads - [dashboard](http://localhost:3000/dashboard/snapshot/0CpFblXUeY1WSGiTdyecds2ImGxQztFT), [dashboard 2](http://localhost:3000/dashboard/snapshot/jKcn1SStU1AF3QMw7p4hgPP5sywrqD6O)
```
Timed out: [0]
Failed: [0]
Average time: [10656.149587900005]
Best time: [5230.7033]
Worst time: [17750.4029]

Average nr. of threads: [108.28346456692914]
Max nr. of threads: [110]
Max nr. of concurrent instances: [102]
```
- Request duration (AnalyzeResult): 4.1s-8.2s (10.14), 2-4 (195.63), 1-2 (19.26)
- HTTP Requests received: All (9.51)


===========================================================================================================================================


## 2. gRPC 
1. 100 instances, 1 MB binary file, new file
```
Timed out: [0]
Failed: [0]
Average time: [1447.5808300000008]
Best time: [717.2967]
Worst time: [2466.1643]

Average nr. of threads: [12.915254237288135]
Max nr. of threads: [14]
```


## TODO
- exclude HASH calculation time (both) => calculate time for different file sizes
- export dashboard snapshots
- export perfmon created stats (elasticsearch, prometheus)
- run tests when file is known