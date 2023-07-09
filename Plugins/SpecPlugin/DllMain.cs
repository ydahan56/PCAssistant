using Sdk.Contracts;
using Sdk.Models;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace Plugins.NSSpec
{
    public class DllMain : IModule
    {
        private const string filePath = "..\\Plugins\\Spec\\Debug\\net6.0\\spec.txt";

        private Spec spec;

        public DllMain()
        {
            Name = "/spec";
            HasArguments = false;
            Description = "Get full hardware information.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            string info = spec.GetInfo();

            File.WriteAllText(filePath, info);

            var fs = new FileStream(filePath, FileMode.Open);
            var raw = new InputOnlineFile(fs, fs.Name);

            await BotClient.SendDocumentAsync(ChatId, raw, replyToMessageId: executeData.FromMessageId);
        }

        public override void Initialize(ModuleData moduleData)
        {
            base.Initialize(moduleData);
            spec = new Spec(moduleData.ServiceProvider);
        }
    }
}
