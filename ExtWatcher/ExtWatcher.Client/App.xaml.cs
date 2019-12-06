using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace ExtWatcher.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InstanceContext site = new InstanceContext(new NotifyCallback());
            NotifyClient client = new NotifyClient(site);

            Guid id = Guid.NewGuid();
            string path = @"C:\";

            try
            {
                client.Register(id);
                client.Start(path);
                Console.WriteLine("Started!"); 
            }
            catch (TimeoutException eTime)
            {
                Console.WriteLine("Timeout. " + eTime.Message);
                client.Stop(path);
                client.UnRegister(id);
                client.Abort();
            }
            catch (CommunicationException eComm)
            {
                Console.WriteLine("Timeout. " + eComm.Message);
                client.Stop(path);
                client.UnRegister(id);
                client.Abort();
            }
        }
    }
}
