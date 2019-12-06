# Bachelor-Ideas
Samples of Code for implementing final idea for Undegraduate Project

## General TODO
- [ ] References
- [ ] Latex Documentation
- [x] Talk Christian Sacarea 
- [ ] Datasets
- [ ] Brainstorm on `brand` / `logo` (**ExtWatcher**)
- [ ] Include or no Microsoft Office Suite Docs / other Docs (**Nope**)


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
- [x] **CornerCases**: Use Chrome at DEMO (Firefox is downloading original + cache -> 2 create Notifications)
- [ ] **CornerCases**: Exclude RecycleBin from monitored directories
- [ ] Code refactor (Use generic controller for extension checking)
- [ ] Logger should log each log
- [ ] Graceful close of client app -> close connections (Handler on close buttons)
- [ ] Use config files for: extensions, monitoredDirectories
- [ ] integrate HTTPWebRequest POST Method to upload file to Flask Server
- [ ] Check if created files are PDFs (`.pdf` extension) (*add ExtensionController maybe as DLL (for future dynamic hotpatch update)*)
- [ ] Generate Client (WCF)
- [ ] Push Windows 10 notification `Toast Notification WPF` (*?keep notification opened until scan is completed?*) [ Naspa: 2 aplicatii `WCF` ]
- [ ] Use configuration files (server address, monitored directories)
- [ ] Create protected folder (hidden / without user access) and move PDF file to that folder until scan is completed.
- [ ] Create Flask server to simulate sandbox
- [ ] Spike for investigating technology to use for file submit from windows service
- [ ] Submit (HTTP) PDF file to the server to be analyzed 

###### Resources
- https://www.codeguru.com/csharp/.net/net_wcf/article.php/c17179/Tray-Notify--Part-I-Getting-Started.htm (`Use Diagram`)