using CommandLine;
using Sdk.Plugins;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Text;

namespace help
{
    [Verb("/+help", HelpText = "List of available commands.")]
    public class DllMain : Plugin
    {
        private StringBuilder sb;

        public override void Execute()
        {
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = sb.ToString().TrimEnd()
                }
            );
        }

        public override void Initialize(IServiceLocator service)
        {
            this.sb = new StringBuilder();

            var Modules = service.ResolveInstances<IPlugin>();

            foreach (IPlugin Module in Modules)
            {
                sb.AppendLine(Module.ToString());
            }
        }
    }
}
