using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Sdk.Telegram
{
    public interface IPCAssistant : ITelegramBotClient
    {
        void Cancel();
        void StartReceiving(IUpdateHandler update);
    }
}
