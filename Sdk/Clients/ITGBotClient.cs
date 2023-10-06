using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Sdk.Clients
{
    public interface ITGBotClient
    {
        User GetMe();

        void SendTextBackToAdmin(string text);
        void SendPhotoBackToAdmin(Stream stream, string fileName);
        void SendDocumentBackToAdmin(Stream stream, string fileName);

        void StopListen();
        void StartListen(IUpdateHandler updateHandler);
    }
}
