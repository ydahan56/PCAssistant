using Nito.AsyncEx;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Sdk.Telegram
{
    public class AssistantBot : IPCAssistant
    {
        private readonly ITelegramBotClient _telegramAPI;
        private readonly CancellationTokenSource _telegramCts;
        private readonly IEnumerable<ChatId> _whitelist;

        public AssistantBot(string token, IEnumerable<long> whitelist)
        {
            this._telegramAPI = new TelegramBotClient(token);
            this._telegramCts = new CancellationTokenSource();
            this._whitelist = whitelist
                .Select(id => new ChatId(id))
                .ToList();
        }

        public async Task<User> GetMeAsync()
        {
            var me = await this._telegramAPI.GetMeAsync();
            return me;
        }

        public async Task SendTextToWhitelistAsync(string text)
        {
            foreach (var chatId in this._whitelist)
            {
                await this._telegramAPI.SendTextMessageAsync(chatId, text, parseMode: ParseMode.Markdown);
            }
        }

        public async Task SendPhotoToWhitelistAsync(Stream stream, string fileName)
        {
            var mediaInput = InputFile.FromStream(stream, fileName);

            foreach (var chatId in this._whitelist)
            {
                await this._telegramAPI.SendPhotoAsync(chatId, mediaInput);
            }
        }

        public async Task SendDocumentToWhitelistAsync(Stream stream, string fileName)
        {
            var documentInput = InputFile.FromStream(stream, fileName);
            
            foreach (var chatId in this._whitelist)
            {
                await this._telegramAPI.SendDocumentAsync(chatId, documentInput);
            }
        }

        public void Cancel()
        {
            this._telegramCts.Cancel();
        }

        public void StartReceiving(IUpdateHandler update)
        {
            var options = new ReceiverOptions() {
                AllowedUpdates = new UpdateType[] { UpdateType.Message }
            };

            this._telegramAPI.StartReceiving(update, options, this._telegramCts.Token);
        }
    }
}
