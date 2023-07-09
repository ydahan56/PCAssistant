using DisplayPlugin.Core;
using DisplayPlugin.Enums;
using Sdk.Contracts;
using Sdk.Models;
using Telegram.Bot;

namespace Plugins.Display
{
    public class DllMain : IModule
    {
        private readonly Dictionary<string, DisplayState> states;

        public DllMain()
        {
            Name = "/screen";
            Arguments = "(on|off)";
            HasArguments = true;
            Description = "Turn off or on the monitor.";

            states = new Dictionary<string, DisplayState>()
            {
                { "on", DisplayState.DisplayStateOn },
                { "off", DisplayState.DisplayStateOff }
            };
        }

        public override async void Execute(ExecuteData executeData)
        {
            string key = executeData.Arguments[1].Value;

            await BotClient.SendTextMessageAsync(
                ChatId, $"Successfully turned {key} the monitor.", replyToMessageId: executeData.FromMessageId
            );

            DisplayState state = states[key];

            var api = new DisplayApi(state);
            api.Invoke();
        }
    }
}
