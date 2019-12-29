using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;

namespace ExtWatcher.Client
{
    internal class TrayMenu
    {
        private readonly NotifyIcon _notifyIcon;

        public TrayMenu()
        {
            var contextMenu = new ContextMenu();
            var menuExitItem = new MenuItem();

            contextMenu.MenuItems.AddRange(new MenuItem[] { menuExitItem });
            menuExitItem.Index = 0;
            menuExitItem.Text = "Stop";
            menuExitItem.Click += new System.EventHandler(this.menuExitItem_Click);

            _notifyIcon = new NotifyIcon();
            _notifyIcon.ContextMenu = contextMenu;
            _notifyIcon.Text = "ExtWatcher";
            _notifyIcon.Visible = true;

            // Extracts your app's icon and uses it as notify icon
            _notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            //// Hides the icon when the notification is closed
            //_notifyIcon.BalloonTipClosed += (s, e) => _notifyIcon.Visible = false;
        }

        private void menuExitItem_Click(object sender, EventArgs e)
        {
            string serviceName = "ExtWatcherService";
            var sc = new ServiceController(serviceName);
            if (sc.Status != ServiceControllerStatus.Stopped)
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                sc.Close();
            }

            _notifyIcon.ShowBalloonTip(3000, "ExtWatcher Stopped", "You are on your own :(", ToolTipIcon.Warning);
            System.Windows.Application.Current.Shutdown();      // Calls Application_Exit()
        }

        public void ShowNotification(string Info)
        {
            // Shows a notification with specified message and title
            _notifyIcon.ShowBalloonTip(3000, "Title", Info, ToolTipIcon.Info);
        }
    }
}
