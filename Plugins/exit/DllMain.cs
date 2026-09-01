using CommandLine;
using FluentScheduler;
using Sdk.Models;
using Sdk.Plugins;

namespace exit
{
    [Verb("/exit", HelpText = "Shutdown Agent")]
    public class DllMain : Plugin
    {
        public override void Execute()
        {
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    Success = true,
                    StatusText = "PCAssistant is about to shutdown..",
                    ResultType = ExecuteResultType.Text
                }
            );

            var exit = new ExitJob();
            JobManager.Initialize(exit);
        }
    }
}
