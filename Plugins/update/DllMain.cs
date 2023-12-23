using FluentScheduler;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Hub;
using Sdk.Models;
using Telegram.Bot;

namespace Plugins.Update
{
    public class DllMain : PluginBase
    {
        private readonly Dictionary<string, Func<int, Task>> _stateActions;

        private const string castUrl = "https://raw.githubusercontent.com/yoni56/PCAssistant/master/update.xml";

        public DllMain()
        {
            base.Name = "/update";
            base.ArgsPattern = "(chk|dl)";
            base.Description = "Check or download an update.";

            this._stateActions = new Dictionary<string, Func<int, Task>>()
            {
                { "chk", CheckAction },
                { "dl", DownloadAction }
            };
        }

        private async void UpdateCheck(UpdateInfoEventArgs e)
        {
            if (e.Error is null)
            {
                if (e.IsUpdateAvailable)
                {
                    // Save it for later incase we want to download the update
                    _updateInfoEventArgs = e;

                    await BotClient.SendTextMessageAsync(ChatId, $"A new version {e.CurrentVersion} of Telebot is available!");
                    return;
                }

                await BotClient.SendTextMessageAsync(ChatId, "You're currently running the latest version.");
            }
        }

        public override void Dispatch(DispatchData data)
        {
            string state = data.Arguments[1].Value;
            bool success = _stateActions.TryGetValue(state, out var action);

            if (success)
            {
                await action(executeData.FromMessageId);
            }
        }

        private async Task CheckAction(int id)
        {
            await BotClient.SendTextMessageAsync(
              ChatId, "Checking for updates...", replyToMessageId: id);

            AutoUpdater.Start();
        }

        private async Task DownloadAction(int id)
        {
            if (_updateInfoEventArgs is null)
            {
                await BotClient.SendTextMessageAsync(
                    ChatId, "Update args is null, please check for update before.", replyToMessageId: id);
                return;
            }

            await BotClient.SendTextMessageAsync(
                ChatId, "Telebot is updating...", replyToMessageId: id);

            bool success = AutoUpdater.DownloadUpdate(_updateInfoEventArgs);
            if (success)
            {
                Mediator.Instance.hub.Publish(AppEventType.Exit);
            }
        }

        public override void Initialize(ModuleData moduleData)
        {
            base.Initialize(moduleData);
            JobManager.Initialize(_updateJob);
        }
    }
}
