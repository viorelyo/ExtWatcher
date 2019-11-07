using ExtWatcher.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service.Impl
{
    [ServiceBehavior(ConfigurationName = "ExtWatcher.WCF.Service.Impl:ExtWatcher.WCF.Service.Impl.NotifyEndpoint", InstanceContextMode = InstanceContextMode.Single)]
    public class NotifyEndpoint : INotify
    {
        public void Register(Guid instanceId)
        {
            throw new NotImplementedException();
        }
    }
}
