﻿using Microsoft.Win32;
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

            FileStream stream = new FileStream(@"C:\install\install.log", FileMode.Append, FileAccess.Write);
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("INSTALLING...\n");
            }
            stream.Close();
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            try
            {
                string appLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ExtWatcher.Client.exe";

                FileStream stream = new FileStream(@"C:\install\install.log", FileMode.Append, FileAccess.Write);
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("COMMITING...\n");
                    writer.WriteLine(appLocation);
                }
                stream.Close();

                // Set RegistryKey to run app at windows startup
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("ExtWatcher.Client", appLocation);

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

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            try
            {
                string appName = "ExtWatcher.Client";

                // Stop running instances of App
                foreach (var proc in Process.GetProcessesByName(appName))
                {
                    proc.Kill();
                }

                // Remove from registry key
                string keyName = @"Software\Microsoft\Windows\CurrentVersion\Run";
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(appName);
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }
    }
}
