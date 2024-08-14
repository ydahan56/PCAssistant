using CommandLine;
using FluentScheduler;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;

namespace Sdk.Base
{
    public abstract class Plugin : Registry, IPlugin, IJob
    {
        protected Action<ExecuteResult> ExecuteResultCallback;

        [Option("hours", HelpText = "Hours till command execution.")]
        public int Hours { get; set; }

        [Option("minutes", HelpText = "Minutes till command execution.")]
        public int Minutes { get; set; }

        [Option("seconds", HelpText = "Seconds till command execution.")]
        public int Seconds { get; set; }

        public abstract void Execute();

        public virtual void Initialize(IServiceLocator services) // todo - remove?
        {
            throw new NotImplementedException();
        }

        public virtual void SetExecuteResultCallback(Action<ExecuteResult> callback)
        {
            this.ExecuteResultCallback = callback;
        }

        public void SetExecutionSchedule()
        {
            // Hours or Minutes could be "00", still affects the same
            if (this.Hours > 0 || this.Minutes > 0)
            {
                this.Schedule(this).ToRunOnceAt(this.Hours, this.Minutes);

                return;
            }

            // seconds?
            if (this.Seconds > 0)
            {
                this.Schedule(this).ToRunOnceIn(this.Seconds).Seconds();

                return;
            }

            // no delay? execute now
            this.Schedule(this).ToRunNow();
        }

        public override string ToString()
        {
            return "";//$"*{this.Name + " " + this.Args.ToString()}* - {this.Description}";
        }
    }
}
