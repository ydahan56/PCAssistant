using CommandLine;
using Easy.MessageHub;
using FluentScheduler;
using Sdk.Dependencies;
using Sdk.Hub;
using Sdk.Models;
using Sdk.Plugins;

namespace exit
{
    [Verb("/exit", HelpText = "Shutdown Agent")]
    public class DllMain : Plugin
    {
        private IMessageHub _hub;
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

            Task.Delay(2500).ContinueWith(async (s) =>
            {
                this._hub.Publish(ApplicationEvent.Exit);
            });
        }

        public override void Initialize(IServiceResolver services)
        {
            this._hub = services.ResolveInstance<IMessageHub>();
        }
    }
}
