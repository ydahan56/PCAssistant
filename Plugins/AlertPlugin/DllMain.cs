using Sdk.Contracts;
using Sdk.Models;
using Telegram.Bot;

namespace Plugins.Alert
{
    public class DllMain : IModule
    {
        public DllMain()
        {
            Name = "/alert";
            Arguments = "\"(.+?)\"";
            HasArguments = true;
            Description = "Display an alert.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            string text = executeData.Arguments[1].Value;

            await BotClient.SendTextMessageAsync(
                ChatId, "Alert has been displayed.", replyToMessageId: executeData.FromMessageId
            );

            MessageBox.Show(
                text,
                "Telebot",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );
        }
    }
}
