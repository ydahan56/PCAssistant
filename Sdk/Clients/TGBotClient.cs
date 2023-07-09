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
        private readonly long _chat_Id;

        public TGBotClient(string token, long chat_Id)
        {
            this._client = new TelegramBotClient(token);
            this._chat_Id = chat_Id;
        }

        public User GetMe()
        {
            return AsyncContext.Run(async () => await this._client.GetMeAsync());
        }

        public void SendPhoto(Stream stream, string fileName)
        {
            var media = InputFile.FromStream(stream, fileName);
            AsyncContext.Run(async () => await this._client.SendPhotoAsync(this._chat_Id, media));
        }

        public void SendDocument(Stream stream, string fileName)
        {
            var file = InputFile.FromStream(stream, fileName);
            AsyncContext.Run(async () => await this._client.SendDocumentAsync(this._chat_Id, file));
        }

        public void SendText(string text)
        {
            AsyncContext.Run(async () => await this._client.SendTextMessageAsync(this._chat_Id, text, parseMode: ParseMode.Markdown));
        }

        public void StartReceiving(IUpdateHandler updateHandler, ReceiverOptions recvOptions, CancellationTokenSource cts)
        {
            this._client.StartReceiving(updateHandler, recvOptions, cts.Token);
        }
    }
}
