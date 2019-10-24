using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitoring
{
    public partial class DirectoryMonitoringService : ServiceBase
    {
        protected FileSystemWatcher Watcher;

        private string PathToFolder = @"C:\";

        public DirectoryMonitoringService()
        {
            Logger.Instance.LoggerPath = @"C:\service_test";
            Logger.Instance.LoggerFileName = "DirectoryMonitoring";
            Watcher = new MyFileSystemWatcher(PathToFolder);
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
