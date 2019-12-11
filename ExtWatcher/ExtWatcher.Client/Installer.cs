using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ExtWatcher.Common.Utils;
using Microsoft.Win32;

namespace ExtWatcher.Client
{
    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer
    {
        public Installer() : base()
        {
            this.Committed += new System.Configuration.Install.InstallEventHandler(Installer_Committed);
            this.Committing += new System.Configuration.Install.InstallEventHandler(Installer_Committing);
        }

        private void Installer_Committing(object sender, InstallEventArgs e)
        {
        }

        private void Installer_Committed(object sender, InstallEventArgs e)
        {
            try
            {
                string appLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ExtWatcher.Client.exe";
                Logger.WriteToLog(appLocation);

                // Set RegistryKey to run app at windows startup
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("ExtWatcher.Client", appLocation);

                // Start App after Install finishes
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Process.Start(appLocation);
            }
            catch
            {
                // Do nothing
            }
        }

        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }
    }
}
