using Microsoft.VisualBasic.ApplicationServices;
using FluentScheduler;
using Hardware;
using Sdk;
using Sdk.Dependencies;
using Sdk.Telegram;
using Telegram.Bot;

namespace Agent
{
    /// <summary>
    /// Application context for the WinForms tray application.
    /// Manages the system tray icon and initializes startup tasks.
    /// </summary>
    public class Main : ApplicationContext
    {
        private readonly NotifyIcon _tray;

        public Main(IPCAssistant client, IServiceLocator services)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            // Initialize system tray
            _tray = new NotifyIcon()
            {
                Icon = new Icon(PCManager.Combine("icon.ico")),
                Text = "PCAssistant",
                Visible = true
            };

            // Create and initialize the startup sequence
            var startup = new StartupSequence(client, _tray, services);

            // Schedule startup tasks with FluentScheduler
            JobManager.Initialize(startup, Cpuid64.Instance.GetRefreshJob());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tray?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Manages the application startup sequence.
    /// Handles Telegram connection initialization and UI updates.
    /// </summary>
    internal class StartupSequence : Registry
    {
        private readonly IPCAssistant _client;
        private readonly NotifyIcon _tray;
        private readonly IServiceLocator _services;

        public StartupSequence(IPCAssistant client, NotifyIcon tray, IServiceLocator services)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tray = tray ?? throw new ArgumentNullException(nameof(tray));
            _services = services ?? throw new ArgumentNullException(nameof(services));

            // Schedule startup tasks
            this.Schedule(this.InitializeTelegramConnection).ToRunOnceIn(5).Seconds();
            this.Schedule(this.UpdateTrayCaption).ToRunOnceIn(2).Seconds();
        }

        /// <summary>
        /// Initializes the Telegram bot connection and starts receiving updates.
        /// </summary>
        private void InitializeTelegramConnection()
        {
            try
            {
                // Resolve update handler from DI container
                var updateHandler = _services.ResolveInstance<AgentUpdateHandler>();

                // Start receiving updates from Telegram
                _client.StartReceiving(updateHandler);

                System.Diagnostics.Debug.WriteLine("Telegram connection initialized successfully.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing Telegram connection: {ex.Message}");
                _tray.ShowBalloonTip(3000, _tray.Text, "Failed to connect to Telegram.", ToolTipIcon.Error);
            }
        }

        /// <summary>
        /// Updates the tray icon caption with the bot's username.
        /// </summary>
        private void UpdateTrayCaption()
        {
            try
            {
                // Get bot information asynchronously
                var user = Nito.AsyncEx.AsyncContext.Run(async () => await _client.GetMe());

                // Update tray label with username
                if (user?.Username != null)
                {
                    _tray.Text += $" - {user.Username}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating tray caption: {ex.Message}");
            }
        }
    }
}
