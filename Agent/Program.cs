using Agent.Infrastructure;
using FluentScheduler;
using Sdk;
using Sdk.Dependencies;
using Sdk.Telegram;

namespace Agent
{
    internal static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        public static IServiceLocator Services { get; private set; }

        [STAThread]
        static void Main()
        {
            // Initialize the application using the bootstrapper
            var bootstrapper = new Bootstrapper();
            Services = bootstrapper.InitializeApplication();

            // Enable high DPI support for proper screen capture on high-resolution displays
            SetProcessDPIAware();

            // Get Telegram bot token
            var token = bootstrapper.GetTelegramToken();

            // Initialize Telegram client
            var telegram = new PCAssistantClient(token);

            try
            {
                // Start the application's main message loop
                Application.Run(new Main(telegram));
            }
            finally
            {
                // Cleanup resources
                telegram.Cancel();
                JobManager.Stop();
                bootstrapper.Shutdown();
            }
        }
    }
}
