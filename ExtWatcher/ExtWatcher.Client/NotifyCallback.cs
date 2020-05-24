using System;
using System.IO;
using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Utils;

namespace ExtWatcher.Client
{
    public class NotifyCallback : INotifyCallback
    {
        private TrayMenu _trayMenu;

        public NotifyCallback()
        {
        }
        public void InitTrayMenu()
        {
            _trayMenu = new TrayMenu();
        }

        public void OnFileCreatedEvent(FileEventArgs e)
        {
            Logger.WriteToLog(String.Format("FileCreatedEvent fired. Info: '{0}', '{1}'", e.Folder, e.FileName));

            //Console.WriteLine(e.Id);
            //Console.WriteLine(e.Folder);
            //Console.WriteLine(e.FileName);
            //Console.WriteLine(e.Date);

            _trayMenu.ShowNotification(Path.GetFileName(e.FileName));
        }
    }
}
