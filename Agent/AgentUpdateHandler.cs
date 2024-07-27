using CommandLine;
using DotNetEnv;
using FluentScheduler;
using Nito.AsyncEx;
using Sdk;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Models;
using Sdk.Telegram;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Agent
{
    public class AgentUpdateHandler : IUpdateHandler
    {
        private readonly NotifyIcon _tray;
        private readonly IPCAssistant _assistant;

        private readonly List<long> _whitelist;
        private readonly List<Plugin> _commands;

        public AgentUpdateHandler(NotifyIcon tray, IPCAssistant assistant)
        {
            this._tray = tray;
            this._assistant = assistant;

            this._whitelist = Env.GetString("whitelist")
                .Split(',')
                .Select(id => Convert.ToInt64(id))
                .ToList();

            this._commands = Program.Services.ResolveInstances<IPlugin>()
                .Cast<Plugin>()
                .ToList();
        }

        public async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            await System.IO.File.AppendAllTextAsync(
                PCManager.Combine("log.txt"), 
                exception.ToString() + Environment.NewLine
            );
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;

            //return; // debug purpose

            if (!this._whitelist.Contains(message.From.Id))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Unauthorized.");

                return;
            }

            if (string.IsNullOrWhiteSpace(message.Text))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Unrecognized command.", replyToMessageId: message.MessageId);
                
                return;
            }

            var tipText = $"Received {message.Text} from {message.From.Username}.";

            // show balloon tip to the user
            this._tray.ShowBalloonTip(1750, this._tray.Text, tipText, ToolTipIcon.Info);


            // read args from user
            var args = message.Text.SplitArgs();

            // search for command
            var worker = this._commands.SingleOrDefault(c => c.TryGetPlugin(args).success);
            
            // notify user incase no command was found
            if (worker == null)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "No such command.");

                return;
            }

            // set callback for the command
            worker.SetExecuteResultCallback(this.ExecuteResultCallback);

            // execute command on a separate thread, "fire and forget"
            JobManager.Initialize(worker);
        }

        private async void ExecuteResultCallback(ExecuteResult result)
        {
            // show balloon tip to the user
            this._tray.ShowBalloonTip(1750, this._tray.Text, result.ErrorMessage, ToolTipIcon.Info);

            // send result to the user
            await this._assistant.SendTextToWhitelistAsync(result.ErrorMessage);
        }
    }
}
