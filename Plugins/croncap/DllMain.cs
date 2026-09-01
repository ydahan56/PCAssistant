using CommandLine;
using croncap.Enums;
using croncap.Jobs;
using croncap.Models;
using FluentScheduler;
using Sdk.Models;
using Sdk.Plugins;
using System.Reflection;
using System.Resources;
using System.Text;

namespace croncap
{
    [Verb("/croncap", HelpText = "Schedules screen capture session.")]
    public class DllMain : CronPlugin
    {
        private readonly ResourceManager _rm;

        public DllMain() : base(
            nameof(croncap),
            nameof(CronCapJob)
            )
        {
            this._rm = new ResourceManager(
                "croncap.Resource1",
                Assembly.GetExecutingAssembly()
            );
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
                this.ExecuteContextCallback(
                    new ExecuteContext()
                    {
                        ErrorMessage = $"{this._nameOfClass} Job with id {this._cronJobId} is already running.",
                        IsErrorSuccess = true
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

            this.ExecuteContext.ErrorMessage = string.Format(
                this._rm.GetString("SUCCESS_ERRORMESSAGE"),
                this._cronJobId, this.Total, this.Timeout
            );
            this.ExecuteContext.IsErrorSuccess = true;
            this.ExecuteContextCallback(this.ExecuteContext);
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

                // create stream
                var ms = new MemoryStream();

                // save to stream
                args.DesktopScreen.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                // seek to begin
                ms.Position = 0;

                // create file name
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png";

                // update client
                this.ExecuteContextCallback(
                    new ImageContext()
                    {
                        FileName = fileName,
                        Stream = ms,
                        ErrorMessage = this.updateMessageBuilder.ToString(),
                        IsErrorSuccess = true
                    }
                );

                // clear previous instance
                this.updateMessageBuilder = new StringBuilder();

                // exit
                return;
            }
        }
    }
}
