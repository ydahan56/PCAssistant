using Agent.Notification;
using Common.Queue;
using Sdk.Models;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agent
{
    public class ExecutionMessageQueue : SimpleMessageQueue<ExecuteContext>
    {
        private readonly IPCAssistant _client;
        private readonly INotificationHandler _tray;

        public ExecutionMessageQueue(IPCAssistant client, INotificationHandler tray)
        {
            this._client = client;
            this._tray = tray;
        }

        protected override async void HandleMessage(ExecuteContext message)
        {
            await message.SendPackage(_client);
        }
    }
}
