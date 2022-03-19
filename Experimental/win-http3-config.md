# Demo grpc over HTTP/3 (.NET 6) SETUP

1. Win11 Enterprise VM + initial snapshot (includes .NET 6; should install ASP.NET)
2. Enable HTTP/3
	- `reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\HTTP\Parameters" /v EnableHttp3 /t REG_DWORD /d 1 /f`
    - `reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\HTTP\Parameters" /v EnableAltSvc /t REG_DWORD /d 1 /f`
   **Reboot computer** / restart http.sys
3. Enable TLS1.3
	- [HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Client]
		"DisabledByDefault"=dword:00000000
		"Enabled"=dword:ffffffff
	- [HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Server]
		"DisabledByDefault"=dword:00000000
		"Enabled"=dword:ffffffff
4. `Enable-TlsCipherSuite -Name TLS_CHACHA20_POLY1305_SHA256 -Position 0`
5. Make sure firewall is not blocking `New-NetFirewallRule -DisplayName "Allow QUIC" -Direction Inbound -Protocol UDP -LocalPort 443 -Action Allow -LocalOnlyMapping $true`

* (Some steps might be unnecessary)


## References
- [Base article](https://devblogs.microsoft.com/dotnet/http-3-support-in-dotnet-6/)
- [msquic enable HTTP/3 support (not sure if necessary)](https://techcommunity.microsoft.com/t5/networking-blog/enabling-http-3-support-on-windows-server-2022/ba-p/2676880)
- [Troubleshooting grpc over HTTP/3](https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-6.0)
- [grpc sample greeting project](https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-6.0&tabs=visual-studio)
- [msquic issues](https://github.com/microsoft/msquic/blob/main/docs/Deployment.md#firewall)


### Content Ideas
- [Deploying HTTP/3](https://techcommunity.microsoft.com/t5/networking-blog/deploying-http-3-on-windows-server-at-scale/ba-p/2839394)
- [HTTP/3 support for .NET 6](https://www.meziantou.net/using-http-3-quic-in-dotnet.htm)