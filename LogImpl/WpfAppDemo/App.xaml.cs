using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string quarantineFolderPath = @"C:\ExtWatcher\Quarantine";

        private void ShowNotification()
        {
            Toast toast = new Toast();
            Thread.Sleep(2000);
            Console.WriteLine("hello");
            toast.ShowNotification();
        }

        public App()
        {
            //CreateQuarantineFolder();
            RemoveQuarantineFolder();
        }

        private void RemoveQuarantineFolder()
        {
            string adminUserName = Environment.UserName;
            DirectorySecurity ds = Directory.GetAccessControl(quarantineFolderPath);
            FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            ds.RemoveAccessRule(fsa);
            Directory.SetAccessControl(quarantineFolderPath, ds);

            Directory.Delete(quarantineFolderPath, true);
        }

        private void CreateQuarantineFolder()
        {
            Directory.CreateDirectory(quarantineFolderPath);

            string adminUserName = Environment.UserName;
            DirectorySecurity ds = Directory.GetAccessControl(quarantineFolderPath);
            FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            ds.AddAccessRule(fsa);
            Directory.SetAccessControl(quarantineFolderPath, ds);
        }
    }
}
