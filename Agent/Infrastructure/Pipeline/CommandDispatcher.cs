using CommandLine;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Plugins;
using PluginBase = Sdk.Plugins.Plugin;

namespace Agent.Infrastructure.Pipeline
{
    /// <summary>
    /// Routes commands to the appropriate plugin and executes them.
    /// Interfaces with CommandLineParser to match command text to plugin types.
    /// </summary>
    public class CommandDispatcher
    {
        private readonly IServiceLocator _services;
        private readonly Type[] _commandTypes;

        public CommandDispatcher(IServiceLocator services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));

            // Load all plugin types that implement the Plugin base class
            _commandTypes = LoadCommandTypes();
        }

        /// <summary>
        /// Executes a command through the dispatcher.
        /// Parses the command, finds the appropriate plugin, and executes it.
        /// </summary>
        public async Task DispatchAsync(CommandContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.IsCancelled)
            {
                // Skip if the pipeline has already cancelled this command
                return;
            }

            // Parse the arguments using CommandLineParser
            var parseResult = Parser.Default.ParseArguments(context.Arguments, _commandTypes);

            // Handle the result by checking if parsing succeeded
            var handled = false;

            foreach (var type in _commandTypes)
            {
                // Try to parse against each command type
                var result = Parser.Default.ParseArguments(context.Arguments, type);

                if (result.Tag == ParserResultType.Parsed)
                {
                    var plugin = result as dynamic;

                    if (plugin is PluginBase parsedPlugin)
                    {
                        await ExecutePluginAsync(context, parsedPlugin);
                        handled = true;
                        break;
                    }
                }
            }

            if (!handled)
            {
                context.Error = new ArgumentException(
                    $"Failed to parse command: {context.CommandText}");
            }
        }

        /// <summary>
        /// Executes a plugin after successful parsing.
        /// </summary>
        private async Task ExecutePluginAsync(CommandContext context, PluginBase plugin)
        {
            try
            {
                // Initialize the plugin with the service locator
                plugin.Initialize(_services);

                // Set the result callback to capture execution results
                plugin.SetExecuteResultCallback((result) =>
                {
                    context.ExecutionResult = result;
                });

                // Schedule plugin execution if delay options are provided
                plugin.SetExecutionSchedule();

                // Execute the plugin
                plugin.Execute();
            }
            catch (Exception ex)
            {
                context.Error = ex;
                throw;
            }
        }

        /// <summary>
        /// Loads all plugin types from the service locator.
        /// </summary>
        private Type[] LoadCommandTypes()
        {
            try
            {
                var plugins = _services.ResolveInstances<IPlugin>();
                return plugins.Select(p => p.GetType()).ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Warning: Failed to load command types: {ex.Message}");
                return Array.Empty<Type>();
            }
        }

        /// <summary>
        /// Gets the number of available commands.
        /// </summary>
        public int GetCommandCount()
        {
            return _commandTypes.Length;
        }
    }
}
