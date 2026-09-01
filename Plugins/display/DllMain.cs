using CommandLine;
using Sdk.Models;
using Sdk.Plugins;
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

            this.ExecuteContext.ErrorMessage = $"PostMessage returned with status code {statusCode}";
            this.ExecuteContext.IsErrorSuccess = true;
            this.ExecuteContext.ResultType = ExecuteResultType.Text;
            this.ExecuteContextCallback(this.ExecuteContext);
        }
    }
}
