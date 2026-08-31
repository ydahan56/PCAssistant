using Agent.Helpers;
using Agent.Infrastructure.Configuration;
using Agent.Infrastructure.Logging;
using DotNetEnv;
using Hardware;
using Sdk;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Hub;
using Sdk.Telegram;
using SimpleInjector;

namespace Agent.Infrastructure
{
    /// <summary>
    /// Bootstraps the application by orchestrating the initialization sequence.
    /// Responsible for coordinating DI setup, plugin loading, and service initialization.
    /// </summary>
    public class Bootstrapper
    {
        private IServiceLocator? _serviceLocator;
        private readonly Container _container;
        private IPCAssistant? _telegramClient;

        public Bootstrapper()
        {
            _container = new Container();
        }

        /// <summary>
        /// Initializes the application with all required services and plugins.
        /// Returns an IServiceLocator that can be used to resolve services throughout the application.
        /// </summary>
        public IServiceLocator InitializeApplication()
        {
            // Load environment variables
            LoadEnvironmentConfiguration();

            // Subscribe to application events
            SubscribeToApplicationEvents();

            // Initialize native dependencies
            InitializeNativeDependencies();

            // Get Telegram bot token and create client
            var token = GetTelegramToken();
            _telegramClient = new PCAssistantClient(token);

            // Load plugins
            var plugins = LoadPlugins();

            // Configure DI container
            var dependencyContainer = new DependencyContainer(_container);

            // Get CPUID helper
            var cpuidHelper = new CpuidHelper();

            // Register all services and plugins
            dependencyContainer.RegisterApplicationServices(cpuidHelper, plugins, _telegramClient);

            // Verify container configuration in debug mode
#if DEBUG
            try
            {
                dependencyContainer.Verify();
                System.Diagnostics.Debug.WriteLine("✓ DI Container verification successful - all dependencies can be resolved");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"⚠ DI Container verification failed: {ex.Message}");
                throw;
            }
#endif

            // Create and store service locator
            _serviceLocator = dependencyContainer.GetServiceLocator();

            return _serviceLocator;
        }

        /// <summary>
        /// Loads environment variables from .env file.
        /// </summary>
        private void LoadEnvironmentConfiguration()
        {
            try
            {
                Env.Load();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Warning: Failed to load .env file: {ex.Message}");
            }
        }

        /// <summary>
        /// Subscribes to application-level events.
        /// </summary>
        private void SubscribeToApplicationEvents()
        {
            EventAggregator.Instance.MessageHub
                .Subscribe<ApplicationEvent>(OnApplicationEvent);
        }

        /// <summary>
        /// Handles application events (Exit, Restart, etc.)
        /// </summary>
        private void OnApplicationEvent(ApplicationEvent eventType)
        {
            switch (eventType)
            {
                case ApplicationEvent.Exit:
                    Application.Exit();
                    break;
                case ApplicationEvent.Restart:
                    Application.Restart();
                    break;
            }
        }

        /// <summary>
        /// Initializes native dependencies (CPUID SDK, etc.)
        /// </summary>
        private void InitializeNativeDependencies()
        {
            try
            {
                // Initialize CPUID SDK for CPU information
                Cpuid64.Instance.InitSDK(PCManager.GetAppDirectory());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Warning: Failed to initialize CPUID SDK: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads all plugins from the plugin directory.
        /// </summary>
        private List<IPlugin> LoadPlugins()
        {
            var pluginsPath = PCManager.Combine("..\\Plugins");
            var loader = new PluginLoader(pluginsPath);

            try
            {
                var plugins = loader.LoadPlugins();
                System.Diagnostics.Debug.WriteLine($"Loaded {plugins.Count} plugins successfully.");
                return plugins;
            }
            catch (DirectoryNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets the Telegram client token from environment variables.
        /// Uses AgentConfiguration for strongly-typed access and validation.
        /// </summary>
        public string GetTelegramToken()
        {
            var config = AgentConfiguration.LoadFromEnvironment();
            ConfigurationValidator.Validate(config);
            return config.TelegramToken;
        }

        /// <summary>
        /// Gets the initialized service locator.
        /// Must be called after InitializeApplication().
        /// </summary>
        public IServiceLocator GetServiceLocator()
        {
            return _serviceLocator ?? throw new InvalidOperationException(
                "Application not initialized. Call InitializeApplication() first.");
        }

        /// <summary>
        /// Gets the Telegram client instance.
        /// Must be called after InitializeApplication().
        /// </summary>
        public IPCAssistant GetTelegramClient()
        {
            return _telegramClient ?? throw new InvalidOperationException(
                "Application not initialized. Call InitializeApplication() first.");
        }

        /// <summary>
        /// Cleanly shuts down the application and releases resources.
        /// </summary>
        public void Shutdown()
        {
            try
            {
                _telegramClient?.Cancel();
                Cpuid64.Instance.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during shutdown: {ex.Message}");
            }
        }
    }
}
