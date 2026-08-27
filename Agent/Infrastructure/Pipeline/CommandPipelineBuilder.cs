namespace Agent.Infrastructure.Pipeline
{
    /// <summary>
    /// Implementation of the command processing pipeline.
    /// Executes middleware components in order, providing a clean way to handle
    /// cross-cutting concerns (logging, authorization, error handling, etc.)
    /// </summary>
    public class CommandPipelineBuilder : ICommandPipeline
    {
        private readonly List<CommandPipelineMiddleware> _middleware = new();

        /// <summary>
        /// Adds a middleware component to the pipeline.
        /// Middleware is executed in the order it was added.
        /// </summary>
        public ICommandPipeline Use(CommandPipelineMiddleware middleware)
        {
            if (middleware == null)
                throw new ArgumentNullException(nameof(middleware));

            _middleware.Add(middleware);
            return this;
        }

        /// <summary>
        /// Executes the command through the entire pipeline.
        /// Creates a delegate chain where each middleware calls the next.
        /// </summary>
        public async Task ExecuteAsync(CommandContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Build the middleware chain
            Func<Task> next = async () => { }; // Default terminal action

            // Reverse iteration to build the chain from the end
            for (int i = _middleware.Count - 1; i >= 0; i--)
            {
                var middleware = _middleware[i];
                var currentNext = next;

                next = async () =>
                {
                    await middleware(context, currentNext);
                };
            }

            // Execute the entire pipeline
            await next();
        }

        /// <summary>
        /// Returns the number of middleware components in the pipeline.
        /// </summary>
        public int GetMiddlewareCount()
        {
            return _middleware.Count;
        }
    }
}
