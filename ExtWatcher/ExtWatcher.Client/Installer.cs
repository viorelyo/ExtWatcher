using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ExtWatcher.Client
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            FileStream stream = new FileStream(@"C:\install\log.log", FileMode.Append, FileAccess.Write);
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("Client - Commit");
            }
            stream.Close();

            try
            {
                string appLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ExtWatcher.Client.exe";

                // Start App after Install finishes
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Process.Start(appLocation);
            }
            catch (Exception)
            {
                base.Rollback(savedState);
            }
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
            FileStream stream = new FileStream(@"C:\install\log.log", FileMode.Append, FileAccess.Write);
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("Client - OnBeforeUninstall");
            }
            stream.Close();

            try
            {
                string appName = "ExtWatcher.Client";

                // Stop running instances of App
                foreach (var proc in Process.GetProcessesByName(appName))
                {
                    proc.Kill();
                }
            }
            catch
            {
                // Do nothing
            }
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            FileStream stream = new FileStream(@"C:\install\log.log", FileMode.Append, FileAccess.Write);
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("Client - Uninstall");
            }
            stream.Close();

            try
            {
                string appName = "ExtWatcher.Client";

                // Stop running instances of App
                foreach (var proc in Process.GetProcessesByName(appName))
                {
                    proc.Kill();
                }
            }
            catch
            {
                // Do nothing
            }
        }
    }
}
