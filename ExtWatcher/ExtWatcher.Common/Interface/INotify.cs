using System;
using System.ServiceModel;

namespace ExtWatcher.Common.Interface
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(INotifyCallback))]
    public interface INotify
    {
        [OperationContract]
        void Register(Guid instanceId);

        [OperationContract]
        void UnRegister(Guid instanceId);

        [OperationContract]
        void Start(string folderToMonitor);

        [OperationContract]
        void Stop(string folderToMonitor);
    }
}
