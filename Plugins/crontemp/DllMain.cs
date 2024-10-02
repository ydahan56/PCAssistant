using CommandLine;
using crontemp.Enums;
using crontemp.Jobs;
using crontemp.Models;
using FluentScheduler;
using Sdk.Plugins;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Devices;
using Sdk.Models;
using System.Resources;
using System.Text;
using System.Reflection;

namespace crontemp
{
    [Verb("/crontemp", HelpText = "Monitor the temperature of the workstation")]
    public class DllMain : CronPlugin
    {
        private IEnumerable<IDevice> _devices;
        private ResourceManager _rm;

        public DllMain() : base(
            nameof(crontemp),
            nameof(CronTempJob)
            )
        {
            this._rm = new ResourceManager(
                "crontemp.Resource1", 
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
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = $"{this._nameOfClass} Job with id {this._cronJobId} is already running.",
                        Success = true
                    }
                );

                return;
            }

            // create new job
            var _cronJob = new CronTempJob(
                this._devices,
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
                        this._cronJobId,
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

            if (updateStatus == UpdateStatus.Append)
            {
                // perform append
                this.updateMessageBuilder.AppendLine(
                    string.Format(
                        this._rm.GetString("APPEND_ERRORMESSAGE"),
                        args.DeviceName,
                        args.Temperature
                    )
                );

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

        public override void Initialize(IServiceLocator service)
        {
            var CpuidHelper = service.ResolveInstance<ICpuidHelper>();

            // fill class field with devices
            this._devices = CpuidHelper
                .GetProcessors()
                .Concat(
                    CpuidHelper.GetDisplayAdapters()
                ).ToList();
        }
    }
}
