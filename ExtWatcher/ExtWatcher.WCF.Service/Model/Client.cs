﻿using ExtWatcher.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service.Model
{
    internal class Client
    {
        public bool IsValid { get; private set; }
        public Guid Id { get; private set; }
        public INotifyCallback Callback { get; private set; }

        public void Invalidate()
        {
            IsValid = false;
        }

        private Client() { }

        public static Client Create(Guid instanceId, INotifyCallback caller)
        {
            return new Client()
            {
                Id = instanceId,
                Callback = caller,
                IsValid = true
            };
        }
    }
}
