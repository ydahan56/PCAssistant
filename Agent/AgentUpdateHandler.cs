using Agent.Notification;
using CommandLine;
using FluentScheduler;
using Nito.AsyncEx;
using Sdk;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Plugins;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agent
{
    public class AgentUpdateHandler : IUpdateHandler
    {
        private Update _update;

        private readonly INotificationHandler _tray;
        private readonly IPCAssistant _assistant;

        private readonly List<long> _whitelist;
        private readonly Type[] _commands;

        public AgentUpdateHandler(INotificationHandler tray, IPCAssistant assistant)
        {
            this._tray = tray;
            this._assistant = assistant;

            //this._whitelist = Env.GetString("whitelist")
            //    .Split(",")
            //    .Select(id => {
            //        if (string.IsNullOrWhiteSpace(id))
            //            return new ChatId(0);

            //        var parsed = Convert.ToInt64(id);
            //        var chat = new ChatId(id);

            //        return chat;
            //    })
            //    .ToList();

            this._commands = Program.IOC
                .GetAllInstances<IPlugin>()
                .Select(x => x.GetType())
                .ToArray();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            this._update = update;

            //if (!this._whitelist.Contains(update.Message.From.Id))
            //{
            //    await client.SendTextMessageAsync(update.Message.Chat.Id, "Unauthorized.");

            //    return;
            //}

            if (string.IsNullOrWhiteSpace(update.Message.Text))
            {
                await this._assistant.SendMessage(
                    new ChatId(update.Message.Chat.Id),
                    "Unrecognized command.",
                    replyParameters: new ReplyParameters()
                    {
                        MessageId = update.Message.MessageId
                    }
                );

                return;
            }


            dynamic from = String.IsNullOrWhiteSpace(
                update.Message.From.Username) ?
                update.Message.From.Id :
                update.Message.From.Username
            ;

            var text = $"Received {update.Message.Text} from {from}.";

            // show balloon tip to the user
            this._tray.ShowMessage(text);

            // read args from user
            var args = update.Message.Text.SplitArgs();

            Parser.Default.ParseArguments(args, this._commands)
                .WithParsed<Plugin>((o) =>
                {
                    // initliaze plugin
                    o.Initialize(new ServiceResolver(Program.IOC));

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
                this._tray.ShowMessage(result.StatusText);

                // send result to the user
                AsyncContext.Run(async () =>
                {
                    await this._assistant.SendMessage(
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
                    await this._assistant.SendDocument(
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
                    await this._assistant.SendPhoto(
                        this._update.Message.Chat.Id,
                        InputFile.FromStream(
                            image.Stream, image.FileName
                        )
                    );
                });
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            string path = PCManager.Combine("log.txt");
            await System.IO.File.AppendAllTextAsync(path, exception.ToString() + Environment.NewLine);
        }
    }
}
