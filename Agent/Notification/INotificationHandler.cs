namespace Agent.Notification
{
    public interface INotificationHandler
    {
        void ShowMessage(string message);
        void SetTitle(string title);

        String GetTitle();
    }
}
