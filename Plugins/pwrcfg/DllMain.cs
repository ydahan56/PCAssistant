using CommandLine;
using FluentScheduler;
using pwrcfg.Commands;
using pwrcfg.Jobs;
using Sdk.Models;
using Sdk.Plugins;

namespace pwrcfg
{
    [Verb("/pwrcfg", HelpText = "Lock, logoff, sleep, reboot or shutdown the workstation.")]
    public class DllMain : Plugin
    {

        [Option("state", Required = true, HelpText = "The desired workstation power state")]
        public string State { get; set; }

        [Option("timeout", Required = false, Default = 0, HelpText = "Timeout in seconds before execution")]
        public int Timeout { get; set; }

        public override void Execute()
        {
            this.ExecuteContextCallback(new TextContext()
            {
                ErrorMessage = $"Workstation is preparing to {this.State} within {this.Timeout} seconds..",
                IsErrorSuccess = true,
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });

            // create default registry
            Registry registry = new Registry();

            switch (this.State)
            {
                case "lock":
                    registry = new LockJob(this.Timeout);
                    break;
                case "logoff":
                    registry = new LogoffJob(this.Timeout);
                    break;
                case "sleep":
                    registry = new SleepJob(this.Timeout);
                    break;
                case "reboot":
                    registry = new RebootJob(this.Timeout);
                    break;
                case "shutdown":
                    registry = new ShutdownJob(this.Timeout);
                    break;
            }

            // execute query
            JobManager.Initialize(registry);
        }
    }
}
