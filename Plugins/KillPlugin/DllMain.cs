using Sdk.Contracts;
using Sdk.Models;
using System.Diagnostics;
using Telegram.Bot;

namespace Plugins.Kill
{
    public class DllMain : IModule
    {
        public DllMain()
        {
            Name = "/kill";
            Arguments = "(\\d+)";
            HasArguments = true;
            Description = "Kill a task with the specified pid.";
        }

        public override async void Execute(ExecuteData executeData)
        {
            int pid = Convert.ToInt32(executeData.Arguments[1].Value);

            Process target;
            string text;

            try
            {
                target = Process.GetProcessById(pid);
            }
            catch (Exception e)
            {
                await BotClient.SendTextMessageAsync(
                    ChatId, e.Message, replyToMessageId: executeData.FromMessageId);
                return;
            }

            try
            {
                text = $"{target.ProcessName} killed.";
                target.Kill();
            }
            catch (Exception e)
            {
                text = e.Message;
            }

            await BotClient.SendTextMessageAsync(ChatId, text, replyToMessageId: executeData.FromMessageId);
        }
    }
}
