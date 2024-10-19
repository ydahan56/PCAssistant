using AutoUpdaterDotNET;
using CommandLine;
using Sdk.Plugins;
using Sdk.Hub;
using Sdk.Models;
using System.Resources;
using System.Reflection;

namespace update
{
    [Verb("/update", HelpText = "This command allows to check or download an update")]
    public class DllMain : Plugin
    {
        private readonly ResourceManager _rm;

        [Option("download", Required = false, HelpText = "Download an update")]
        public bool Download { get; set; }

        [Option("check", Required = false, HelpText = "Check for an update")]
        public bool Check {  get; set; }

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
                    this.ExecuteResultCallback(
                        new ExecuteResult()
                        {
                            StatusText = "PCAssistant is updating...",
                            Success = true
                        }
                    );

                    var updateSuccess = AutoUpdater.DownloadUpdate(e);

                    if (updateSuccess)
                    {
                        EventAggregator.Instance.MessageHub.Publish(ApplicationEvent.Exit);
                    }

                    // we don't need to reset flag, we restart the client anyways

                    return;
                }

                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = $"A new version {e.CurrentVersion} of PCAssistant is available!",
                        Success = true
                    }
                );

                return;
            }

            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = "You're currently running the latest version of PCAssistant.",
                    Success = true
                }
            );
        }

        public override void Execute()
        {
            if (this.Check)
            {
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = "Checking for updates...",
                        Success = true
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
    }
}
