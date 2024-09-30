using CommandLine;
using croncap.Jobs;
using croncap.Models;
using croncap.Enums;
using FluentScheduler;
using Sdk.Plugins;
using Sdk.Models;
using System.Resources;
using System.Text;

namespace croncap
{
    [Verb("croncap", HelpText = "Schedules screen capture session.")]
    public class DllMain : CronPlugin
    {
        private readonly ResourceManager _rm;

        public DllMain() : base(
            nameof(croncap),
            nameof(CronCapJob)
            )
        {
            this._rm = new ResourceManager(typeof(DllMain));
        }

        public override void Execute()
        {
            if (this.Cancel)
            {
                if (this.IsCronJobActive)
                {
                    // perform cancellation
                    this.InternalCancelJob();
                }

                // we're done processing cancellation
                return;
            }

            // check if an existing Job is already running
            if (this.IsCronJobActive)
            {
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = $"crontemp Job with id {this._cronJobId} is already running.",
                        Success = true
                    }
                );

                return;
            }

            // create new job
            var _cronJob = new CronCapJob(
                this.OnJobUpdate,
                this.Total,
                this.Timeout
            );

            // update Job's ID
            this._cronJobId = _cronJob.GetHashCode();

            // fire Job
            JobManager.Initialize(_cronJob);

            // update client
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = string.Format(
                        this._rm.GetString("SUCCESS_ERRORMESSAGE"),
                        this.Total,
                        this.Timeout
                    ),
                    Success = true
                }
            );
        }

        private StringBuilder updateMessageBuilder = new StringBuilder();

        private void OnJobUpdate(UpdateStatus updateStatus, UpdateArgs? args)
        {
            if (updateStatus == UpdateStatus.Elapsed)
            {
                // perform cancellation
                this.InternalCancelJob();

                // exit
                return;
            }

            if (updateStatus == UpdateStatus.Send)
            {
                // add finalize text
                this.updateMessageBuilder.AppendLine("\nFrom *PCAssistant*");

                // update client
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = this.updateMessageBuilder.ToString(),
                        Success = true
                    }
                );

                // clear previous instance
                this.updateMessageBuilder = new StringBuilder();

                // exit
                return;
            }
        }

        public async Task<string> GetStatus()
        {
            return $"*Cap Time*: {worker.Active.ToReadable()}";
        }
    }
}
