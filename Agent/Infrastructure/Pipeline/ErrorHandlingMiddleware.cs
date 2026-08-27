using Sdk;

namespace Agent.Infrastructure.Pipeline
{
    /// <summary>
    /// Middleware for centralized error handling and recovery.
    /// Catches exceptions from downstream middleware and command handlers,
    /// converts them to appropriate error results.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// Middleware implementation for error handling.
        /// </summary>
        public async Task InvokeAsync(CommandContext context, Func<Task> next)
        {
            try
            {
                // Execute the next middleware
                await next();

                // Log if authorization was denied
                if (context.Error is UnauthorizedAccessException)
                {
                    LogUnauthorizedAccess(context);
                }
            }
            catch (OperationCanceledException ex)
            {
                context.Error = ex;
                context.IsCancelled = true;
                LogError(context, "Operation was cancelled");
            }
            catch (ArgumentException ex)
            {
                context.Error = ex;
                LogError(context, "Invalid arguments provided");
            }
            catch (Exception ex)
            {
                context.Error = ex;
                LogError(context, $"Unexpected error: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Logs unauthorized access attempts.
        /// </summary>
        private void LogUnauthorizedAccess(CommandContext context)
        {
            var filePath = PCManager.Combine("log.txt");
            var entry = $"[{DateTime.UtcNow:O}] UNAUTHORIZED: User {context.UserId} ({context.Username}) attempted command: {context.CommandText}\n";

            try
            {
                File.AppendAllText(filePath, entry);
            }
            catch
            {
                // Suppress logging errors
            }
        }

        /// <summary>
        /// Logs general errors to the error log file.
        /// </summary>
        private void LogError(CommandContext context, string message)
        {
            var filePath = PCManager.Combine("log.txt");
            var entry = $"[{DateTime.UtcNow:O}] ERROR: {message}\n" +
                        $"Command: {context.CommandText}\n" +
                        $"User: {context.UserId} ({context.Username})\n" +
                        $"Exception: {context.Error}\n\n";

            try
            {
                File.AppendAllText(filePath, entry);
            }
            catch
            {
                // Suppress logging errors
            }
        }
    }
}
