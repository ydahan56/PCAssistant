using Agent.Notification;
using DotNetEnv;
using Easy.MessageHub;
using FluentScheduler;
using Nito.AsyncEx;
using Sdk.Hub;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agent.Startup
{
    internal class Bootstrapper : Registry, IBootstrapper
    {
        private readonly IPCAssistant _client;
        private readonly INotificationHandler _tray;

        private event Func<Task> ClientHelloEvent;

        public Bootstrapper(IPCAssistant client, IMessageHub hub, INotificationHandler tray)
        {
            this._client = client;
            this._tray = tray;

            this.Schedule(this.UpdateTrayCaption).ToRunOnceIn(2).Seconds();
            this.Schedule(this.SendClientHello).ToRunOnceIn(5).Seconds();

            hub.Subscribe<ApplicationEvent>(this.ApplicationEventHandler);

            foreach (var chatid in Env.GetString("whitelist").Split(","))
            {
                if (String.IsNullOrWhiteSpace(chatid))
                    continue;

                var whiteid = new WhiteClient(this._client, this._tray, Convert.ToInt64(chatid));
                this.ClientHelloEvent += whiteid.SendClientHello;
            }
        }

        public Registry GeInstance()
        {
            return this;
        }

        private void ApplicationEventHandler(ApplicationEvent eventType)
        {
            switch (eventType)
            {
                case ApplicationEvent.Exit:
                    Application.Exit();
                    break;
                case ApplicationEvent.Restart:
                    Application.Restart();
                    break;
            }
        }

        private void UpdateTrayCaption()
        {
            // get current user
            var user = AsyncContext.Run(async () => await this._client.GetMe());

            // update tray label
            this._tray.SetTitle(user.Username);
        }

        private void SendClientHello()
        {
            this.ClientHelloEvent();
        }
    }

    public interface IWhiteClient
    {
        Task SendClientHello();
    }

    public class WhiteClient : IWhiteClient
    {
        private readonly long _chatId;
        private readonly IPCAssistant _client;
        private readonly INotificationHandler _tray;

        public WhiteClient(IPCAssistant client, INotificationHandler tray, long chatid)
        {
            this._client = client;
            this._tray = tray;
            this._chatId = chatid;
        }
        public async Task SendClientHello()
        {
            await this._client.SendMessage(
                this._chatId, $"*{this._tray.GetTitle()}*: I'm Up.", parseMode: ParseMode.Markdown);
        }
    }
}
