using CommandLine;
using Sdk.Plugins;
using Sdk.Models;
using FluentScheduler;

namespace exit
{
    [Verb("/exit", HelpText = "Shutdown Agent")]
    public class DllMain : Plugin
    {
        public override void Execute()
        {
            var exit = new ExitJob();
            JobManager.Initialize(exit);

            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    Success = true,
                    StatusText = "PCAssistant is about to shutdown..",
                    ResultType = ExecuteResultType.Text
                }
            );
        }
    }
}
