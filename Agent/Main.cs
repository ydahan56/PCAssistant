using Microsoft.VisualBasic.ApplicationServices;
using FluentScheduler;
using Hardware;
using Sdk;
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
        public Main(IPCAssistant client)
        {
            // Initialize system tray
            var tray = new NotifyIcon()
            {
                Icon = new Icon(PCManager.Combine("icon.ico")),
                Text = "PCAssistant",
                Visible = true
            };

            // Create and initialize the startup sequence
            var startup = new StartupSequence(client, tray);

            // Schedule startup tasks with FluentScheduler
            JobManager.Initialize(startup, Cpuid64.Instance.GetRefreshJob());
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

        public StartupSequence(IPCAssistant client, NotifyIcon tray)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _tray = tray ?? throw new ArgumentNullException(nameof(tray));

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
                // Create update handler with reference to services
                var updateHandler = new AgentUpdateHandler(_tray, _client, Program.Services);

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
