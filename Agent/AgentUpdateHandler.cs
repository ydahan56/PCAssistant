using CommandLine;
using DotNetEnv;
using FluentScheduler;
using Sdk;
using Sdk.Plugins;
using Sdk.Contracts;
using Sdk.Models;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Nito.AsyncEx;
using Telegram.Bot.Types.Enums;

namespace Agent
{
    public class AgentUpdateHandler : IUpdateHandler
    {
        private Update _update;

        private readonly NotifyIcon _tray;
        private readonly IPCAssistant _assistant;

        private readonly List<long> _whitelist;
        private readonly Type[] _commands;

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
                .Select(x => x.GetType())
                .ToArray();
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

            Parser.Default.ParseArguments(args, this._commands)
                .WithParsed<Plugin>((o) =>
                {
                    // initliaze plugin
                    o.Initialize(Program.Services);

                    // set callback for the command
                    o.SetExecuteResultCallback(this.ExecuteResultCallback);

                    // schedule the job to run
                    o.SetExecutionSchedule();

                    // execute command on a separate thread, "fire and forget"
                    JobManager.Initialize(o);
                })
                .WithNotParsed((o) =>
                {
                    Console.WriteLine("Error");
                });
        }

        private void ExecuteResultCallback(ExecuteResult result)
        {
            if (result.ResultType == ExecuteResultType.Text)
            {
                // show balloon tip to the user
                this._tray.ShowBalloonTip(1750, this._tray.Text, result.StatusText, ToolTipIcon.Info);

                // send result to the user
                AsyncContext.Run(async () =>
                {
                    await this._assistant.SendTextMessageAsync(
                        this._update.Message.Chat.Id,
                        result.StatusText,
                        parseMode: ParseMode.Markdown
                    );
                });
            }
            else if (result.ResultType == ExecuteResultType.Document)
            {
                AsyncContext.Run(async () =>
                {
                    var document = (result as ExecuteDocumentResult);

                    // perform send
                    await this._assistant.SendDocumentAsync(
                        this._update.Message.Chat.Id,
                        InputFile.FromStream(
                            document.Stream, document.FileName
                        )
                    );
                });
            }
            else if (result.ResultType == ExecuteResultType.Image)
            {
                AsyncContext.Run(async () =>
                {
                    var image = (result as ExecuteImageResult);

                    // perform send
                    await this._assistant.SendPhotoAsync(
                        this._update.Message.Chat.Id,
                        InputFile.FromStream(
                            image.Stream, image.FileName
                        )
                    );
                });
            }
        }
    }
}
