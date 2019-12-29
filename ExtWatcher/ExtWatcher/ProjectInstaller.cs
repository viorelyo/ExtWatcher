using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Security.AccessControl;
using System.ServiceProcess;

namespace ExtWatcher
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private const string quarantineFolderPath = @"C:\ExtWatcher\Quarantine";

        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            CreateQuarantineFolder();
            using (var sc = new ServiceController(serviceInstaller.ServiceName))
            {
                sc.Start();
            }
        }

        /// <summary>
        /// Create folder, which no user can access
        /// </summary>
        private void CreateQuarantineFolder()
        {
            Directory.CreateDirectory(quarantineFolderPath);

            string adminUserName = Environment.UserName;
            DirectorySecurity ds = Directory.GetAccessControl(quarantineFolderPath);
            FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            ds.AddAccessRule(fsa);
            Directory.SetAccessControl(quarantineFolderPath, ds);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            RemoveQuarantineFolder();

            using (var sc = new ServiceController(serviceInstaller.ServiceName))
            { 
                if (sc.Status != ServiceControllerStatus.Stopped)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                }
            }
            base.OnBeforeUninstall(savedState);
        }

        private void RemoveQuarantineFolder()
        {
            Directory.Delete(quarantineFolderPath, true);
        }
    }
}
