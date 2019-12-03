using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ExtWatcherService service = new ExtWatcherService();

            // If Debug Mode Enabled, run WCF Service as Console App
            if (IsDebugMode(args))
            {
                ExtWatcher.Common.Service.WCFServiceHost.RunServiceAsConsoleApp("ExtWatcher Host", service.EventLog);
                return;
            }

            // If Debug Mode Disabled, run WCF Service in Windows Service
            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    service
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception e)
            {
                service.EventLog.WriteEntry(e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        private static bool IsDebugMode(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return false;
            }
            if (args[0].ToLower() == "/debug")
            {
                return true;
            }
            return false;
        }
    }
}
