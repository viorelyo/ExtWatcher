using ExtWatcher.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service
{
    internal class Monitor
    {
        private Queue<FileEventArgs> _fileEventQueue = new Queue<FileEventArgs>()

        private Monitor() { }

        public static Monitor Create()
        {
            return new Monitor();
        }

        public void Start()
        {
            
        }

        public void AddQueueItem(FileEventArgs e)
        {
            _fileEventQueue.Enqueue(e);
        }
    }
}
