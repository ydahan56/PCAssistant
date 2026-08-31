using DotNetEnv;
using FluentScheduler;
using Hardware;
using Microsoft.VisualBasic.ApplicationServices;
using Nito.AsyncEx;
using Sdk;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agent
{
    public class Main : ApplicationContext
    {
        public Main(IPCAssistant client)
        {
            // init tray
            var tray = new NotifyIcon()
            {
                Icon = new Icon(PCManager.Combine("icon.ico")),
                Text = "PCAssistant",
                Visible = true
            };

            // create an instace of init job
            var startup = new Startup(client, tray);

            // init startup and refresh job
            JobManager.Initialize(startup, Cpuid64.Instance.GetRefreshJob());
        }
    }

    internal class Startup : Registry
    {
        private readonly IPCAssistant _client;
        private readonly NotifyIcon _tray;

        public Startup(IPCAssistant client, NotifyIcon tray)
        {
            this._client = client;
            this._tray = tray;

            this.Schedule(this.StartClientListen).ToRunOnceIn(5).Seconds();
            this.Schedule(this.UpdateTrayCaption).ToRunOnceIn(2).Seconds();
            //this.Schedule(this.NotifyClientHello).ToRunOnceIn(5).Seconds();
        }

        private void StartClientListen()
        {
            var update = new AgentUpdateHandler(this._tray, this._client);
            this._client.StartReceiving(update);
        }

        private void UpdateTrayCaption()
        {
            // get current user
            var user = AsyncContext.Run(
                async () => await this._client.GetMeAsync()
            );

            // update tray label
            this._tray.Text += $" - {user.Username}";
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
                    async () => await this._client.SendTextMessageAsync(
                        chatId,
                        $"*{this._tray.Text}*: I'm Up.",
                        parseMode: ParseMode.Markdown
                    )
                );
            }
        }
    }
}
