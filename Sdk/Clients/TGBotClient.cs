using Nito.AsyncEx;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Sdk.Clients
{
    public class TGBotClient : ITGBotClient
    {
        private readonly ITelegramBotClient _client;
        private readonly CancellationTokenSource _cancel;

        private readonly ChatId _chatId;

        public TGBotClient(string token, long chatId)
        {
            this._client = new TelegramBotClient(token);
            this._cancel = new CancellationTokenSource();

            this._chatId = new ChatId(chatId);
        }

        public User GetMe()
        {
            return AsyncContext.Run(async () => await this._client.GetMeAsync());
        }

        public void SendPhotoBackToAdmin(Stream stream, string fileName)
        {
            var media = InputFile.FromStream(stream, fileName);
            AsyncContext.Run(async () => await this._client.SendPhotoAsync(this._chatId, media));
        }

        public void SendDocumentBackToAdmin(Stream stream, string fileName)
        {
            var file = InputFile.FromStream(stream, fileName);
            AsyncContext.Run(async () => await this._client.SendDocumentAsync(this._chatId, file));
        }

        public void SendTextBackToAdmin(string text)
        {
            AsyncContext.Run(async () => await this._client.SendTextMessageAsync(this._chatId, text, parseMode: ParseMode.Markdown));
        }

        public void StopListen()
        {
            this._cancel.Cancel();
        }

        public void StartListen(IUpdateHandler updateHandler)
        {
            var options = new ReceiverOptions() {
                AllowedUpdates = new UpdateType[] { UpdateType.Message }
            };

            this._client.StartReceiving(updateHandler, options, this._cancel.Token);
        }
    }
}
