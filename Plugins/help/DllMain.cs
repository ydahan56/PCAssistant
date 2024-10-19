using CommandLine;
using Sdk.Plugins;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Text;
using System.Reflection;

namespace help
{
    [Verb("/help", HelpText = "List of available commands.")]
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
            var modules = service.ResolveInstances<IPlugin>();

            this.sb = new StringBuilder();

            // Iterate over each module (plugin)
            foreach (IPlugin module in modules)
            {
                var type = module.GetType();
                var verbAttr = type.GetCustomAttribute<VerbAttribute>();

                // Display the verb (command)
                if (verbAttr != null)
                {
                    sb.AppendLine($"{verbAttr.Name}: {verbAttr.HelpText}");

                    // Get and display the options for each verb
                    var options = type.GetProperties()
                        .Where(p => p.GetCustomAttribute<OptionAttribute>() != null);

                    foreach (var option in options)
                    {
                        var optionAttr = option.GetCustomAttribute<OptionAttribute>();
                        sb.AppendLine($"  --{optionAttr.LongName ?? optionAttr.ShortName.ToString()} ({optionAttr.HelpText})");
                    }
                }
            }
        }

    }
}
