using Hardware;
using FluentScheduler;
using Sdk.Clients;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using System.Reflection;

namespace Agent
{
    public class Main : ApplicationContext
    {
        public Main(ITGBotClient client, CancellationTokenSource cts)
        {
            // init tray
            var tray = new NotifyIcon()
            {
                Icon = new Icon(".\\icon.ico"),
                Text = "PCAssistant",
                Visible = true
            };

            // create an instace of init job
            var startup_Job = new Startup(client, cts, tray);

            // init startup and refresh job
            JobManager.Initialize(startup_Job, Cpuid64.Instance);
        }
    }

    internal class Startup : Registry
    {
        private readonly ITGBotClient _client;
        private readonly CancellationTokenSource cts;
        private readonly NotifyIcon tray;

        public Startup(ITGBotClient client, CancellationTokenSource cts, NotifyIcon tray)
        {
            this._client = client;
            this.cts = cts;

            this.tray = tray;

            this.Schedule(this.StartEventListener).ToRunOnceIn(5).Seconds();
            this.Schedule(this.AddBotNicknameToTrayTitle).ToRunOnceIn(5).Seconds();
            this.Schedule(this.SendBotClientHelloToAdmin).ToRunOnceIn(5).Seconds();
        }

        private void StartEventListener()
        {
            var update = new MainUpdateHandler(this.tray);
            var options = new ReceiverOptions()
            {
                AllowedUpdates = new UpdateType[] { UpdateType.Message }
            };

            this._client.StartReceiving(update, options, this.cts);
        }

        private void AddBotNicknameToTrayTitle()
        {
            var user = this._client.GetMe();
            this.tray.Text += $" - {user.Username}";
        }

        private void SendBotClientHelloToAdmin()
        {
            this._client.SendText($"*{this.tray.Text}*: I'm Up.");
        }
    }
}
