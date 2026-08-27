using Sdk.Contracts;
using System.Reflection;

namespace Agent.Infrastructure
{
    /// <summary>
    /// Responsible for discovering and loading plugin assemblies from the plugin directory.
    /// Implements plugin discovery pattern with assembly reflection.
    /// </summary>
    public class PluginLoader
    {
        private readonly string _pluginDirectoryPath;
        private const string PluginSearchPattern = "*Plugin.dll";
        private const string PluginClassName = "DllMain";

        public PluginLoader(string pluginDirectoryPath)
        {
            _pluginDirectoryPath = pluginDirectoryPath ?? throw new ArgumentNullException(nameof(pluginDirectoryPath));
        }

        /// <summary>
        /// Discovers and loads all plugins from the configured plugin directory.
        /// Returns a list of instantiated plugin objects.
        /// </summary>
        /// <returns>List of loaded plugins, or empty list if no plugins found.</returns>
        /// <exception cref="DirectoryNotFoundException">Thrown when plugin directory does not exist.</exception>
        public List<IPlugin> LoadPlugins()
        {
            ValidatePluginDirectory();

            var pluginAssemblies = DiscoverPluginAssemblies();

            if (pluginAssemblies.Count == 0)
            {
                return new List<IPlugin>();
            }

            return InstantiatePlugins(pluginAssemblies);
        }

        /// <summary>
        /// Validates that the plugin directory exists.
        /// </summary>
        private void ValidatePluginDirectory()
        {
            if (!Directory.Exists(_pluginDirectoryPath))
            {
                throw new DirectoryNotFoundException(
                    $"Plugin directory not found: {_pluginDirectoryPath}");
            }
        }

        /// <summary>
        /// Discovers all plugin assemblies in the plugin directory.
        /// Searches recursively for files matching the plugin pattern.
        /// </summary>
        private List<string> DiscoverPluginAssemblies()
        {
            return Directory
                .EnumerateFiles(_pluginDirectoryPath, PluginSearchPattern, SearchOption.AllDirectories)
                .ToList();
        }

        /// <summary>
        /// Instantiates plugins from discovered assemblies.
        /// For each assembly, loads the type named "DllMain" and instantiates it as IPlugin.
        /// </summary>
        private List<IPlugin> InstantiatePlugins(List<string> assemblyPaths)
        {
            var plugins = new List<IPlugin>();

            foreach (var path in assemblyPaths)
            {
                try
                {
                    var plugin = LoadPluginFromAssembly(path);
                    if (plugin != null)
                    {
                        plugins.Add(plugin);
                    }
                }
                catch (Exception ex)
                {
                    // Log the error but continue loading other plugins
                    System.Diagnostics.Debug.WriteLine($"Failed to load plugin from {path}: {ex.Message}");
                }
            }

            return plugins;
        }

        /// <summary>
        /// Loads a single plugin from an assembly file.
        /// </summary>
        private IPlugin? LoadPluginFromAssembly(string assemblyPath)
        {
            try
            {
                var assembly = Assembly.LoadFrom(assemblyPath);
                var pluginType = assembly.GetExportedTypes()
                    .FirstOrDefault(t => t.Name == PluginClassName);

                if (pluginType == null)
                {
                    return null;
                }

                return Activator.CreateInstance(pluginType) as IPlugin;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading plugin type from {assemblyPath}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the total number of discovered plugin assemblies.
        /// Useful for diagnostics and logging.
        /// </summary>
        public int GetPluginAssemblyCount()
        {
            try
            {
                return Directory
                    .EnumerateFiles(_pluginDirectoryPath, PluginSearchPattern, SearchOption.AllDirectories)
                    .Count();
            }
            catch
            {
                return 0;
            }
        }
    }
}
