using System;
using ExtWatcher.Common.Contract;

namespace ExtWatcher.Client
{
    public class NotifyCallback : INotifyCallback
    {
        public void OnFileCreatedEvent(FileEventArgs e)
        {
            Console.WriteLine(e.Id);
            Console.WriteLine(e.Folder);
            Console.WriteLine(e.FileName);
            Console.WriteLine(e.Date);

            ToastNotification toast = new ToastNotification();
            toast.ShowNotification(e.FileName + " " + e.Date);
        }
    }
}
