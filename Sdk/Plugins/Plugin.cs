using CommandLine;
using FluentScheduler;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;

namespace Sdk.Plugins
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

        public virtual void Initialize(IServiceResolver services)
        {
            // throw new NotImplementedException();
        }

        public virtual void SetExecuteResultCallback(Action<ExecuteResult> callback)
        {
            this.ExecuteResultCallback = callback;
        }

        public void SetExecutionSchedule()
        {
            DateTime now = DateTime.Now;

            if (this.Hours > 0)
            {
                now = now.AddHours(this.Hours);
            }

            if (this.Minutes > 0)
            {
                now = now.AddMinutes(this.Minutes);
            }

            if (this.Seconds > 0)
            {
                now = now.AddSeconds(this.Seconds);
            }

            // no delay? execute now
            this.Schedule(this).ToRunOnceAt(now);
        }
    }
}
