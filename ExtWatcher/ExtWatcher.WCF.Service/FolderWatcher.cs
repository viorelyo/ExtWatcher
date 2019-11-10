using ExtWatcher.Common.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service
{
    internal class FolderWatcher
    {
        private string _directoryPath;
        private FileSystemWatcher _watcher = new FileSystemWatcher();
        private Monitor _monitor;

        private FolderWatcher(Monitor monitor, string directoryToMonitor)
        {
            _directoryPath = directoryToMonitor;
            _monitor = monitor;

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
            _monitor.AddQueueItem(FileEventArgs.Create(e, _directoryPath));
        }
    }
}
