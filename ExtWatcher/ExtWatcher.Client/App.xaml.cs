using ExtWatcher.Common.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceProcess;
using System.Windows;

namespace ExtWatcher.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NotifyClient _client;
        private List<string> _monitoredPaths;
        private Guid _id;

        public App()
        {
            WaitForServiceToStart();

            InstanceContext site = new InstanceContext(new NotifyCallback());
            _client = new NotifyClient(site);
            _id = Guid.NewGuid();

            try
            {
                _monitoredPaths = new List<string>(ConfigurationManager.AppSettings["extwatcher:MonitoredPaths"].Split(new char[] { ';' }));
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.WriteToLog("Could not extract monitoredPaths from config file.");
                Logger.WriteToLog(e);
                CloseSession();
            }

            StartSession();
        }

        private void WaitForServiceToStart()
        {
            var sc = new ServiceController("ExtWatcherService");
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            CloseSession();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            CloseSession();
            base.OnExit(e);
        }

        private void StartSession()
        {
            try
            {
                Logger.WriteToLog(String.Format("Client with GUID: '{0}' is starting.", _id));

                _client.Register(_id);

                foreach (string path in _monitoredPaths)
                {
                    if (!Directory.Exists(path))
                    {
                        Logger.WriteToLog(String.Format("[GUID: '{0}'] '{1}' monitored path does not exist.", _id, path));
                    }
                    else
                    { 
                        Logger.WriteToLog(String.Format("[GUID: '{0}'] Adding '{1}' path to monitoring.", _id, path));
                        _client.Start(path);
                    }
                }
            }
            catch (System.TimeoutException eTime)
            {
                Logger.WriteToLog(eTime);
                CloseSession();
            }
            catch (CommunicationException eComm)
            {
                Logger.WriteToLog(eComm);
                CloseSession();
            }
        }

        private void CloseSession()
        {
            Logger.WriteToLog(String.Format("Closing session for client with GUID: '{0}'.", _id));

            foreach (string path in _monitoredPaths)
            {
                _client.Stop(path);
            }
            _client.UnRegister(_id);
            _client.Abort();

            System.Windows.Application.Current.Shutdown();
        }
    }
}
