using DirectoryMonitoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitoring.Impl
{
    [ServiceBehavior(ConfigurationName = "DirectoryMonitoring.Impl:DirectoryMonitoring.Impl.NotifyEndpoint", InstanceContextMode = InstanceContextMode.Single)]
    public class NotifyEndpoint : INotify
    {
        public void Register(Guid instanceId)
        {
            throw new NotImplementedException();
        }
    }
}
