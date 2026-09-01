using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Sdk.Telegram
{
    public interface IPCAssistant : ITelegramBotClient
    {
        void Cancel();
        void StartReceiving(IUpdateHandler update);
    }
}
