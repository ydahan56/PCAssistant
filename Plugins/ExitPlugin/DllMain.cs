namespace Plugins.Exit
{
    public class DllMain : IModule
    {
        public DllMain()
        {
            Name = "/exit";
            HasArguments = false;
            Description = "Shutdown Telebot.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            await BotClient.SendTextMessageAsync(
                ChatId, "Telebot is closing...", replyToMessageId: executeData.FromMessageId
            );

            await Task.Delay(2000).ContinueWith((t) =>
            {
                Mediator.Instance.hub.Publish(AppEventType.Exit);
            });
        }
    }
}
