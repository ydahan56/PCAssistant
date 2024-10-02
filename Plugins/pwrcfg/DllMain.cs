using CommandLine;
using FluentScheduler;
using pwrcfg.Commands;
using pwrcfg.Jobs;
using Sdk.Plugins;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;

namespace pwrcfg
{
    [Verb("/pwrcfg", HelpText = "Lock, logoff, sleep, reboot or shutdown the workstation.")]
    public class DllMain : Plugin
    {

        [Option("state", Required = true, HelpText = "The desired workstation power state")]
        public string State { get; set; }

        [Option("timeout", Required = false, Default = 0)]
        public int Timeout { get; set; }

        public override void Execute()
        {
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = $"Workstation is preparing to {this.State} within {this.Timeout} seconds..",
                    Success = true
                }
            );

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
