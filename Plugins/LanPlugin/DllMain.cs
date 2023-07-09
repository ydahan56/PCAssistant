using LanPlugin.Intranet;
using Sdk.Contracts;
using Sdk.Extensions;
using Sdk.Models;
using System.Text;
using Telegram.Bot;

namespace Plugins.Lan
{
    public class DllMain : IModule, IJobStatus
    {
        private IInetScanner lanScanner;
        private IINetMonitor lanMonitor;

        private Dictionary<string, Action> actions;

        public DllMain()
        {
            Name = "/lan";
            Arguments = "(mon|moff|scan)";
            HasArguments = true;
            Description = "Scan or listen for devices on the LAN.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            string state = executeData.Arguments[1].Value;

            await BotClient.SendTextMessageAsync(
                ChatId, $"Lan triggered to {state}.", replyToMessageId: executeData.FromMessageId);

            actions[state].Invoke();
        }

        private async void HostHandler(object sender, HostsArg e)
        {
            StringBuilder builder = new();

            foreach (Host host in e.Hosts)
            {
                builder.AppendLine(host.ToString());
                builder.AppendLine();
                builder.AppendLine();
            }

            await BotClient.SendTextMessageAsync(ChatId, builder.ToString());
        }

        private async void FeedbackHandler(object sender, Feedback e)
        {
            await BotClient.SendTextMessageAsync(ChatId, e.Text);
        }

        public override void Initialize(ModuleData moduleData)
        {
            base.Initialize(moduleData);

            lanScanner = new LanScanner
            {
                Discovered = HostHandler,
                Feedback = FeedbackHandler
            };

            lanMonitor = new LanMonitor
            {
                Connected = HostHandler,
                Disconnected = HostHandler,
                Feedback = FeedbackHandler
            };

            actions = new Dictionary<string, Action>
            {
                { "mon", lanMonitor.Listen },
                { "moff", lanMonitor.Disconnect },
                { "scan", lanScanner.Discover }
            };
        }

        public async Task<string> GetStatus()
        {
            return $"*LAN Monitor*: {lanMonitor.Active.ToReadable()}";
        }
    }
}
