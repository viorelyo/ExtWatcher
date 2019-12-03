using ExtWatcher.Common.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher
{
    public partial class ExtWatcherService : ServiceBase
    {
        private WCFServiceHost _wcfServiceHost;

        public ExtWatcherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            OnStop();

            if (_wcfServiceHost == null)
            {
                _wcfServiceHost = WCFServiceHost.Create(EventLog);
            }

            _wcfServiceHost.Start();
        }

        protected override void OnStop()
        {
            if (_wcfServiceHost == null)
            {
                return;
            }

            _wcfServiceHost.Stop();
        }

        protected override void OnShutdown()
        {
            OnStop();
            _wcfServiceHost = null;
        }
    }
}
