namespace Agent.Infrastructure.Configuration
{
    /// <summary>
    /// Validates agent configuration for required values and formats.
    /// Ensures configuration is safe and complete before application startup.
    /// </summary>
    public class ConfigurationValidator
    {
        /// <summary>
        /// Validates the agent configuration
        /// Throws InvalidOperationException if validation fails
        /// </summary>
        public static void Validate(AgentConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            // Validate Telegram token
            if (string.IsNullOrWhiteSpace(config.TelegramToken))
            {
                throw new InvalidOperationException(
                    "Telegram token is not configured. Set the 'token' environment variable or add it to .env file.");
            }

            // Validate whitelist format if provided
            if (!string.IsNullOrWhiteSpace(config.WhitelistedUserIds))
            {
                ValidateWhitelist(config.WhitelistedUserIds);
            }
        }

        /// <summary>
        /// Validates the whitelist format
        /// </summary>
        private static void ValidateWhitelist(string whitelist)
        {
            var userIdStrings = whitelist.Split(',');

            foreach (var idStr in userIdStrings)
            {
                if (string.IsNullOrWhiteSpace(idStr))
                    continue;

                if (!long.TryParse(idStr.Trim(), out _))
                {
                    throw new InvalidOperationException(
                        $"Invalid user ID in whitelist: '{idStr.Trim()}'. User IDs must be valid long integers.");
                }
            }
        }

        /// <summary>
        /// Gets validation errors without throwing exceptions
        /// Returns empty list if configuration is valid
        /// </summary>
        public static List<string> GetValidationErrors(AgentConfiguration config)
        {
            var errors = new List<string>();

            if (config == null)
            {
                errors.Add("Configuration is null");
                return errors;
            }

            // Check required fields
            if (string.IsNullOrWhiteSpace(config.TelegramToken))
            {
                errors.Add("Telegram token is not configured. Set the 'token' environment variable.");
            }

            // Check whitelist format
            if (!string.IsNullOrWhiteSpace(config.WhitelistedUserIds))
            {
                var whitelistErrors = ValidateWhitelistNonThrow(config.WhitelistedUserIds);
                errors.AddRange(whitelistErrors);
            }

            return errors;
        }

        /// <summary>
        /// Validates whitelist without throwing exceptions
        /// </summary>
        private static List<string> ValidateWhitelistNonThrow(string whitelist)
        {
            var errors = new List<string>();
            var userIdStrings = whitelist.Split(',');

            for (int i = 0; i < userIdStrings.Length; i++)
            {
                var idStr = userIdStrings[i];

                if (string.IsNullOrWhiteSpace(idStr))
                    continue;

                if (!long.TryParse(idStr.Trim(), out _))
                {
                    errors.Add($"Invalid user ID at position {i}: '{idStr.Trim()}'. User IDs must be valid long integers.");
                }
            }

            return errors;
        }
    }
}
