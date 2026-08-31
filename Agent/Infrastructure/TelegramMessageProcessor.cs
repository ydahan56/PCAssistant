using Nito.AsyncEx;
using Sdk;
using Sdk.Telegram;
using Sdk.Extensions;
using Agent.Infrastructure.Pipeline;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agent.Infrastructure
{
    /// <summary>
    /// Processes Telegram messages and routes them through the command pipeline.
    /// Extracts user input, builds command context, and handles results.
    /// </summary>
    public class TelegramMessageProcessor
    {
        private readonly IPCAssistant _assistant;
        private readonly ICommandPipeline _pipeline;
        private Update? _currentUpdate;

        public TelegramMessageProcessor(
            IPCAssistant assistant,
            ICommandPipeline pipeline)
        {
            _assistant = assistant ?? throw new ArgumentNullException(nameof(assistant));
            _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        }

        /// <summary>
        /// Processes a message update from Telegram.
        /// </summary>
        public async Task ProcessMessageAsync(Update update)
        {
            if (update?.Message == null || string.IsNullOrWhiteSpace(update.Message.Text))
            {
                await SendInvalidCommandNotificationAsync(update);
                return;
            }

            _currentUpdate = update;

            // Extract user information
            var userId = update.Message.From?.Id ?? 0;
            var username = update.Message.From?.Username;
            var chatId = update.Message.Chat.Id;
            var messageId = update.Message.MessageId;
            var commandText = update.Message.Text;

            // Log the incoming command
            var displayName = string.IsNullOrWhiteSpace(username) ? userId.ToString() : username;
            System.Diagnostics.Debug.WriteLine($"Received '{commandText}' from {displayName}.");

            // Parse arguments
            var args = commandText.SplitArgs();

            // Build command context
            var context = new CommandContext
            {
                CommandText = commandText,
                Arguments = args,
                ChatId = chatId,
                UserId = userId,
                Username = username,
                MessageId = messageId
            };

            // Process through the pipeline
            await _pipeline.ExecuteAsync(context);

            // Handle the result
            await HandleExecutionResultAsync(context);
        }

        /// <summary>
        /// Handles the command execution result.
        /// </summary>
        private async Task HandleExecutionResultAsync(CommandContext context)
        {
            if (_currentUpdate?.Message == null)
                return;

            // If execution failed, send error notification
            if (context.Error != null)
            {
                var errorMessage = context.Error is UnauthorizedAccessException
                    ? "❌ Unauthorized. You don't have permission to execute this command."
                    : "❌ An error occurred while executing the command.";

                await SendTextMessageAsync(context.ChatId, errorMessage, context.MessageId);
                return;
            }

            // If execution was cancelled, don't send a response
            if (context.IsCancelled)
            {
                return;
            }

            // Send the execution result if available
            if (context.ExecutionResult != null)
            {
                await SendResultAsync(context);
            }
        }

        /// <summary>
        /// Sends an execution result to the user.
        /// </summary>
        private async Task SendResultAsync(CommandContext context)
        {
            if (context.ExecutionResult == null)
                return;

            var resultType = context.ExecutionResult.ResultType;

            if (resultType == Sdk.Models.ExecuteResultType.Text)
            {
                // Send text message
                await SendTextMessageAsync(context.ChatId, context.ExecutionResult.StatusText, context.MessageId);
            }
            else if (resultType == Sdk.Models.ExecuteResultType.Document)
            {
                var document = context.ExecutionResult as Sdk.Models.ExecuteDocumentResult;
                if (document != null)
                {
                    await SendDocumentAsync(context.ChatId, document);
                }
            }
            else if (resultType == Sdk.Models.ExecuteResultType.Image)
            {
                var image = context.ExecutionResult as Sdk.Models.ExecuteImageResult;
                if (image != null)
                {
                    await SendImageAsync(context.ChatId, image);
                }
            }
        }

        /// <summary>
        /// Sends a text message response.
        /// </summary>
        private async Task SendTextMessageAsync(long chatId, string text, int? replyToMessageId = null)
        {
            AsyncContext.Run(async () =>
            {
                await ((ITelegramBotClient)_assistant).SendMessage(
                    new ChatId(chatId), text,
                    replyParameters: new ReplyParameters() { MessageId = replyToMessageId },
                    parseMode: ParseMode.Markdown
                );
            });
        }

        /// <summary>
        /// Sends a document response.
        /// </summary>
        private async Task SendDocumentAsync(long chatId, Sdk.Models.ExecuteDocumentResult document)
        {
            AsyncContext.Run(async () =>
            {
                await ((ITelegramBotClient)_assistant).SendDocument(
                    new ChatId(chatId),
                    InputFile.FromStream(document.Stream, document.FileName)
                );
            });
        }

        /// <summary>
        /// Sends an image response.
        /// </summary>
        private async Task SendImageAsync(long chatId, Sdk.Models.ExecuteImageResult image)
        {
            AsyncContext.Run(async () =>
            {
                await ((ITelegramBotClient)_assistant).SendPhoto(
                    new ChatId(chatId),
                    InputFile.FromStream(image.Stream, image.FileName)
                );
            });
        }

        /// <summary>
        /// Sends an invalid command notification.
        /// </summary>
        private async Task SendInvalidCommandNotificationAsync(Update? update)
        {
            if (update?.Message == null)
                return;

            AsyncContext.Run(async () =>
            {
                await ((ITelegramBotClient)_assistant).SendMessage(
                    new ChatId(update.Message.Chat.Id),
                    "❌ Unrecognized command.",
                    replyParameters: new ReplyParameters() { MessageId = update.Message.MessageId }
                );
            });
        }
    }
}
