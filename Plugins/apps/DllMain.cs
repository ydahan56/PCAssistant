using apps.Helpers;
using CommandLine;
using Sdk.Models;
using Sdk.Plugins;

namespace apps
{
    [Verb("/apps", HelpText = "Check which apps are running on the workstation")]
    public class DllMain : Plugin
    {
        [Option("switch", Required = true, HelpText = "the type of apps, 'fg' for foreground or 'all'")]
        public string Switch { get; set; }

        public override void Execute()
        {
            var text = "";

            if (this.Switch.Equals("fg"))
            {
                var foreground = new ForegroundHelper();
                text = foreground.ToString();
            }
            else if (this.Switch.Equals("all"))
            {
                var background = new BackgroundHelper();
                text = background.ToString();
            }

            this.ExecuteContextCallback(new TextContext()
            {
                IsErrorSuccess = true,
                ErrorMessage = text,
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });
        }
    }
}