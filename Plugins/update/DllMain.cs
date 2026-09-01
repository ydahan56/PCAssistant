using AutoUpdaterDotNET;
using CommandLine;
using Easy.MessageHub;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Hub;
using Sdk.Models;
using Sdk.Plugins;
using System.Reflection;
using System.Resources;

namespace update
{
    [Verb("/update", HelpText = "This command allows to check or download an update")]
    public class DllMain : Plugin
    {
        private IMessageHub _hub;
        private readonly ResourceManager _rm;

        [Option("download", Required = false, HelpText = "Download an update")]
        public bool Download { get; set; }

        [Option("check", Required = false, HelpText = "Check for an update")]
        public bool Check { get; set; }

        public DllMain()
        {
            this._rm = new ResourceManager(
                "update.Resource1",
                Assembly.GetExecutingAssembly()
            );
        }

        // this flag indicates whether we're allowed to download an update
        private bool _isDownloadEnabled;

        private void OnUpdateCheck(UpdateInfoEventArgs e)
        {
            if (e.Error is not null)
            {
                // todo - print error?
                return;
            }

            if (e.IsUpdateAvailable)
            {
                if (_isDownloadEnabled)
                {
                    this.ExecuteContextCallback(
                        new ExecuteContext()
                        {
                            ErrorMessage = "PCAssistant is updating...",
                            IsErrorSuccess = true
                        }
                    );

                    var updateSuccess = AutoUpdater.DownloadUpdate(e);

                    if (updateSuccess)
                    {
                        this._hub.Publish(ApplicationEvent.Exit);
                    }

                    // we don't need to reset flag, we restart the client anyways

                    return;
                }

                this.ExecuteContextCallback(
                    new ExecuteContext()
                    {
                        ErrorMessage = $"A new version {e.CurrentVersion} of PCAssistant is available!",
                        IsErrorSuccess = true
                    }
                );

                return;
            }

            this.ExecuteContextCallback(
                new ExecuteContext()
                {
                    ErrorMessage = "You're currently running the latest version of PCAssistant.",
                    IsErrorSuccess = true
                }
            );
        }

        public override void Execute()
        {
            if (this.Check)
            {
                this.ExecuteContextCallback(
                    new ExecuteContext()
                    {
                        ErrorMessage = "Checking for updates...",
                        IsErrorSuccess = true
                    }
                );

                // execute command
                AutoUpdater.Start();

                // exit
                return;
            }

            if (this.Download)
            {
                // update flag
                this._isDownloadEnabled = true;

                // execute command
                AutoUpdater.Start();

                return;
            }
        }

        public override IPlugin Initialize(IServiceResolver services)
        {
            this._hub = services.ResolveInstance<IMessageHub>();
            return this;
        }
    }
}
