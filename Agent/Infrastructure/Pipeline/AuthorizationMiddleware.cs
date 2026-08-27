using Agent.Infrastructure.Configuration;

namespace Agent.Infrastructure.Pipeline
{
    /// <summary>
    /// Middleware for validating user authorization.
    /// Checks if the user sending the command is in the whitelist.
    /// Uses AgentConfiguration for configuration management.
    /// </summary>
    public class AuthorizationMiddleware
    {
        private readonly List<long> _whitelistedUserIds;

        public AuthorizationMiddleware()
        {
            var config = AgentConfiguration.LoadFromEnvironment();
            _whitelistedUserIds = config.GetAuthorizedUserIds();
        }

        /// <summary>
        /// Middleware implementation for authorization checks.
        /// </summary>
        public async Task InvokeAsync(CommandContext context, Func<Task> next)
        {
            // Skip authorization if whitelist is empty (all users allowed)
            if (_whitelistedUserIds.Count == 0)
            {
                await next();
                return;
            }

            // Check if user is authorized
            if (!_whitelistedUserIds.Contains(context.UserId))
            {
                context.IsCancelled = true;
                context.Error = new UnauthorizedAccessException(
                    $"User {context.UserId} is not authorized to execute commands.");

                return;
            }

            // User is authorized, continue to next middleware
            await next();
        }

        /// <summary>
        /// Gets the number of whitelisted users.
        /// </summary>
        public int GetWhitelistCount()
        {
            return _whitelistedUserIds.Count;
        }
    }
}
