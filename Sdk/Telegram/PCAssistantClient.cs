using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Sdk.Telegram
{
    public class PCAssistantClient : TelegramBotClient, IPCAssistant
    {
        private readonly CancellationTokenSource _cancel;
     
        public PCAssistantClient(string token) : base(token)
        {
            this._cancel = new CancellationTokenSource();
        }

        public void Cancel()
        {
            this._cancel.Cancel();
        }

        public void StartReceiving(IUpdateHandler update)
        {
            var options = new ReceiverOptions()
            {
                AllowedUpdates = new UpdateType[] { UpdateType.Message }
            };

            this.StartReceiving(update, options, this._cancel.Token);
        }
    }
}
