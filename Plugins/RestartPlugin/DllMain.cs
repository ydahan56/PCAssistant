using Sdk.Contracts;
using Sdk.Hub;
using Sdk.Models;
using Telegram.Bot;

namespace Plugins.Restart
{
    public class DllMain : IModule
    {
        public DllMain()
        {
            Name = "/restart";
            HasArguments = false;
            Description = "Restart Telebot.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            await BotClient.SendTextMessageAsync(
                ChatId, "Telebot is restarting...", replyToMessageId: executeData.FromMessageId);

            await Task.Delay(2000).ContinueWith((t) =>
            {
                Mediator.Instance.hub.Publish(AppEventType.Restart);
            });
        }
    }
}
