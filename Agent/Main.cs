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
        public Main(ITGBotClient client)
        {
            // init tray
            var tray = new NotifyIcon()
            {
                Icon = new Icon(".\\icon.ico"),
                Text = "PCAssistant",
                Visible = true
            };

            // create an instace of init job
            var startup_Job = new Startup(client, tray);

            // init startup and refresh job
            JobManager.Initialize(startup_Job, Cpuid64.Instance);
        }
    }

    internal class Startup : Registry
    {
        private readonly ITGBotClient client;
        private readonly NotifyIcon tray;

        public Startup(ITGBotClient client, NotifyIcon tray)
        {
            this.client = client;
            this.tray = tray;

            this.Schedule(this.StartEventListener).ToRunOnceIn(5).Seconds();
            this.Schedule(this.AddBotNicknameToTrayTitle).ToRunOnceIn(5).Seconds();
            this.Schedule(this.SendBotClientHelloToAdmin).ToRunOnceIn(5).Seconds();
        }

        private void StartEventListener()
        {
            var update = new MainUpdateHandler(this.tray);
            this.client.StartListen(update);
        }

        private void AddBotNicknameToTrayTitle()
        {
            var user = this.client.GetMe();
            this.tray.Text += $" - {user.Username}";
        }

        private void SendBotClientHelloToAdmin()
        {
            this.client.SendTextBackToAdmin($"*{this.tray.Text}*: I'm Up.");
        }
    }
}
