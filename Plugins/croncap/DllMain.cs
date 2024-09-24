using Sdk.Contracts;
using Sdk.Extensions;
using Sdk.Jobs;
using Sdk.Models;
using Telebot.Capture;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace croncap
{
    public class DllMain : IModule, IJobStatus
    {
        private IWorker<CaptureArgs> worker;

        public DllMain()
        {
            Name = "/captime";
            Arguments = "(off|(\\d+) (\\d+))";
            HasArguments = true;
            Description = "Schedules screen capture session.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            string state = executeData.Arguments[1].Value;

            if (state.Equals("off"))
            {
                await BotClient.SendTextMessageAsync(
                    ChatId, $"Screen capture has turned {state}.", replyToMessageId: executeData.FromMessageId
                );

                worker.Stop();
                return;
            }

            var args = state.Split(' ');

            int duration = Convert.ToInt32(args[0]);
            int interval = Convert.ToInt32(args[1]);

            string text = $"Screen capture has been scheduled to run {duration} sec for every {interval} sec.";

            await BotClient.SendTextMessageAsync(
                ChatId, text, replyToMessageId: executeData.FromMessageId
            );

            ((IScheduled)worker).Start(duration, interval);
        }

        private async void UpdateHandler(object sender, CaptureArgs e)
        {
            var raw = new InputOnlineFile(e.Capture.ToMemStream(), "preview.jpg");
            await BotClient.SendDocumentAsync(ChatId, raw);
        }

        private async void FeedbackHandler(object sender, Feedback e)
        {
            await BotClient.SendTextMessageAsync(ChatId, e.Text);
        }

        public override void Initialize(ModuleData moduleData)
        {
            base.Initialize(moduleData);

            worker = new CaptureSchedule
            {
                Update = UpdateHandler,
                Feedback = FeedbackHandler
            };
        }

        public async Task<string> GetStatus()
        {
            return $"*Cap Time*: {worker.Active.ToReadable()}";
        }
    }
}
