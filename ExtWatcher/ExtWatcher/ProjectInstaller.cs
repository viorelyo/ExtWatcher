using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;

namespace ExtWatcher
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            FileStream stream = new FileStream(@"C:\install\log.log", FileMode.Append, FileAccess.Write);
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("Service - AfterInstall");
            }
            stream.Close();

            new ServiceController(serviceInstaller.ServiceName).Start();
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
            FileStream stream = new FileStream(@"C:\install\log.log", FileMode.Append, FileAccess.Write);
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("Service - BeforeUninstall");
            }
            stream.Close();

            var sc = new ServiceController(serviceInstaller.ServiceName);
            if (sc.Status != ServiceControllerStatus.Stopped)
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
            }
        }
    }
}
