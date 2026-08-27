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
        public void RegisterApplicationServices(ICpuidHelper cpuidHelper, List<IPlugin> plugins)
        {
            if (cpuidHelper == null)
                throw new ArgumentNullException(nameof(cpuidHelper));

            if (plugins == null)
                throw new ArgumentNullException(nameof(plugins));

            // Register singleton services
            RegisterCoreServices(cpuidHelper);

            // Register plugin instances
            RegisterPlugins(plugins);

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

            // Register event aggregator as singleton (if not already registered)
            var eventAggregator = EventAggregator.Instance;
            _container.RegisterInstance<EventAggregator>(eventAggregator);

            // Register Cpuid64 as singleton
            _container.RegisterInstance(Cpuid64.Instance);
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
