using Sdk.Models;

namespace Agent.Infrastructure.Pipeline
{
    /// <summary>
    /// Represents a command processing context that flows through the pipeline.
    /// Contains the parsed command, user information, and execution state.
    /// </summary>
    public class CommandContext
    {
        /// <summary>
        /// The command text received from Telegram
        /// </summary>
        public string CommandText { get; set; } = string.Empty;

        /// <summary>
        /// The parsed command arguments
        /// </summary>
        public string[] Arguments { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Chat ID where the command originated
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// User ID who sent the command
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Username of the user (if available)
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Message ID to reply to
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// The result of command execution
        /// </summary>
        public ExecuteResult? ExecutionResult { get; set; }

        /// <summary>
        /// Indicates if pipeline execution should continue
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Any error that occurred during processing
        /// </summary>
        public Exception? Error { get; set; }

        /// <summary>
        /// User-defined metadata storage
        /// </summary>
        public Dictionary<string, object> Metadata { get; } = new();
    }

    /// <summary>
    /// Delegate for pipeline middleware. 
    /// Represents a middleware component in the command processing pipeline.
    /// </summary>
    public delegate Task CommandPipelineMiddleware(CommandContext context, Func<Task> next);

    /// <summary>
    /// Executes middleware in a pipeline chain.
    /// Handles the sequential execution of middleware components.
    /// </summary>
    public interface ICommandPipeline
    {
        /// <summary>
        /// Adds a middleware component to the pipeline.
        /// </summary>
        ICommandPipeline Use(CommandPipelineMiddleware middleware);

        /// <summary>
        /// Executes the command through the entire pipeline.
        /// </summary>
        Task ExecuteAsync(CommandContext context);
    }
}
