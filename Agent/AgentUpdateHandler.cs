using Agent.Notification;
using CommandLine;
using Common.Queue;
using DotNetEnv;
using FluentScheduler;
using Sdk;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Plugins;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Agent
{
    public class AgentUpdateHandler : IUpdateHandler
    {
        private Update _update;


        private readonly IPCAssistant _client;
        private readonly INotificationHandler _tray;
        private readonly ISimpleMessageQueue<ExecuteContext> _queue;

        private readonly List<ChatId> _whitelist; // todo - restore
        private readonly Type[] _commands;

        public AgentUpdateHandler(
            IPCAssistant client, INotificationHandler tray, ISimpleMessageQueue<ExecuteContext> queue)
        {
            this._tray = tray;
            this._client = client;
            this._queue = queue;

            this._whitelist = Env.GetString("whitelist")
                .Split(",")
                .Select(id =>
                {
                    if (string.IsNullOrWhiteSpace(id))
                        return new ChatId(0);

                    var parsed = Convert.ToInt64(id);
                    var chat = new ChatId(id);

                    return chat;
                })
                .ToList();

            this._commands = Program.IOC
                .GetAllInstances<IPlugin>()
                .Select(x => x.GetType())
                .ToArray();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            this._update = update;

            if (!this._whitelist.Contains(update.Message.From.Id))
            {
                //await client.SendMessage(update.Message.Chat.Id, "Unauthorized.");

                return;
            }

            if (string.IsNullOrWhiteSpace(update.Message.Text))
            {
                await this._client.SendMessage(
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
                    o.Initialize(new ServiceResolver(Program.IOC))
                     .SetExecuteContext(new ExecuteContext()
                     {
                         ChatId = new ChatId(update.Message.Chat.Id),
                         ReplyParameters = new ReplyParameters()
                         {
                             MessageId = update.Message.MessageId
                         }
                     })
                    .SetExecuteResultCallback(this._queue.Enqueue)
                    .SetExecutionSchedule();

                    // execute command on a separate thread, "fire and forget"
                    JobManager.Initialize(o);
                })
                .WithNotParsed((o) =>
                {
                    Console.WriteLine("Error");
                });
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            string path = PCManager.Combine("log.txt");
            await System.IO.File.AppendAllTextAsync(path, exception.ToString() + Environment.NewLine);
        }
    }
}
