using AutoUpdaterDotNET;
using CommandLine;
using Sdk.Base;
using Sdk.Hub;
using Sdk.Models;
using System.Resources;

namespace Update
{
    [Verb("update", HelpText = "This command allows to check or download an update")]
    public class DllMain : Plugin
    {
        private readonly ResourceManager _rm;

        [Value(0, Required = true, HelpText = "The command argument, either 'check' or 'download'")]
        public string UpdateCommand { get; set; }

        public DllMain()
        {
            this._rm = new ResourceManager(typeof(DllMain));
        }

        // this flag indicates whether we're allowed to download an update
        private bool _isDownloadEnabled;

        private void OnUpdateCheck(UpdateInfoEventArgs e)
        {
            if (e.Error is null)
            {
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

                        return;
                    }

                    this.ExecuteResultCallback(
                        new ExecuteResult()
                        {
                            StatusText = $"A new version {e.CurrentVersion} of Telebot is available!",
                            Success = true
                        }
                    );

                    return;
                }

                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = "You're currently running the latest version.",
                        Success = true
                    }
                );
            }
        }

        public override void Execute()
        {
            if (this.UpdateCommand.Equals("check"))
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

            if (this.UpdateCommand.Equals("download"))
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
