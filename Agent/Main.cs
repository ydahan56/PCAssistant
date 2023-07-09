using Hardware;
using FluentScheduler;
using Sdk.Clients;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Agent
{
    public class Main : ApplicationContext
    {
        public Main(ITGBotClient client, CancellationTokenSource cts)
        {
            var tray = new NotifyIcon()
            {
                Icon = new Icon(".\\icon.ico"),
                Text = "Telebot"
            };

            // show tray icon
            tray.Visible = true;

            // create an instace of init job
            var startup_Job = new Startup(client, cts, tray);

            // init startup job
            JobManager.Initialize(startup_Job);

            // init refresh thread
            JobManager.Initialize(Cpuid64.Instance);
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
            var updateHandler = new MainUpdateHandler(this.tray);
            var recvOptions = new ReceiverOptions()
            {
                AllowedUpdates = new UpdateType[] { UpdateType.Message }
            };

            this._client.StartReceiving(updateHandler, recvOptions, this.cts);
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
