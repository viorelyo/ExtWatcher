using ExtWatcher.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.Common.Interface
{
    [ServiceContract]
    public interface INotifyCallback
    {
        [OperationContract]
        void OnPDFFileCreatedEvent(FileEventArgs e);
    }
}
