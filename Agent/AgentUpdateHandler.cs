using Agent.Infrastructure;
using Agent.Infrastructure.Pipeline;
using Sdk;
using Sdk.Dependencies;
using Sdk.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Agent
{
    /// <summary>
    /// Handles Telegram bot update events.
    /// Routes incoming messages through the command processing pipeline.
    /// Implements IUpdateHandler for the Telegram Bot Client.
    /// </summary>
    public class AgentUpdateHandler : IUpdateHandler
    {
        private readonly TelegramMessageProcessor _messageProcessor;

        public AgentUpdateHandler(
            TelegramMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor ?? throw new ArgumentNullException(nameof(messageProcessor));
        }

        /// <summary>
        /// Handles incoming Telegram updates.
        /// Delegates message processing to the message processor.
        /// </summary>
        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Only process message updates
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    await _messageProcessor.ProcessMessageAsync(update);
                }
            }
            catch (Exception ex)
            {
                // Log but don't throw - ensure handler doesn't crash
                System.Diagnostics.Debug.WriteLine($"Error handling update: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles polling errors from the Telegram bot client.
        /// </summary>
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            try
            {
                var logPath = Sdk.PCManager.Combine("log.txt");
                await System.IO.File.AppendAllTextAsync(
                    logPath,
                    $"[{DateTime.UtcNow:O}] Telegram polling error: {exception}\n"
                );
            }
            catch
            {
                // Suppress logging errors
            }
        }
    }
}
