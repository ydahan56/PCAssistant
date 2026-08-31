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

        [STAThread]
        static void Main()
        {
            System.Diagnostics.Debug.WriteLine("=== PCAssistant Agent Starting ===");

            // Enable high DPI support for proper screen capture on high-resolution displays
            SetProcessDPIAware();

            // Initialize the bootstrapper and application
            System.Diagnostics.Debug.WriteLine("Initializing bootstrapper...");
            var bootstrapper = new Bootstrapper();
            var services = bootstrapper.InitializeApplication();
            System.Diagnostics.Debug.WriteLine("✓ Application initialized successfully");

            try
            {
                // Resolve Main (ApplicationContext) from DI container
                System.Diagnostics.Debug.WriteLine("Resolving application context from DI...");
                var mainContext = services.ResolveInstance<Main>();
                System.Diagnostics.Debug.WriteLine("✓ Application context resolved");

                // Start the application's main message loop
                System.Diagnostics.Debug.WriteLine("Starting application message loop...");
                Application.Run(mainContext);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Fatal error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                throw;
            }
            finally
            {
                // Cleanup resources
                System.Diagnostics.Debug.WriteLine("Shutting down application...");
                JobManager.Stop();
                bootstrapper.Shutdown();
                System.Diagnostics.Debug.WriteLine("=== PCAssistant Agent Stopped ===");
            }
        }
    }
}
