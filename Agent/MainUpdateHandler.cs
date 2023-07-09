using CommandLine;
using DotNetEnv;
using Nito.AsyncEx;
using Sdk.Contracts;
using Sdk.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Agent
{
    public class MainUpdateHandler : IUpdateHandler
    {
        private readonly long authorized_Id;
        private readonly NotifyIcon tray;

        private readonly Dictionary<string, IPlugin> dict;

        public MainUpdateHandler(NotifyIcon tray)
        {
            var _id = Env.GetString("_id");
            this.authorized_Id = Convert.ToInt64(_id);
            this.tray = tray;

            this.dict = new Dictionary<string, IPlugin>();
            this.InitDict();
        }

        private void InitDict()
        {
            var _Plugins = Program.AppService.ResolveInstances<IPlugin>();

            foreach (var _Plugin in _Plugins.ToList())
            {
                this.dict.Add(_Plugin.Name!, _Plugin);
            }
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            AsyncContext.Run(async () => await System.IO.File.AppendAllTextAsync(".\\log.txt", exception.Message + "\n"));
            return Task.CompletedTask;
        }

        public Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;

            if (message!.From!.Id != this.authorized_Id)
            {
                AsyncContext.Run(async () => await client.SendTextMessageAsync(message.Chat.Id, "Unauthorized."));
                return Task.CompletedTask;
            }

            if (string.IsNullOrWhiteSpace(message.Text))
            {
                AsyncContext.Run(async () => await client.SendTextMessageAsync(
                    message.Chat.Id, "Unrecognized command.", replyToMessageId: message.MessageId));
                return Task.CompletedTask;
            }

            var tray_text = $"Received {message.Text} from {message.From.Username}.";
            this.tray.ShowBalloonTip(1750, "Telebot", tray_text, ToolTipIcon.Info);

            var split = message.Text.Split(' ', 2);

            var name = split[0];
            var isTrySuccess = this.dict.TryGetValue(name, out IPlugin? _Plugin);

            if (isTrySuccess)
            {
                var data = new DispatchData();
                data.FromMessageId = message.MessageId;

                if (_Plugin!.HasArguments)
                {
                    data.Args = split[1].SplitArgs();
                    AsyncContext.Run(() => _Plugin!.Dispatch(data));
                    return Task.CompletedTask;
                }

                AsyncContext.Run(() => _Plugin.Dispatch());
                return Task.CompletedTask;
            }

            AsyncContext.Run(async () => await client.SendTextMessageAsync(message.Chat.Id, "No such command."));
            return Task.CompletedTask;
        }
    }
}
