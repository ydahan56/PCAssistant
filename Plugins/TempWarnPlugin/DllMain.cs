using Components.Base;
using DotNetEnv;
using Sdk.Contracts;
using Sdk.Extensions;
using Sdk.Jobs;
using Sdk.Models;
using SimpleInjector;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TempWarnPlugin.Jobs;
using TempWarnPlugin.Models;

namespace Plugins.TempWarn
{
    public class DllMain : IModule, IJobStatus
    {
        private IWorker<TempArgs> worker;
        private Dictionary<string, Action> _stateAction;

        public DllMain()
        {
            Name = "/tempmon";
            Arguments = "(on|off)";
            HasArguments = true;
            Description = "Turn on or off the temperature monitor.";
        }

        public override async void Execute(ExecuteData executionData)
        {
            string state = executionData.Arguments[1].Value;

            await BotClient.SendTextMessageAsync(
                ChatId,
                $"Temperature monitor has turned {state}.",
                replyToMessageId: executionData.FromMessageId);

            bool success = _stateAction.TryGetValue(state, out var action);

            if (success)
            {
                action();
            }
        }

        private async void UpdateHandler(object sender, TempArgs e)
        {
            await BotClient.SendTextMessageAsync(
                ChatId,
                $"*[WARNING] {e.DeviceName}*: {e.Temperature}°C\nFrom *Telebot*",
                parseMode: ParseMode.Markdown
            );
        }

        private async void FeedbackHandler(object sender, Feedback e)
        {
            await BotClient.SendTextMessageAsync(ChatId, e.Text);
        }

        public override void Initialize(ModuleData moduleData)
        {
            base.Initialize(moduleData);

            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string folderPath = Path.GetDirectoryName(assemblyPath);

            Env.Load($"{folderPath}\\.env");

            var container = (Container)moduleData.ServiceProvider.GetService(typeof(Container));

            var cpuInstances = container.GetAllInstances<IProcessor>();
            var gpuInstances = container.GetAllInstances<IDisplay>();

            var devicesInsstances = cpuInstances.Concat<IDevice>(gpuInstances);

            worker = new TempWarning(devicesInsstances)
            {
                Update = UpdateHandler,
                Feedback = FeedbackHandler
            };

            _stateAction = new()
            {
                { "on", worker.Start },
                { "off", worker.Stop }
            };
        }

        public async Task<string> GetStatus()
        {
            return $"*Temp Monitor*: {worker.Active.ToReadable()}";
        }
    }
}
