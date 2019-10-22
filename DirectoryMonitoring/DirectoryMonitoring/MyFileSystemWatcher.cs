using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitoring
{
    public class MyFileSystemWatcher : FileSystemWatcher
    {
        public MyFileSystemWatcher()
        {
            Init();
        }

        public MyFileSystemWatcher(string directoryPath) : base(directoryPath)
        {
            Init();
        }

        public MyFileSystemWatcher(string directoryPath, string filter)
        {
            Init();
        }

        private void Init()
        {
            IncludeSubdirectories = true;
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            EnableRaisingEvents = true;
            Created += Watcher_Created;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Logger.WriteLine("File created or added: " + e.FullPath);
        }
    }
}
