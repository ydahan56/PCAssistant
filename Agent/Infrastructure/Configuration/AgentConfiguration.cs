using DotNetEnv;

namespace Agent.Infrastructure.Configuration
{
    /// <summary>
    /// Strongly-typed configuration for the PCAssistant Agent.
    /// Loads and validates configuration from environment variables.
    /// </summary>
    public class AgentConfiguration
    {
        /// <summary>
        /// Telegram bot token for authentication
        /// </summary>
        public string TelegramToken { get; set; } = string.Empty;

        /// <summary>
        /// Comma-separated list of authorized user IDs
        /// Empty or null means all users are allowed
        /// </summary>
        public string? WhitelistedUserIds { get; set; }

        /// <summary>
        /// Gets the list of authorized user IDs
        /// </summary>
        public List<long> GetAuthorizedUserIds()
        {
            if (string.IsNullOrWhiteSpace(WhitelistedUserIds))
            {
                return new List<long>();
            }

            var userIds = new List<long>();

            foreach (var id in WhitelistedUserIds.Split(','))
            {
                if (string.IsNullOrWhiteSpace(id))
                    continue;

                if (long.TryParse(id.Trim(), out var userId))
                {
                    userIds.Add(userId);
                }
            }

            return userIds;
        }

        /// <summary>
        /// Loads configuration from environment variables
        /// </summary>
        public static AgentConfiguration LoadFromEnvironment()
        {
            // Load .env file if it exists
            try
            {
                Env.Load();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Warning: Failed to load .env file: {ex.Message}");
            }

            var config = new AgentConfiguration
            {
                TelegramToken = Env.GetString("token", ""),
                WhitelistedUserIds = Env.GetString("whitelist", "")
            };

            return config;
        }

        /// <summary>
        /// Creates a configuration with default/test values
        /// </summary>
        public static AgentConfiguration CreateDefault()
        {
            return new AgentConfiguration
            {
                TelegramToken = string.Empty,
                WhitelistedUserIds = string.Empty
            };
        }

        /// <summary>
        /// Creates a configuration with specific values
        /// </summary>
        public static AgentConfiguration Create(string token, string? whitelistedUserIds = null)
        {
            return new AgentConfiguration
            {
                TelegramToken = token,
                WhitelistedUserIds = whitelistedUserIds
            };
        }
    }
}
