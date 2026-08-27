using Sdk;

namespace Agent.Infrastructure.Logging
{
    /// <summary>
    /// Simple file-based logger implementation.
    /// Writes structured log entries to a log file.
    /// Thread-safe using lock mechanism.
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly object _lockObject = new object();
        private readonly LogLevel _minimumLevel;

        public FileLogger(string? logFilePath = null, LogLevel minimumLevel = LogLevel.Debug)
        {
            _logFilePath = logFilePath ?? PCManager.Combine("log.txt");
            _minimumLevel = minimumLevel;
        }

        /// <summary>
        /// Logs a message at the specified log level
        /// </summary>
        public void Log(LogLevel level, string message, Exception? exception = null)
        {
            // Skip if below minimum level
            if (level < _minimumLevel)
                return;

            lock (_lockObject)
            {
                try
                {
                    var entry = FormatLogEntry(level, message, exception);
                    File.AppendAllText(_logFilePath, entry + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    // Suppress logging errors
                    System.Diagnostics.Debug.WriteLine($"Logging error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Logs an informational message
        /// </summary>
        public void LogInformation(string message)
        {
            Log(LogLevel.Information, message);
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        public void LogError(string message, Exception? exception = null)
        {
            Log(LogLevel.Error, message, exception);
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        public void LogDebug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// Formats a log entry as a structured string
        /// </summary>
        private static string FormatLogEntry(LogLevel level, string message, Exception? exception)
        {
            var timestamp = DateTime.UtcNow.ToString("O");
            var entry = $"[{timestamp}] [{level}] {message}";

            if (exception != null)
            {
                entry += Environment.NewLine + $"Exception: {exception}";
            }

            return entry;
        }

        /// <summary>
        /// Clears the log file
        /// </summary>
        public void Clear()
        {
            lock (_lockObject)
            {
                try
                {
                    File.Delete(_logFilePath);
                }
                catch
                {
                    // Suppress errors
                }
            }
        }

        /// <summary>
        /// Gets the log file path
        /// </summary>
        public string GetLogFilePath()
        {
            return _logFilePath;
        }
    }
}
