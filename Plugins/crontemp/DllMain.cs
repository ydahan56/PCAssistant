using CommandLine;
using crontemp.Enums;
using crontemp.Jobs;
using crontemp.Models;
using FluentScheduler;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Devices;
using Sdk.Extensions;
using Sdk.Models;
using Sdk.Telegram;
using System.Resources;
using System.Text;

namespace crontemp
{
    [Verb("crontemp", HelpText = "Monitor the temperature of the workstation")]
    public class DllMain : Plugin
    {
        [Option("total", HelpText = "The total amount of time in seconds to run the cron")]
        public int Total { get; set; }

        [Option("timeout", HelpText = "The timeout in seconds between each execution")]
        public int Timeout { get; set; }

        [Option("stop", HelpText = "Sends a signal to cancel execution")]
        public bool Cancel { get; set; }


        private readonly string _name;
        private IEnumerable<IDevice> _devices;

        private readonly ResourceManager _rm;

        public DllMain()
        {
            this._name = nameof(CronTempJob);
            this._rm = new ResourceManager(
                "crontemp.Resource1", 
                this.GetType().Assembly
            );
        }

        // boolean flag to determine whether a Job exists
        private bool IsCronJobActive => this._cronJobId > 0;

        // this is used to store the ID of existing Job
        private int _cronJobId;

        private void InternalCancelJob()
        {
            // remove Job
            JobManager.RemoveJob(this._name);

            // update client
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    ErrorMessage = $"crontemp Job with id {this._cronJobId} has been cancelled.",
                    Success = true
                }
            );

            // reset Job id
            this._cronJobId = 0;
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
                        ErrorMessage = $"crontemp Job with id {this._cronJobId} is already running.",
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
                    ErrorMessage = string.Format(
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

            if (updateStatus == UpdateStatus.Append)
            {
                // perform append
                this.updateMessageBuilder.AppendLine(
                    string.Format(
                        this._rm.GetString("UPDATE_ERRORMESSAGE"),
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
                this.updateMessageBuilder.AppendLine("\nFrom *Telebot*");

                // update client
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        ErrorMessage = this.updateMessageBuilder.ToString(),
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

        public string GetStatus()
        {
            return ""; // $"*{base.Name}*: {(this._state == JobState.Started).ToReadable()}";
        }
    }
}
