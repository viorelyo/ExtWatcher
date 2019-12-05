using ExtWatcher.Common.Contract;
using System.ServiceModel;

namespace ExtWatcher.Common.Interface
{
    [ServiceContract]
    public interface INotifyCallback
    {
        [OperationContract]
        void OnPDFFileCreatedEvent(FileEventArgs e);
    }
}
