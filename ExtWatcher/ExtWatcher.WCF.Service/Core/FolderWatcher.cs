﻿using ExtWatcher.Common.Contract;
using System.IO;

namespace ExtWatcher.WCF.Service.Core
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
            string extension = Path.GetExtension(e.FullPath);
            if (extension == ".pdf")
            {
                _monitor.AddQueueItem(FileEventArgs.Create(e, _directoryPath));
            }
        }
    }
}
