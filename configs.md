# Configurations for ExtWatcher

### Additional applied settings
1. ExtWatcher (Windows Service) Properties -> Application -> Output Type -> SET TO: `Console Application` instead of `Windows Application`
2. App.config - code commented
3. ExtWatcher.WCF.Service Properties -> WCF Options -> DISABLE: Start WCF Service Host when debugging another project in the same solutions
`https://blogs.msdn.microsoft.com/keithmg/2009/08/10/http-could-not-register-url-http-another-application-has-already-registered-this-url-with-http-sys/`
4. For DEBUG: ExtWatcher Properties -> Debug -> Command line args: `/Debug`


#### Resources
1. `https://www.codeguru.com/csharp/.net/net_wcf/article.php/c17187/Tray-Notify--Part-III-WCF-Service.htm#page-5`
2. `https://github.com/baikangwang/Learn/blob/05d8fbfb9db875e42b69ef035369e280f4494afe/CG.TrayNotify/CG.TrayNotify.Host.Service/Program.cs`
3. `https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-host-a-wcf-service-in-a-managed-windows-service?redirectedfrom=MSDN`
4. `https://stackoverflow.com/questions/6064989/running-a-windows-service-in-console-mode`