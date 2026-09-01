using Agent.Hardware;
using Agent.Notification;
using Agent.Startup;
using DotNetEnv;
using Easy.MessageHub;
using FluentScheduler;
using Hardware;
using Sdk;
using Sdk.Contracts;
using Sdk.Telegram;
using SimpleInjector;
using System.Reflection;
using Telegram.Bot.Polling;

namespace Agent
{
    public static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        public static Container IOC = new Container();


        [STAThread]
        static void Main()
        {
            Env.Load();

            // to capture the entire screen on high DPI computers
            SetProcessDPIAware();

            Cpuid64.Instance.InitSDK(PCManager.GetAppDirectory());

            IOC.RegisterSingleton<Main>();
            IOC.RegisterSingleton<IBootstrapper, Bootstrapper>();
            IOC.RegisterSingleton<IMessageHub, MessageHub>();
            IOC.RegisterSingleton<IUpdateHandler, AgentUpdateHandler>();
            IOC.RegisterSingleton<INotificationHandler, NotificationHandler>();
            IOC.RegisterSingleton<IPCAssistant>(() =>
            {
                var token = Env.GetString("token");
                return new PCAssistantClient(token);
            });
            IOC.RegisterSingleton<IHardwareCapability>(() => new HardwareCapability());

            var modules = EnumeratePlugins();
            IOC.Collection.Register<IPlugin>(modules);

            var mainContext = IOC.GetInstance<Main>();
            //IOC.Verify();

            Application.Run(mainContext);

            JobManager.StopAndBlock();
            Cpuid64.Instance.Dispose();
        }

        static List<IPlugin?> EnumeratePlugins()
        {
            // init list
            var list = new List<IPlugin?>();

            var pluginsDirPath = PCManager.Combine("..\\Plugins");

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