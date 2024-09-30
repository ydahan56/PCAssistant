using CommandLine;
using FluentScheduler;
using Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Plugins
{
    public abstract class CronPlugin : Plugin
    {
        [Option("total", HelpText = "The total amount of time in seconds to run the cron")]
        public int Total { get; set; }

        [Option("timeout", HelpText = "The timeout in seconds between each execution")]
        public int Timeout { get; set; }

        [Option("stop", HelpText = "Sends a signal to cancel execution")]
        public bool Cancel { get; set; }


        protected readonly string _nameOfClass;
        protected readonly string _nameOfJob;

        protected CronPlugin(
            string nameofClass, 
            string nameofJob
            )
        {
            this._nameOfClass = nameofClass;
            this._nameOfJob = nameofJob;
        }

        // boolean flag to determine whether a Job exists
        protected bool IsCronJobActive => this._cronJobId > 0;

        // this is used to store the ID of existing Job
        protected int _cronJobId;

        protected void InternalCancelJob()
        {
            // remove Job
            JobManager.RemoveJob(this._nameOfJob);

            // update client
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = $"{this._nameOfClass} Job with id {this._cronJobId} has been cancelled.",
                    Success = true
                }
            );

            // reset Job id
            this._cronJobId = 0;
        }
    }
}
