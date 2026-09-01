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
            if (message.ResultType == ExecuteResultType.Text)
            {
                this._tray.ShowMessage(message.ErrorMessage);
                await this._client.SendMessage(
                    message.ChatId, message.ErrorMessage, parseMode: ParseMode.Markdown);
            }
            else if (message.ResultType == ExecuteResultType.Document)
            {
                var document = message as DocumenContext;
                await this._client.SendDocument(
                    message.ChatId, InputFile.FromStream(document.Stream, document.FileName));
            }
            else if (message.ResultType == ExecuteResultType.Image)
            {
                var image = message as ImageContext;
                await this._client.SendPhoto(
                    message.ChatId, InputFile.FromStream(image.Stream, image.FileName));
            }
        }
    }
}
