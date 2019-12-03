using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.Common.Service
{
    public class WCFServiceHost
    {
        private List<ServiceHost> _serviceHostList;
        private EventLog EventLog { get; set; }

        public static WCFServiceHost Create(EventLog eventLog)
        {
            return new WCFServiceHost()
            {
                EventLog = eventLog
            };
        }

        /// <summary>
        /// Hosts the WCF Services in a Console App for debugging.
        /// </summary>
        /// <param name="serviceAppName"></param>
        /// <param name="eventLog"></param>
        public static void RunServiceAsConsoleApp(string serviceAppName, EventLog eventLog)
        {
            Console.WriteLine(String.Format("Running {0} Service. \n\n", serviceAppName));

            var serviceHost = WCFServiceHost.Create(eventLog);
            serviceHost.Start();

            Console.WriteLine(String.Format("\n{0} WCF services have successfully started.\n\nPress ENTER to exit.", serviceAppName));
            Console.ReadKey(false);
        }

        public void Start()
        {
            Stop();
            Console.WriteLine("Loading WCF Services...");

            _serviceHostList = new List<ServiceHost>();

            string appFolder = GetAppFolder();
            WCFServices wcfServices = WCFServices.Create(GetAppConfig());

            //Load WCF Service assemblies and create a service host for each service
            foreach (var wcfService in wcfServices.ServiceModel.Services)
            {
                // If assembly is duplicated, it's loaded only once
                var assemblyName = Path.Combine(appFolder, wcfService.AssemblyName);
                var assembly = Assembly.LoadFile(assemblyName);

                // Get the type from the class name
                var type = assembly.GetType(wcfService.ClassName);

                var host = new ServiceHost(type);
                host.Faulted += new EventHandler(OnServiceHostFaulted);

                // Create a service host based on the type and add it to our list
                _serviceHostList.Add(host);
            }

            Console.WriteLine("Starting WCF Services...\n");
            // Start each WCF ServiceHost in our list
            foreach (ServiceHost serviceHost in _serviceHostList)
            {
                Console.Write(String.Format("\t{0}...", serviceHost.Description.Name));
                serviceHost.Open();
                Console.WriteLine(String.Format(" {0}", serviceHost.State));
            }
        }

        public void Stop()
        {
            if (null == _serviceHostList)
            {
                return;
            }

            // Stop each open WCF service from the list
            foreach (var serviceHost in _serviceHostList)
            {
                if (serviceHost.State != CommunicationState.Closed)
                {
                    serviceHost.Close();
                }
            }

            // Clear the list before Start
            _serviceHostList.Clear();
            _serviceHostList = null;
        }

        private void OnServiceHostFaulted(object sender, EventArgs e)
        {
            foreach (var serviceHost in _serviceHostList)
            {
                if (serviceHost.State == CommunicationState.Faulted)
                {
                    EventLog.WriteEntry(String.Format("The {0} service has faulted.", serviceHost.Description.Name),
                        EventLogEntryType.Error);
                }
            }
        }

        private string GetAppFolder()
        {
            string appFolder = GetAppConfig();
            return appFolder.Substring(0, appFolder.LastIndexOf('\\'));
        }

        private string GetAppConfig()
        {
            return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
        }
    }
}
