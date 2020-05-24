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
            //CheckIfAnotherInstanceNotRunning();
            WaitForServiceToStart();

            var nc = new NotifyCallback();
            nc.InitTrayMenu();

            InstanceContext site = new InstanceContext(nc);
            _client = new NotifyClient(site);
            _id = Guid.NewGuid();

            try
            {
                _monitoredPaths = new List<string>(ConfigurationManager.AppSettings["extwatcher:MonitoredPaths"].Split(new char[] { ';' }));
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.WriteToLog(String.Format("[GUID: '{0}'] Could not extract monitoredPaths from config file.", _id));
                Logger.WriteToLog(e);
                System.Windows.Application.Current.Shutdown();      // Calls Application_Exit()
            }

            StartSession();
        }

        private void CheckIfAnotherInstanceNotRunning()
        {
            //TODO implement this
            throw new NotImplementedException();
        }

        private void WaitForServiceToStart()
        {
            string serviceName = "ExtWatcherService";
            try
            {
                var sc = new ServiceController(serviceName);
                if (sc.Status != ServiceControllerStatus.Stopped)
                { 
                    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                }
                else
                {
                    // Start service if is stopped
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                }
            }
            catch (InvalidOperationException)
            {
                MessageBoxResult warningBox = MessageBox.Show("ExtWatcher Service isn't running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.WriteToLog(String.Format("Service '{0}' not found. Client can not start.", serviceName));
                Environment.Exit(0);        // Exits immediately
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            CloseSession();
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
                        _monitoredPaths.Remove(path);
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
                System.Windows.Application.Current.Shutdown();
            }
            catch (CommunicationException eComm)
            {
                Logger.WriteToLog(eComm);
                System.Windows.Application.Current.Shutdown();
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
        }
    }
}
