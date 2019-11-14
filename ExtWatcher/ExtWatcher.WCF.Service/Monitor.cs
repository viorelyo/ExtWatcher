using ExtWatcher.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service
{
    internal class Monitor
    {
        private const int StopEvent = 0;
        private const int ItemAddedEvent = 1;

        private FolderWatcher _watchedFolder;
        //TODO use Locks / Use ConcurrentQueue / Use QueueSync from example
        private Queue<FileEventArgs> _fileEventQueue = new Queue<FileEventArgs>();
        private AutoResetEvent _itemAddedEvent = new AutoResetEvent(false);
        private AutoResetEvent _stopEvent = new AutoResetEvent(false);
        private event FileEventHandler _fileEvent;
        private WaitHandle[] _waitHandles;
        private Thread _queueServiceThread;


        private Monitor() { }

        public static Monitor Create()
        {
            return new Monitor();
        }

        public void Start()
        {
            if (_waitHandles == null)
            {
                _waitHandles = new WaitHandle[] { _itemAddedEvent };
            }

            if (_queueServiceThread == null)
            {
                _queueServiceThread = new Thread(new ThreadStart(QueueServiceThreadProc))
                    {
                        Priority = ThreadPriority.AboveNormal,
                        IsBackground = true
                    };
                _queueServiceThread.Start();
            }

            _watchedFolder.Start();
        }

        public void Add(string folderToMonitor)
        {
            //TODO save monitored folders into concurrentDictionary
            _watchedFolder = FolderWatcher.Create(this, folderToMonitor);
        }

        public void Stop()
        {
            if (_queueServiceThread != null)
            {
                _stopEvent.Set();
                _queueServiceThread = null;

                _watchedFolder.Stop();
            }
        }

        private void QueueServiceThreadProc()
        {
            try
            {
                while (true)
                {
                    switch (WaitHandle.WaitAny(_waitHandles, Timeout.Infinite, false))
                    {
                        case StopEvent:
                            return;
                        case ItemAddedEvent:
                            FileEventArgs args = null;
                            if (null != (args = _fileEventQueue.Peek()))
                            {
                                FireFileEvent(args);
                                _fileEventQueue.Dequeue();
                            }
                            break;
                    }
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
            _fileEventQueue.Enqueue(e);
            _itemAddedEvent.Set();
        }
    }
}
