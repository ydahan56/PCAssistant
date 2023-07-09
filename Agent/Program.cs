using Hardware;
using DotNetEnv;
using FluentScheduler;
using Agent.Helpers;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Hub;
using SimpleInjector;
using System.Reflection;

namespace Agent
{
    internal static class Program
    {
        public static IDependencyService AppService { get; private set; }

        [STAThread]
        static void Main()
        {
            Env.Load();

            Mediator.Instance.MessageHub.Subscribe<ApplicationEvent>(OnApplicationEvent);

            AppService = new AppService(new Container());

            var token = Env.GetString("_token");
            var chatId = Convert.ToInt64(Env.GetString("_id"));

            var tgBotClient = new TGBotClient(token, chatId);
            var cpuidHelper = new CpuidHelper();
            var cts = new CancellationTokenSource();

            List<IPlugin> _Plugins;
            LoadPlugins(out _Plugins);

            RegisterComponents(tgBotClient, cpuidHelper, _Plugins);
            InitPlugins(_Plugins);

            Application.Run(new Main(tgBotClient, cts));

            cts.Cancel();
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
            ITGBotClient client,
            ICpuidHelper cpuidHelper,
            IEnumerable<IPlugin> plugins)
        {
            // instances
            AppService.RegisterInstance(client);
            AppService.RegisterInstance(cpuidHelper);

            // collections
            AppService.RegisterInstances(plugins);

            //IOC.Verify();
        }

        static void InitPlugins(List<IPlugin> _Plugins)
        {
            foreach (IPlugin _Plugin in _Plugins)
            {
                _Plugin.Init(AppService);
            }
        }

        static void LoadPlugins(out List<IPlugin> list)
        {
            // init list
            list = new List<IPlugin>();

            var path = Env.GetString("_path");

            var isNullOrWhitespace = string.IsNullOrWhiteSpace(path);
            var isDirectoryExist = Directory.Exists(path);

            if (isNullOrWhitespace || !isDirectoryExist)
            {
                return;
            }

            var dlls_path = Directory.EnumerateFiles(
                path, "*Plugin.dll", SearchOption.AllDirectories).ToList();

            if (dlls_path.Count == 0)
            {
                return;
            }

            foreach (string dll_path in dlls_path)
            {
                var assembly = Assembly.LoadFrom(dll_path);
                var typeName = assembly.ExportedTypes
                    .Single(x => x.Name.Equals("DllMain")).FullName;

                var instance = (IPlugin)assembly.CreateInstance(typeName!)!;

                // add to list
                list.Add(instance);
            }
        }
    }
}