# Bachelor-Ideas
Samples of Code for implementing final idea for Undegraduate Project

## General TODO
- [ ] References
- [ ] Latex Documentation
- [x] Talk Christian Sacarea 
- [ ] Datasets
- [ ] Brainstorm on `brand` / `logo` (**ExtWatcher**)
- [x] Include or no Microsoft Office Suite Docs / other Docs (**Nope**)


## Structure
1. .NET Windows Service for FileSystem monitoring (PDF files detector)
2. Python Flask server (REST API)
3. ML Model for malicios PDF detecting
4. WEB GUI for visualizing (VirusTotal alike)
* *?Raspberry PI Server deployment?*


### .NET Windows Service
Windows Service (runs in background) implemented using C# that monitors in real time all the new PDF files that were downloaded (created on disk) and submits them to the server for analyzing.

###### TODO
- [x] .NET Service that uses `FileSystemWatcher` to monitor disk `C:\` (*?add manually other disks?*)
- [x] Log everything (Custom Logger)
- [x] Add debug mode for windows service `/Debug` parameter to be added
- [x] **CornerCases**: Use **Chrome** at DEMO (Firefox is downloading original + cache -> 2 create Notifications)
- [ ] **CornerCases**: Exclude RecycleBin from monitored directories
- [x] Code refactor (Use generic controller for extension checking)
- [x] Graceful close of client app -> close connections (Handler on close events - Application_Exit / OnExit)
- [ ] Add registry at install service - to run app at windows login and Test it + test that app closes on logout
- [ ] Check function for install windows service
- [ ] Create script / wizzard for automatic install the service (`https://www.c-sharpcorner.com/UploadFile/b7531b/create-simple-window-service-and-setup-project-with-installa/` / `https://www.youtube.com/watch?v=cp2aFNtcZfk`)
- [ ] Integrate HTTPWebRequest POST Method to upload file to Flask Server
- [x] Check if created files are PDFs (`.pdf` extension) (*add ExtensionController maybe as DLL (for future dynamic hotpatch update)*)
- [x] Generate Client (WCF)
- [x] Push Windows 10 notification `Toast Notification WPF` (*?keep notification opened until scan is completed?*) [ Naspa: 2 aplicatii `WCF` ]
- [x] Use configuration files (supportedExstensions, server address, monitored directories)
- [ ] Create protected folder (hidden / without user access) and move PDF file to that folder until scan is completed.
- [ ] Create Flask server to simulate sandbox
- [ ] Spike for investigating technology to use for file submit from windows service
- [ ] Submit (HTTP) PDF file to the server to be analyzed 

- [ ] Submit PDF also to virusTotal and compare results

###### Resources
- https://www.codeguru.com/csharp/.net/net_wcf/article.php/c17179/Tray-Notify--Part-I-Getting-Started.htm (`Use Diagram`)
- https://books.google.ro/books?id=81OLVXxb-qcC&pg=PA1110&lpg=PA1110&dq=is+projectinstaller+called+once+c%23&source=bl&ots=XK7yLviRPe&sig=ACfU3U1gHbu-ZZJz0TWWi_--fXXv04jh1Q&hl=ro&sa=X&ved=2ahUKEwih_Kqln6TmAhXKvosKHS_PCw8Q6AEwBXoECAkQAQ  (`Windows Service Architecture - 32 Windows Services`)
- https://candordeveloper.com/2012/12/28/simple-installer-for-windows-service-using-visual-studio-2012/ (`Installer`)
- https://books.google.ro/books?id=4yPnAgAAQBAJ&pg=PA789&lpg=PA789&dq=ProjectInstaller+Install(&source=bl&ots=XdLuu0dfVj&sig=ACfU3U0qcWJJ8aerA7SczDTdaGrgnT8Tgg&hl=ro&sa=X&ved=2ahUKEwjIzeP3pqTmAhXJs4sKHd_PBHIQ6AEwEnoECAoQAQ#v=onepage&q=ProjectInstaller%20Install(&f=false (`27 - Install + Service Manager`)