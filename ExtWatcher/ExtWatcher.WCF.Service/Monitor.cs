using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service
{
    internal class Monitor
    {
        private ConcurrentDictionary<string, FolderWatcher> _folderDictionary = new ConcurrentDictionary<string, FolderWatcher>();
        private readonly BlockingCollection<FileEventArgs> _fileEventQueue = new BlockingCollection<FileEventArgs>(new ConcurrentQueue<FileEventArgs>());
        private event FileEventHandler _fileEvent;
        private Thread _queueServiceThread;

        private Monitor() { }

        public static Monitor Create()
        {
            return new Monitor();
        }

        public void Start()
        {
            Logger.WriteToLog(String.Format("Starting the Working Thread..."));
            if (_queueServiceThread == null)
            {
                _queueServiceThread = new Thread(new ThreadStart(QueueServiceThreadProc))
                    {
                        Priority = ThreadPriority.AboveNormal,
                        IsBackground = true
                    };
                _queueServiceThread.Start();
            }

            foreach (var folder in _folderDictionary)
            {
                Logger.WriteToLog(String.Format("Starting FolderWatch for '{0}'.", folder.Key));    
                folder.Value.Start();
            }
        }

        public void Add(string folderToMonitor)
        {
            Logger.WriteToLog(String.Format("Adding '{0}' to MonitoredFolders.", folderToMonitor));
            bool result = _folderDictionary.TryAdd(folderToMonitor, FolderWatcher.Create(this, folderToMonitor));
            if (!result)
            {
                Logger.WriteToLog(String.Format("Unable to add '{0}' to MonitoredFolders.", folderToMonitor));
            }
        }

        public void Remove(string folderToMonitor)
        {
            FolderWatcher folder;
            Logger.WriteToLog(String.Format("Removing '{0}' from MonitoredFolders.", folderToMonitor));
            bool result = _folderDictionary.TryRemove(folderToMonitor, out folder);
            if (!result)
            {
                Logger.WriteToLog(String.Format("Unable to remove '{0}' from MonitoredFolders.", folderToMonitor));
            }
        }

        public void Stop()
        {
            Logger.WriteToLog(String.Format("Stopping the Working Thread..."));
            if (_queueServiceThread != null)
            {
                _fileEventQueue.CompleteAdding();
                _fileEventQueue.Dispose();
                _queueServiceThread = null;

                foreach (var folder in _folderDictionary)
                {
                    Logger.WriteToLog(String.Format("Stopping FolderWatch for '{0}'.", folder.Key));
                    folder.Value.Stop();
                }
            }
        }

        /// <summary>
        /// Worker function for Thread
        /// The function dequeues each args from the fileEventQueue and then triggers the event for it.
        /// Source: https://stackoverflow.com/questions/43117502/thread-safety-in-concurrent-queue-c-sharp
        /// Source: https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/how-to-use-foreach-to-remove
        /// </summary>
        private void QueueServiceThreadProc()
        {
            Logger.WriteToLog(String.Format("Starting the Worker Thread to process the Queue of FileArgs."));
            try
            {
                foreach (var args in _fileEventQueue.GetConsumingEnumerable())
                {
                    FireFileEvent(args);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(ex);
            }
        }

        private void FireFileEvent(FileEventArgs args)
        {
            if (_fileEvent != null)
            {
                Logger.WriteToLog(String.Format("Firing FileEventArgs [{0}; {1}; {2}; {3}].", args.Folder, args.FileName, args.Date, args.ChangeType));
                _fileEvent(this, args);
            }
        }

        public event FileEventHandler FileEvent
        {
            remove { _fileEvent -= value; }
            add { _fileEvent += value; }
        }

        public void AddQueueItem(FileEventArgs args)
        {
            Logger.WriteToLog(String.Format("Adding FileEventArgs to Queue [{0}; {1}; {2}; {3}].", args.Folder, args.FileName, args.Date, args.ChangeType));
            _fileEventQueue.Add(args);
        }
    }
}
