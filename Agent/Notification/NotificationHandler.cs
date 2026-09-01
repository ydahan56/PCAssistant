using Sdk;

namespace Agent.Notification
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly NotifyIcon _tray;
        public NotificationHandler()
        {
            this._tray = new NotifyIcon()
            {
                Icon = new Icon(PCManager.Combine("icon.ico")),
                Text = "PCAssistant",
                Visible = true
            };
        }

        public string GetTitle()
        {
            return this._tray.Text;
        }

        public void SetTitle(string title)
        {
            this._tray.Text += $" - {title}";
        }

        public void ShowMessage(string message)
        {
            this._tray.ShowBalloonTip(3000, this._tray.Text, message, ToolTipIcon.Info);
        }
    }
}
