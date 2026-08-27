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

        public AgentUpdateHandler(NotifyIcon tray, IPCAssistant assistant, IServiceLocator services)
        {
            // Build the command processing pipeline
            var pipeline = BuildCommandPipeline(services);

            // Create message processor
            _messageProcessor = new TelegramMessageProcessor(assistant, tray, pipeline);
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
        public async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
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

        /// <summary>
        /// Builds the command processing pipeline with middleware.
        /// </summary>
        private ICommandPipeline BuildCommandPipeline(IServiceLocator services)
        {
            var pipeline = new CommandPipelineBuilder();

            // Add middleware in order
            var authMiddleware = new AuthorizationMiddleware();
            var errorMiddleware = new ErrorHandlingMiddleware();
            var dispatcher = new CommandDispatcher(services);

            // Build the pipeline: Authorization -> Error Handling -> Dispatch
            pipeline
                .Use(authMiddleware.InvokeAsync)
                .Use(errorMiddleware.InvokeAsync)
                .Use(async (context, next) =>
                {
                    await dispatcher.DispatchAsync(context);
                    await next();
                });

            return pipeline;
        }
    }
}
