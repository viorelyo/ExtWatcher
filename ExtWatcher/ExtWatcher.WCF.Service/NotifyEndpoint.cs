﻿using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Interface;
using ExtWatcher.Common.Utils;
using ExtWatcher.WCF.Service.Controller;
using ExtWatcher.WCF.Service.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Threading;

namespace ExtWatcher.WCF.Service
{
    [ServiceBehavior(ConfigurationName = "ExtWatcher.WCF.Service:ExtWatcher.WCF.Service.NotifyEndpoint", InstanceContextMode = InstanceContextMode.Single)]
    public class NotifyEndpoint : INotify
    {
        private Core.Monitor _monitor = Core.Monitor.Create();
        private ConcurrentDictionary<Guid, Client> _clients = new ConcurrentDictionary<Guid, Client>();
        private List<Thread> _fileAnalyzersThreads = new List<Thread>();
        private string _cloudAnalyzerURL;

        public void Start(string folderToMonitor)
        {
            Logger.WriteToLog("Starting NotifyEndpoint.");
            _monitor.Add(folderToMonitor);
            LoadCloudAnalyzerURL();
            _monitor.Start();
        }

        private void LoadCloudAnalyzerURL()
        {
            try
            {
                _cloudAnalyzerURL = ConfigurationManager.AppSettings["extwatcher:CloudAnalyzerURL"];
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.WriteToLog("Could not extract Cloud Analyzer URL from config file.");
                Logger.WriteToLog(e);
            }
        }

        public void Stop(string folderToMonitor)
        {
            Logger.WriteToLog("Stopping NotifyEndpoint.");
            _monitor.Stop();
            _monitor.Remove(folderToMonitor);

            foreach (var faThread in _fileAnalyzersThreads)
            {
                faThread.Join(2000);
            }
        }

        public NotifyEndpoint()
        {
            _monitor.FileEvent += new FileEventHandler(OnFileEvent);
        }

        private void SendNotificationToAllClients(FileEventArgs e)
        {
            foreach (var client in _clients)
            {
                // Create a new thread for each client in order to send the notification
                ThreadPool.QueueUserWorkItem(NotifyThreadProc, NotifyThreadStateInfo.Create(client.Value, e));
            }
        }

        private void OnFileEvent(object sender, FileEventArgs e)
        {
            RemoveInvalidClients();

            SendNotificationToAllClients(e);

            // Analyze each file on a separate Thread 
            var _fileAnalyzerThread = new Thread(new ThreadStart(() => 
            {
                try
                { 
                    Logger.WriteToLog("Starting a new FileAnalyzerThread.");

                    FileAnalyzer fileAnalyzer = new FileAnalyzer(_cloudAnalyzerURL, e);
                    FileAnalysisStatus analysisStatus = fileAnalyzer.Analyze();
                    e.AnalysisStatus = analysisStatus;

                    SendNotificationToAllClients(e);
                }
                catch (Exception ex)
                { 
                    Logger.WriteToLog("Exception caught in FileAnalyzerThread.");
                    Logger.WriteToLog(ex);
                }
            }))
            {
                Priority = ThreadPriority.AboveNormal,
                IsBackground = true
            };

            _fileAnalyzersThreads.Add(_fileAnalyzerThread);
            _fileAnalyzerThread.Start();
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
                Logger.WriteToLog(String.Format("[NotifyThreadPool] Notifying client with GUID: '{0}', Info: '{1}', Status: '{2}'", 
                    stateInfo.Client.Id, stateInfo.Args.FileName, FileAnalysisStatusExtension.ToString(stateInfo.Args.AnalysisStatus)));
                stateInfo.Client.Callback.OnSentNotification(stateInfo.Args);
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
