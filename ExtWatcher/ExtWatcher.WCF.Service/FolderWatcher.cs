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

        private FolderWatcher(string directoryToMonitor)
        {
            _directoryPath = directoryToMonitor;

            _watcher.Path = _directoryPath;
            _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            _watcher.IncludeSubdirectories = true;
        }

        public static FolderWatcher Create(string directoryToMonitor)
        {
            return new FolderWatcher(directoryToMonitor);
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
            //TODO add behaviour
        }
    }
}
