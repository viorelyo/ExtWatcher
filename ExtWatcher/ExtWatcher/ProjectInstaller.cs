using System.ComponentModel;
using System.Configuration.Install;
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

        private void serviceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            new ServiceController(serviceInstaller.ServiceName).Start();
        }

        private void serviceInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            new ServiceController(serviceInstaller.ServiceName).Stop();
        }
    }
}
