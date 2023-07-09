using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Sdk.Clients
{
    public interface ITGBotClient
    {
        User GetMe();
        void SendText(string text);
        void SendPhoto(Stream stream, string fileName);
        void SendDocument(Stream stream, string fileName);
        void StartReceiving(IUpdateHandler updateHandler, ReceiverOptions options, CancellationTokenSource cts);
    }
}
