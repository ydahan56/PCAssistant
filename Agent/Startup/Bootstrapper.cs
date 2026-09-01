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

        public Bootstrapper(IPCAssistant client, IMessageHub hub, INotificationHandler tray)
        {
            this._client = client;
            this._tray = tray;

            this.Schedule(this.UpdateTrayCaption).ToRunOnceIn(2).Seconds();
            this.Schedule(this.NotifyClientHello).ToRunOnceIn(5).Seconds();

            hub.Subscribe<ApplicationEvent>(this.ApplicationEventHandler);
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

        private void NotifyClientHello()
        {
            var whitelist = Env.GetString("whitelist")
                .Split(",")
                .Select(id =>
                {
                    if (string.IsNullOrWhiteSpace(id))
                        return new ChatId(0);

                    var parsed = Convert.ToInt64(id);
                    var chat = new ChatId(id);

                    return chat;
                })
                .ToList();

            foreach (ChatId chatId in whitelist) // todo - replace with an event
            {
                AsyncContext.Run(
                    async () => await this._client.SendMessage(
                        chatId,
                        $"*{this._tray.GetTitle()}*: I'm Up.",
                        parseMode: ParseMode.Markdown
                    )
                );
            }
        }
    }
}
