using ExtWatcher.Common.Contract;
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
            if (_queueServiceThread == null)
            {
                _queueServiceThread = new Thread(new ThreadStart(QueueServiceThreadProc))
                    {
                        Priority = ThreadPriority.AboveNormal,
                        IsBackground = true
                    };
                _queueServiceThread.Start();
            }

            foreach (FolderWatcher folder in _folderDictionary.Values)
            {
                folder.Start();
            }
        }

        public void Add(string folderToMonitor)
        {
            bool result = _folderDictionary.TryAdd(folderToMonitor, FolderWatcher.Create(this, folderToMonitor));
            if (!result)
            {
                //TODO log if unable to add data, it exists
            }
        }

        public void Remove(string folderToMonitor)
        {
            FolderWatcher folder;
            bool result = _folderDictionary.TryRemove(folderToMonitor, out folder);
            if (!result)
            {
                //TODO log if unable to remove key
            }

        }

        public void Stop()
        {
            if (_queueServiceThread != null)
            {
                _fileEventQueue.CompleteAdding();
                _fileEventQueue.Dispose();
                _queueServiceThread = null;

                foreach (FolderWatcher folder in _folderDictionary.Values)
                {
                    folder.Stop();
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
            try
            {
                foreach (var args in _fileEventQueue.GetConsumingEnumerable())
                {
                    FireFileEvent(args);
                }
            }
            catch (Exception)
            {
                //TODO report errror with Logger
            }
        }

        private void FireFileEvent(FileEventArgs args)
        {
            if (_fileEvent != null)
            {
                _fileEvent(this, args);
            }
        }

        public event FileEventHandler FileEvent
        {
            remove { _fileEvent -= value; }
            add { _fileEvent += value; }
        }

        public void AddQueueItem(FileEventArgs e)
        {
            _fileEventQueue.Add(e);
        }
    }
}
