using Agent.Helpers;
using Agent.Infrastructure.Configuration;
using Agent.Infrastructure.Logging;
using Agent.Infrastructure.Pipeline;
using Hardware;
using Sdk;
using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Hub;
using Sdk.Telegram;
using SimpleInjector;
using System.Collections.Generic;

namespace Agent.Infrastructure
{
    /// <summary>
    /// Centralized dependency injection container configuration.
    /// Handles registration of all application services, plugins, and dependencies.
    /// </summary>
    public class DependencyContainer
    {
        private readonly Container _container;

        public DependencyContainer(Container container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        /// <summary>
        /// Registers all application services and dependencies.
        /// </summary>
        public void RegisterApplicationServices(ICpuidHelper cpuidHelper, List<IPlugin> plugins, IPCAssistant telegramClient)
        {
            if (cpuidHelper == null)
                throw new ArgumentNullException(nameof(cpuidHelper));

            if (plugins == null)
                throw new ArgumentNullException(nameof(plugins));

            if (telegramClient == null)
                throw new ArgumentNullException(nameof(telegramClient));

            System.Diagnostics.Debug.WriteLine("=== Registering Application Services ===");

            // Register singleton services
            RegisterCoreServices(cpuidHelper);

            // Register configuration
            RegisterConfiguration();

            // Register telegram client
            RegisterTelegramServices(telegramClient);

            // Register infrastructure services
            RegisterInfrastructureServices();

            // Register pipeline services
            RegisterPipelineServices();

            // Register plugin instances
            RegisterPlugins(plugins);

            System.Diagnostics.Debug.WriteLine($"✓ Registered {plugins.Count} plugins");
            System.Diagnostics.Debug.WriteLine("=== Service Registration Complete ===");

            // Verify container configuration (optional - uncomment for strict validation)
            // _container.Verify();
        }

        /// <summary>
        /// Registers core application services.
        /// </summary>
        private void RegisterCoreServices(ICpuidHelper cpuidHelper)
        {
            // Register CPUID helper as singleton
            _container.RegisterInstance(cpuidHelper);

            // Register event aggregator as singleton
            var eventAggregator = EventAggregator.Instance;
            _container.RegisterInstance<EventAggregator>(eventAggregator);

            // Register Cpuid64 as singleton
            _container.RegisterInstance(Cpuid64.Instance);
        }

        /// <summary>
        /// Registers configuration services.
        /// </summary>
        private void RegisterConfiguration()
        {
            // Register agent configuration as singleton
            var config = AgentConfiguration.LoadFromEnvironment();
            ConfigurationValidator.Validate(config);
            _container.RegisterInstance(config);
        }

        /// <summary>
        /// Registers Telegram-related services.
        /// </summary>
        private void RegisterTelegramServices(IPCAssistant telegramClient)
        {
            // Register Telegram client as singleton
            _container.RegisterInstance<IPCAssistant>(telegramClient);
        }

        /// <summary>
        /// Registers infrastructure services.
        /// </summary>
        private void RegisterInfrastructureServices()
        {
            // Register Main (ApplicationContext) as transient
            _container.Register<Main>();

            // Register message processor as singleton
            _container.RegisterSingleton<TelegramMessageProcessor>();

            // Register update handler as singleton
            _container.RegisterSingleton<AgentUpdateHandler>();

            // Register logger (if needed in future)
            // _container.RegisterSingleton<ILogger, FileLogger>();
        }

        /// <summary>
        /// Registers pipeline middleware and services.
        /// </summary>
        private void RegisterPipelineServices()
        {
            // Register middleware as singletons
            _container.RegisterSingleton<AuthorizationMiddleware>();
            _container.RegisterSingleton<ErrorHandlingMiddleware>();

            // Register command dispatcher as singleton
            _container.RegisterSingleton<CommandDispatcher>();

            // Register pipeline factory
            _container.Register<ICommandPipeline>(() =>
            {
                var pipeline = new CommandPipelineBuilder();
                var authMiddleware = _container.GetInstance<AuthorizationMiddleware>();
                var errorMiddleware = _container.GetInstance<ErrorHandlingMiddleware>();
                var dispatcher = _container.GetInstance<CommandDispatcher>();

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
            }, Lifestyle.Singleton);
        }

        /// <summary>
        /// Registers all discovered plugins.
        /// </summary>
        private void RegisterPlugins(List<IPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                if (plugin != null)
                {
                    // Register each plugin by its type
                    _container.RegisterInstance(plugin.GetType(), plugin);
                }
            }
        }

        /// <summary>
        /// Gets the configured service locator for the application.
        /// </summary>
        public IServiceLocator GetServiceLocator()
        {
            return new DependencyLocator(_container);
        }

        /// <summary>
        /// Verifies that the container is properly configured.
        /// Should be called before the application runs in production.
        /// </summary>
        public void Verify()
        {
            _container.Verify();
        }
    }
}
