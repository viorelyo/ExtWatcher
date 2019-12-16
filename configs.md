# Configurations for ExtWatcher

### Additional applied settings 
0. Dependency Injection on WCF Service
1. ExtWatcher (Windows Service) Properties -> Application -> Output Type -> SET TO: `Console Application` instead of `Windows Application`
2. App.config - code commented
3. ExtWatcher.WCF.Service Properties -> WCF Options -> DISABLE: Start WCF Service Host when debugging another project in the same solutions
`https://blogs.msdn.microsoft.com/keithmg/2009/08/10/http-could-not-register-url-http-another-application-has-already-registered-this-url-with-http-sys/`
4. For DEBUG: ExtWatcher Properties -> Debug -> Command line args: `/Debug`
5. Used command to generate code for client: `"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\SvcUtil.exe" /language:cs /out:Notify.cs /config:app.config http://localhost:8000/ExtWatcher/`

##### For installer
1. Install extension for setup in VS2017: `https://stackoverflow.com/questions/43308270/vs2017-setup-project-where`
2. For startup: `https://stackoverflow.com/questions/5092477/set-that-a-program-has-to-run-at-startup-from-an-installer`
    Create shortcut for client and move to special created folder `User's Startup Folder` (from the Setup Project)
3. For x64 installer: `https://stackoverflow.com/questions/15257865/install-a-service-as-x64?rq=1`
    Edit with `Orca` the created .msi from Setup: change InstallUtilLib.dll - to load x64 version
4. Set service (`serviceInstaller`) to run Automatic (Delayed Start = true)



#### Resources
1. `https://www.codeguru.com/csharp/.net/net_wcf/article.php/c17187/Tray-Notify--Part-III-WCF-Service.htm#page-5`
2. `https://github.com/baikangwang/Learn/blob/05d8fbfb9db875e42b69ef035369e280f4494afe/CG.TrayNotify/CG.TrayNotify.Host.Service/Program.cs`
3. `https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-host-a-wcf-service-in-a-managed-windows-service?redirectedfrom=MSDN`
4. `https://stackoverflow.com/questions/6064989/running-a-windows-service-in-console-mode`
5. Client Part: `https://docs.microsoft.com/en-us/dotnet/framework/wcf/how-to-create-a-wcf-client`
6. Hidden WPF Client: `https://social.msdn.microsoft.com/Forums/silverlight/en-US/e4e819cd-4e00-4b35-9d5d-d2cf91f886d7/starting-a-wpf-application-in-hidden-mode?forum=wpf`
7. Concept of client with windows service: `https://docs.microsoft.com/en-us/windows/win32/services/interactive-services`
8. Possible solution: `https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/hh802768(v%3Dvs.85)`
