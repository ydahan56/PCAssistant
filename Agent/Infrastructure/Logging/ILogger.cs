namespace Agent.Infrastructure.Logging
{
    /// <summary>
    /// Log levels for the agent
    /// </summary>
    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }

    /// <summary>
    /// Contract for logging implementations
    /// </summary>
    public interface ILogger
    {
        void Log(LogLevel level, string message, Exception? exception = null);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? exception = null);
        void LogDebug(string message);
    }
}
