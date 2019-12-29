using ExtWatcher.Common.Contract;
using ExtWatcher.WCF.Service.Controller;
using System.IO;

namespace ExtWatcher.WCF.Service.Core
{
    internal class FolderWatcher
    {
        private readonly string _directoryPath;
        private FileSystemWatcher _watcher = new FileSystemWatcher();
        private Monitor _monitor;
        private ExtensionController _extensionCtrl;

        private FolderWatcher(Monitor monitor, string directoryToMonitor)
        {
            _directoryPath = directoryToMonitor;
            _monitor = monitor;
            _extensionCtrl = new ExtensionController();

            _watcher.Path = _directoryPath;
            _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            _watcher.IncludeSubdirectories = true;
        }

        public static FolderWatcher Create(Monitor monitor, string directoryToMonitor)
        {
            return new FolderWatcher(monitor, directoryToMonitor);
        }

        public void Start()
        {
            _watcher.Created += new FileSystemEventHandler(OnFileEvent);

            _watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;

            _watcher.Created -= new FileSystemEventHandler(OnFileEvent);
        }

        private void OnFileEvent(object sender, FileSystemEventArgs e)
        {
            string fullPath = e.FullPath;

            // Exclude files added to Recycle Bin
            // If isn't working, refactor using this: https://www.dreamincode.net/forums/topic/164491-working-with-the-windows-recycle-bin-with-c%23/
            if (!fullPath.Contains("$Recycle.Bin") && !fullPath.Contains("$RECYCLE.BIN"))
            { 
                if (_extensionCtrl.IsExtensionSupported(fullPath))
                {
                    _monitor.AddQueueItem(FileEventArgs.Create(e, _directoryPath));
                }
            }
        }
    }
}
