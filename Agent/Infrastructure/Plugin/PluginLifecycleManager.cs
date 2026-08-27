using Sdk.Contracts;

namespace Agent.Infrastructure.Plugin
{
    /// <summary>
    /// Metadata about a loaded plugin
    /// </summary>
    public class PluginMetadata
    {
        public string Name { get; set; } = string.Empty;
        public Version Version { get; set; } = new Version(1, 0, 0);
        public string Description { get; set; } = string.Empty;
        public Type PluginType { get; set; } = typeof(object);
        public DateTime LoadedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Manages plugin lifecycle: loading, initialization, and cleanup
    /// </summary>
    public class PluginLifecycleManager
    {
        private readonly Dictionary<string, PluginMetadata> _pluginMetadata = new();
        private readonly List<IPlugin> _activePlugins = new();

        /// <summary>
        /// Registers a plugin and its metadata
        /// </summary>
        public void RegisterPlugin(IPlugin plugin, string name, string? description = null)
        {
            if (plugin == null)
                throw new ArgumentNullException(nameof(plugin));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Plugin name cannot be empty", nameof(name));

            var metadata = new PluginMetadata
            {
                Name = name,
                Description = description ?? string.Empty,
                PluginType = plugin.GetType(),
                LoadedAt = DateTime.UtcNow,
                IsActive = true
            };

            _pluginMetadata[name] = metadata;
            _activePlugins.Add(plugin);
        }

        /// <summary>
        /// Gets metadata for a plugin
        /// </summary>
        public PluginMetadata? GetPluginMetadata(string pluginName)
        {
            return _pluginMetadata.TryGetValue(pluginName, out var metadata) ? metadata : null;
        }

        /// <summary>
        /// Gets all registered plugins
        /// </summary>
        public IReadOnlyList<IPlugin> GetActivePlugins()
        {
            return _activePlugins.AsReadOnly();
        }

        /// <summary>
        /// Gets all plugin metadata
        /// </summary>
        public IReadOnlyDictionary<string, PluginMetadata> GetAllPluginMetadata()
        {
            return _pluginMetadata.AsReadOnly();
        }

        /// <summary>
        /// Gets the total number of loaded plugins
        /// </summary>
        public int GetPluginCount()
        {
            return _pluginMetadata.Count;
        }

        /// <summary>
        /// Deactivates a plugin
        /// </summary>
        public void DeactivatePlugin(string pluginName)
        {
            if (_pluginMetadata.TryGetValue(pluginName, out var metadata))
            {
                metadata.IsActive = false;
            }
        }

        /// <summary>
        /// Activates a plugin
        /// </summary>
        public void ActivatePlugin(string pluginName)
        {
            if (_pluginMetadata.TryGetValue(pluginName, out var metadata))
            {
                metadata.IsActive = true;
            }
        }

        /// <summary>
        /// Gets summary statistics about plugins
        /// </summary>
        public (int Total, int Active, int Inactive) GetPluginStatistics()
        {
            var total = _pluginMetadata.Count;
            var active = _pluginMetadata.Values.Count(m => m.IsActive);
            var inactive = total - active;

            return (total, active, inactive);
        }

        /// <summary>
        /// Clears all plugin registrations
        /// </summary>
        public void Clear()
        {
            _pluginMetadata.Clear();
            _activePlugins.Clear();
        }
    }
}
