using CommandLine;
using Easy.MessageHub;
using Sdk.Contracts;
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
            this.ExecuteContextCallback(new TextContext()
            {
                IsErrorSuccess = true,
                ErrorMessage = "PCAssistant is about to shutdown..",
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });

            Task.Delay(2500).ContinueWith(async (s) =>
            {
                this._hub.Publish(ApplicationEvent.Exit);
            });
        }

        public override IPlugin Initialize(IServiceResolver services)
        {
            this._hub = services.ResolveInstance<IMessageHub>();
            return this;
        }
    }
}
