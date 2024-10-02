using CommandLine;
using FluentScheduler;
using New.Jobs;
using Sdk.Plugins;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;

namespace New
{
    [Verb("/new", HelpText = "Create a new instance of PCAssistant.")]
    public class DllMain : Plugin
    {
        public override void Execute()
        {
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = "PCAssistant is restarting...",
                    Success = true
                }
            );

            // we run the job in 5 seconds to allow
            // the bot client to observe the message
            // to prevent it from going in an endless loop

            var Job = new RestartJob();
            JobManager.Initialize(Job);
        }
    }
}
