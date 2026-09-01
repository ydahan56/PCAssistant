using Agent.Hardware;
using Agent.Notification;
using Agent.Startup;
using Common.Queue;
using DotNetEnv;
using Easy.MessageHub;
using FluentScheduler;
using Hardware;
using Sdk;
using Sdk.Contracts;
using Sdk.Models;
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
            IOC.RegisterSingleton<ISimpleMessageQueue<ExecuteContext>, ExecutionMessageQueue>();

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

            var moduleDirectory = PCManager.Combine("..\\Plugins");

            if (!Directory.Exists(moduleDirectory))
            {
                throw new DirectoryNotFoundException(moduleDirectory);
            }

            var Modules = Directory.EnumerateFiles(
                moduleDirectory,
                "*Plugin.dll",
                SearchOption.AllDirectories
            ).ToList();

            if (Modules.Count == 0)
            {
                return list;
            }

            list = Modules.Select(LoadAssembly).Select(FindEntrypoint).Select(CreateInstance).ToList();
            return list;
        }

        static Type[] LoadAssembly(string path)
        {
            var assembly = Assembly.LoadFrom(path);
            return assembly.GetExportedTypes();
        }

        static Type? FindEntrypoint(Type[] types)
        {
            return types.SingleOrDefault(type => type.Name == "DllMain");
        }

        static IPlugin? CreateInstance(Type? type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            var instance = Activator.CreateInstance(type) as IPlugin;
            if (instance == null)
            {
                throw new InvalidOperationException($"Failed to create an instance of type {type.FullName}.");
            }
            return instance;
        }
    }
}