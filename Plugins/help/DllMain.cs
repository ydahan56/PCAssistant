using CommandLine;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Plugins;
using System.Reflection;
using System.Text;

namespace help
{
    [Verb("/help", HelpText = "List of available commands.")]
    public class DllMain : Plugin
    {
        private StringBuilder builder;

        public override void Execute()
        {
            this.ExecuteContextCallback(new TextContext()
            {
                ErrorMessage = builder.ToString().TrimEnd(),
                IsErrorSuccess = true,
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });
        }

        public override IPlugin Initialize(IServiceResolver service)
        {
            var modules = service.ResolveInstances<IPlugin>();

            this.builder = new StringBuilder();

            // Iterate over each module (plugin)
            foreach (IPlugin module in modules)
            {
                var type = module.GetType();
                var verbAttr = type.GetCustomAttribute<VerbAttribute>();

                // Display the verb (command)
                if (verbAttr != null)
                {
                    builder.AppendLine($"{verbAttr.Name}: {verbAttr.HelpText}");

                    // Get and display the options for each verb
                    var options = type.GetProperties()
                        .Where(p => p.GetCustomAttribute<OptionAttribute>() != null);

                    foreach (var option in options)
                    {
                        var optionAttr = option.GetCustomAttribute<OptionAttribute>();
                        builder.AppendLine($"  --{optionAttr.LongName ?? optionAttr.ShortName.ToString()} ({optionAttr.HelpText})");
                    }
                }
            }

            return this;
        }

    }
}
