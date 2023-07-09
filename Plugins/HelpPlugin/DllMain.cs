using Sdk.Contracts;
using Sdk.Models;
using SimpleInjector;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Plugins.Help
{
    public class DllMain : IModule
    {
        private string text;

        public DllMain()
        {
            Name = "/help";
            Description = "List of available plugins.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            await BotClient.SendTextMessageAsync(
                ChatId, text, parseMode: ParseMode.Markdown, replyToMessageId: executeData.FromMessageId);
        }

        public override void Initialize(ModuleData moduleData)
        {
            base.Initialize(moduleData);

            var container = (Container)moduleData.ServiceProvider.GetService(typeof(Container));
            var modules = container.GetAllInstances<IModule>();

            StringBuilder builder = new();
            foreach (IModule module in modules)
            {
                builder.AppendLine(module.ToString());
            }

            text = builder.ToString();
        }
    }
}
