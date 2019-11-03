# Bachelor-Ideas
Samples of Code for implementing final idea for Undegraduate Project

## General TODO
- [ ] References
- [ ] Latex Documentation
- [ ] Talk Christian Sacarea 
- [ ] Datasets
- [ ] Brainstorm on `brand` / `logo`
- [ ] Include or no Microsoft Office Suite Docs / other Docs


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
- [x] Check if created files are PDFs (`.pdf` extension)
- [x] Log everything 
- [ ] Use configuration files (server address, monitored directories)
- [ ] Create protected folder (hidden / without user access) and move PDF file to that folder until scan is completed.
- [ ] Push Windows 10 notification `Toast Notification WPF` (*?keep notification opened until scan is completed?*) [ Naspa: 2 aplicatii .NET Remotin / NamedPipe ]
- [ ] Submit (HTTP) PDF file to the server to be analyzed

###### Resources
- https://www.codeguru.com/csharp/.net/net_wcf/article.php/c17179/Tray-Notify--Part-I-Getting-Started.htm (`Use Diagram`)