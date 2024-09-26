using Agent.Helpers;
using DotNetEnv;
using FluentScheduler;
using Hardware;
using Sdk;
using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Hub;
using Sdk.Telegram;
using SimpleInjector;
using System.Reflection;
using Telegram.Bot.Types;

namespace Agent
{
    internal static class Program
    {
        public static IServiceLocator Services { get; private set; }

        [STAThread]
        static void Main()
        {
            Env.Load();

            EventAggregator.Instance.MessageHub
                .Subscribe<ApplicationEvent>(OnApplicationEvent);

            Services = new DependencyLocator(new Container());

            var token = Env.GetString("token");
            var whitelist = Env.GetString("whitelist")
                .Split(",")
                .Select(id => Convert.ToInt64(id))
                .Select(id => new ChatId(id))
                .ToList();

            // init cpuidsdk
            Cpuid64.Instance.InitSDK(PCManager.GetAppDirectory());

            // init telegram
            var telegram = new AssistantBot(token, whitelist);

            // init cpuid helper
            var cpuidHelper = new CpuidHelper();

            // read plugins
            var plugins = EnumPluginsInit();

            // register components
            RegisterComponents(cpuidHelper, plugins);

            // initialize plugins
            InitializePlugins(plugins);

            // start application's message loop
            Application.Run(new Main(telegram));

            // cleanup
            telegram.Cancel();
            JobManager.Stop();
            Cpuid64.Instance.Dispose();
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

        static void RegisterComponents(ICpuidHelper cpuid, List<IPlugin> items)
        {
            Services.RegisterInstance(cpuid);
            Services.RegisterInstances(items);

            //IOC.Verify();
        }

        static void InitializePlugins(List<IPlugin> items)
        {
            foreach (IPlugin item in items)
            {
                try
                {
                    item.Initialize(Services);
                }
                catch (NotImplementedException)
                {
                    // ignore
                }
            }
        }

        static List<IPlugin?> EnumPluginsInit()
        {
            // init list
            var list = new List<IPlugin?>();

            var pluginsDirPath = PCManager.Combine("Plugins");

            if (!Directory.Exists(pluginsDirPath))
            {
                throw new DirectoryNotFoundException(pluginsDirPath);
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