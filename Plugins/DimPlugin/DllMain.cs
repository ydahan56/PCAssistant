using DimPlugin.Core;
using Sdk.Contracts;
using Sdk.Models;
using Telegram.Bot;

namespace Plugins.Dim
{
    public class DllMain : IModule
    {
        public DllMain()
        {
            Name = "/dim";
            Arguments = "(\\d{1,3})";
            HasArguments = true;
            Description = "Adjust workstation's brightness.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            byte brightness = Convert.ToByte(executeData.Arguments[1].Value);

            await BotClient.SendTextMessageAsync(
                ChatId, $"Successfully set brightness to {brightness}%.", replyToMessageId: executeData.FromMessageId
            );

            var api = new DimApi(brightness);
            api.Invoke();
        }
    }
}
