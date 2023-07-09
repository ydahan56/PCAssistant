using Sdk.Contracts;
using Sdk.Models;
using Telegram.Bot;
using VolPlugin.Core;

namespace Plugins.Vol
{
    public class DllMain : IModule
    {
        public DllMain()
        {
            Name = "/vol";
            Arguments = "(\\d{1,3})";
            HasArguments = true;
            Description = "Adjust workstation's volume.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            if (!File.Exists("..\\Plugins\\Vol\\Debug\\net6.0-windows\\sndvol64.exe"))
            {
                await BotClient.SendTextMessageAsync(
                    ChatId, $"Cannot find file sndvol64.exe.", replyToMessageId: executeData.FromMessageId);
                return;
            }

            int vol = Convert.ToInt32(executeData.Arguments[1].Value);

            await BotClient.SendTextMessageAsync(
                ChatId, $"Successfully set volume to {vol}%.", replyToMessageId: executeData.FromMessageId);

            var api = new VolApi(vol);
            api.Invoke();
        }
    }
}
