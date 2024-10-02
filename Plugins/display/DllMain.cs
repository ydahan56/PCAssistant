using CommandLine;
using Sdk.Plugins;
using Sdk.Models;
using static display.Helpers.User32Helper;

namespace display
{
    [Verb("/display", HelpText = "Control the state of the display adapter")]
    public class DllMain : Plugin
    {
        [Option("enabled", Required = true, HelpText = "Turn the display on or off (true|false)")]
        public string Enabled { get; set; }

        public override void Execute()
        {

            var statusCode = PostMessage(
                HWND_BROADCAST,
                WM_SYSCOMMAND,
                SC_MONITORPOWER,
                Convert.ToBoolean(this.Enabled) ? -1 : 2
            );

            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = $"PostMessage returned with status code {statusCode}",
                    Success = true
                }
            );
        }
    }
}
