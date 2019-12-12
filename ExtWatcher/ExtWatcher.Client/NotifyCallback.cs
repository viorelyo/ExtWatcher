using System;
using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Utils;

namespace ExtWatcher.Client
{
    public class NotifyCallback : INotifyCallback
    {
        private ToastNotification _toast;

        public NotifyCallback()
        {
            _toast = new ToastNotification();
        }

        public void OnFileCreatedEvent(FileEventArgs e)
        {
            Logger.WriteToLog(String.Format("FileCreatedEvent fired. Info: '{0}', '{1}'", e.Folder, e.FileName));

            Console.WriteLine(e.Id);
            Console.WriteLine(e.Folder);
            Console.WriteLine(e.FileName);
            Console.WriteLine(e.Date);

            _toast.ShowNotification(e.FileName + " " + e.Date);
        }
    }
}
