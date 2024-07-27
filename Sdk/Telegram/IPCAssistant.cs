using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Sdk.Telegram
{
    public interface IPCAssistant
    {
        Task<User> GetMeAsync();

        Task SendTextToWhitelistAsync(string text);
        Task SendPhotoToWhitelistAsync(Stream stream, string fileName);
        Task SendDocumentToWhitelistAsync(Stream stream, string fileName);

        void Cancel();
        void StartReceiving(IUpdateHandler update);
    }
}
