using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Interface;
using ExtWatcher.Common.Utils;
using ExtWatcher.WCF.Service.Controller;
using ExtWatcher.WCF.Service.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;

namespace ExtWatcher.WCF.Service
{
    [ServiceBehavior(ConfigurationName = "ExtWatcher.WCF.Service:ExtWatcher.WCF.Service.NotifyEndpoint", InstanceContextMode = InstanceContextMode.Single)]
    public class NotifyEndpoint : INotify
    {
        private Core.Monitor _monitor = Core.Monitor.Create();
        private ConcurrentDictionary<Guid, Client> _clients = new ConcurrentDictionary<Guid, Client>();

        public void Start(string folderToMonitor)
        {
            Logger.WriteToLog("Starting NotifyEndpoint.");
            _monitor.Add(folderToMonitor);
            _monitor.Start();
        }

        public void Stop(string folderToMonitor)
        {
            Logger.WriteToLog("Stopping NotifyEndpoint.");
            _monitor.Stop();
            _monitor.Remove(folderToMonitor);
        }

        public NotifyEndpoint()
        {
            _monitor.FileEvent += new FileEventHandler(OnFileEvent);
        }

        private void OnFileEvent(object sender, FileEventArgs e)
        {
            FileAnalyzer fileAnalyzer = new FileAnalyzer();
            fileAnalyzer.Prepare(e);
            RemoveInvalidClients();
            
            foreach (var client in _clients)
            {
                // Create a new thread for each client in order to send the notification
                ThreadPool.QueueUserWorkItem(NotifyThreadProc, NotifyThreadStateInfo.Create(client.Value, e));
            }

            fileAnalyzer.Analyze();
        }

        private void NotifyThreadProc(object state)
        {
            NotifyThreadStateInfo stateInfo = state as NotifyThreadStateInfo;
            if (stateInfo == null)
            {
                return;
            }

            try
            {
                Logger.WriteToLog(String.Format("[NotifyThreadPool] Notifying client with GUID: '{0}', Info: '{1}'", stateInfo.Client.Id, stateInfo.Args.FileName));
                stateInfo.Client.Callback.OnFileCreatedEvent(stateInfo.Args);
            }
            catch (TimeoutException)
            {
                Logger.WriteToLog(String.Format("TimeoutException. Invalidating client with GUID: '{0}'.", stateInfo.Client.Id));
                stateInfo.Client.Invalidate();
            }
        }

        private void RemoveInvalidClients()
        {
            List<Guid> clientsToBeRemoved = new List<Guid>();
            foreach (Client client in _clients.Values)
            {
                if (!client.IsValid)
                {
                    clientsToBeRemoved.Add(client.Id);
                }
            }

            foreach (Guid id in clientsToBeRemoved)
            {
                Logger.WriteToLog(String.Format("Removing invalid client with GUID: '{0}'.", id));

                Client client;
                bool result = _clients.TryRemove(id, out client);
                if (!result)
                {
                    Logger.WriteToLog(String.Format("Unable to remove client with GUID: '{0}'. Client doesn't exist.", id));
                }
            }
        } 

        public void Register(Guid instanceId)
        {
            Logger.WriteToLog(String.Format("Registering new client with GUID: '{0}'.", instanceId));
            INotifyCallback caller = OperationContext.Current.GetCallbackChannel<INotifyCallback>();
            if (caller != null)
            {
                bool result = _clients.TryAdd(instanceId, Client.Create(instanceId, caller));
                if (!result)
                {
                    Logger.WriteToLog(String.Format("Unable to register new client with GUID: '{0}'.", instanceId));
                }
            }
        }

        public void UnRegister(Guid instanceId)
        {
            Logger.WriteToLog(String.Format("Unregistering client with GUID: '{0}'.", instanceId));

            Client client;
            bool result = _clients.TryRemove(instanceId, out client);
            if (!result)
            {
                Logger.WriteToLog(String.Format("Unable to unregister client with GUID: '{0}'. Client doesn't exist.", instanceId));
            }
        }
    }
}
