﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.Common.Interface
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(INotifyCallback))]
    public interface INotify
    {
        [OperationContract]
        void Register(Guid instanceId);
    }
}
