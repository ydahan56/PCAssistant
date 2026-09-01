using CommandLine;
using Easy.MessageHub;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Hub;
using Sdk.Models;
using Sdk.Plugins;

namespace New
{
    [Verb("/new", HelpText = "Create a new instance of PCAssistant.")]
    public class DllMain : Plugin
    {
        private IMessageHub _hub;
        public override void Execute()
        {
            this.ExecuteContext.ErrorMessage = "PCAssistant is restarting...";
            this.ExecuteContext.IsErrorSuccess = true;
            this.ExecuteContext.ResultType = ExecuteResultType.Text;
            this.ExecuteContextCallback(this.ExecuteContext);

            // we run the job in 5 seconds to allow
            // the bot client to observe the message
            // to prevent it from going in an endless loop

            Task.Delay(2500).ContinueWith((s) =>
            {
                this._hub.Publish(ApplicationEvent.Restart);
            });
        }

        public override IPlugin Initialize(IServiceResolver services)
        {
            this._hub = services.ResolveInstance<IMessageHub>();
            return this;
        }
    }
}
