using apps.Helpers;
using CommandLine;
using Sdk.Plugins;
using Sdk.Models;

namespace Apps
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

            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    Success = true,
                    StatusText = text,
                    ResultType = ExecuteResultType.Text
                }
            );
        }
    }
}
