using CommandLine;
using DotNetEnv;
using FluentScheduler;
using Sdk;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Models;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Agent
{
    public class AgentUpdateHandler : IUpdateHandler
    {
        private Update _update;
        private ITelegramBotClient _client;

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

            this._commands = Program.Services
                .ResolveInstances<IPlugin>()
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
            this._update = update;
            this._client = client;

            if (!this._whitelist.Contains(update.Message.From.Id))
            {
                await client.SendTextMessageAsync(update.Message.Chat.Id, "Unauthorized.");

                return;
            }

            if (string.IsNullOrWhiteSpace(update.Message.Text))
            {
                await client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    "Unrecognized command.",
                    replyToMessageId: update.Message.MessageId
                );

                return;
            }

            var tipText = $"Received {update.Message.Text} from {update.Message.From.Username}.";

            // show balloon tip to the user
            this._tray.ShowBalloonTip(1750, this._tray.Text, tipText, ToolTipIcon.Info);

            // read args from user
            var args = update.Message.Text.SplitArgs();

            Parser.Default.ParseArguments(args, this._commands.Select(x => x.GetType()).ToArray())
                .WithParsed<Plugin>((o) =>
                {
                    // set callback for the command
                    o.SetExecuteResultCallback(this.ExecuteResultCallback);

                    // execute command on a separate thread, "fire and forget"
                    JobManager.Initialize(o);
                })
                .WithNotParsed((o) =>
                {
                    Console.WriteLine("Error");
                });
        }

        private async void ExecuteResultCallback(ExecuteResult result)
        {
            // show balloon tip to the user
            this._tray.ShowBalloonTip(1750, this._tray.Text, result.ErrorMessage, ToolTipIcon.Info);

            // send result to the user
            await this._client.SendTextMessageAsync(this._update.Message.Chat.Id, result.ErrorMessage);
        }
    }
}
