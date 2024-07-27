using Hardware;
using DotNetEnv;
using FluentScheduler;
using Agent.Helpers;
using Sdk.Telegram;
using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Hub;
using SimpleInjector;
using System.Reflection;
using Sdk.Telegram;
using Sdk.Dependencies;
using Sdk;

namespace Agent
{
    internal static class Program
    {
        public static IServiceLocator Services { get; private set; }
        public static Control UIThread { get; set; }

        [STAThread]
        static void Main()
        {
            Env.Load();

            EventAggregator.Instance.MessageHub.Subscribe<ApplicationEvent>(OnApplicationEvent);

            Services = new DependencyLocator(new Container());

            var token = Env.GetString("token");
            var whitelist = Env.GetString("whitelist")
                .Select(id => Convert.ToInt64(id))
                .ToList();

            var telegram = new AssistantBot(token, whitelist);
            var cpuidHelper = new CpuidHelper();

            var plugins = ReadAndCreatePlugins();
            RegisterComponents(telegram, cpuidHelper, plugins);
            InitializePlugins(plugins);

            Application.Run(new Main(telegram));

            telegram.Cancel();
            JobManager.Stop();
            Cpuid64.Instance.Sdk64.UninitSDK();
        }

        static void OnApplicationEvent(ApplicationEvent eventType)
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

        static void RegisterComponents(
            IPCAssistant client,
            ICpuidHelper cpuidHelper,
            IEnumerable<IPlugin> plugins)
        {
            // instances
            Services.RegisterInstance(client);
            Services.RegisterInstance(cpuidHelper);

            // collections
            Services.RegisterInstances(plugins);

            //IOC.Verify();
        }

        static void InitializePlugins(List<IPlugin> _Plugins)
        {
            foreach (IPlugin _Plugin in _Plugins)
            {
                try
                {
                    _Plugin.Initialize(Services);
                }
                catch (NotImplementedException)
                {
                    // ignore
                }
            }
        }

        static List<IPlugin?> ReadAndCreatePlugins()
        {
            // init list
            var list = new List<IPlugin?>();

            var pluginsDirPath = PCManager.Combine("Plugins");

            if (!Directory.Exists(pluginsDirPath))
            {
                return list;
            }

            var pluginsPaths = Directory.EnumerateFiles(
                pluginsDirPath,
                "*Plugin.dll",
                SearchOption.AllDirectories
            ).ToList();

            if (pluginsPaths.Count == 0)
            {
                return list;
            }

            list = pluginsPaths
                .Select(path =>
                {
                    return Assembly.LoadFrom(path).GetExportedTypes();
                })
                .Select(types =>
                {
                    return types.SingleOrDefault(type => type.Name == "DllMain");
                })
                .Select(type =>
                {
                    if (type == null)
                    {
                        return default;
                    }

                    return Activator.CreateInstance(type) as IPlugin;
                })
                .ToList();

            return list;
        }
    }
}