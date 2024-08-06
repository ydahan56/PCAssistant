using Hardware;
using FluentScheduler;
using Sdk.Telegram;
using Nito.AsyncEx;
using Sdk;
using Telegram.Bot.Types;

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

            this.Schedule(this.StartEventListener).ToRunOnceIn(5).Seconds();
            this.Schedule(this.AddBotNicknameToTrayTitle).ToRunOnceIn(5).Seconds();
            this.Schedule(this.SendBotClientHelloToAdmin).ToRunOnceIn(5).Seconds();
        }

        private void StartEventListener()
        {
            var update = new AgentUpdateHandler(this._tray, this._client);
            this._client.StartReceiving(update);
        }

        private void AddBotNicknameToTrayTitle()
        {
            AsyncContext.Run(async () =>
            {
                User user = await this._client.GetMeAsync();
                Program.UIThread.Invoke(() => this._tray.Text += $" - {user.Username}");
            });
        }

        private void SendBotClientHelloToAdmin()
        {
            AsyncContext.Run(async () =>
            {
                string trayTitle = "";
                Program.UIThread.Invoke(() => trayTitle = this._tray.Text);
                await this._client.SendTextToWhitelistAsync($"*{trayTitle}*: I'm Up.");
            });
        }
    }
}
