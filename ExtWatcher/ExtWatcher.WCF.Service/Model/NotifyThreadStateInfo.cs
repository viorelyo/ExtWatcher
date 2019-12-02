using ExtWatcher.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service.Model
{
    internal class NotifyThreadStateInfo
    {
        public Client Client { get; private set; }
        public FileEventArgs Args { get; private set; }

        private NotifyThreadStateInfo() { }

        public static NotifyThreadStateInfo Create(Client client, FileEventArgs e)
        {
            return new NotifyThreadStateInfo()
            {
                Client = client,
                Args = e
            };
        }
    }
}
