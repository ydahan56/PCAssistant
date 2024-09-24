using CommandLine;
using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using static display.Helpers.User32Helper;

namespace Plugins.Display
{
    [Verb("display", HelpText = "Control the state of the display adapter")]
    public class DllMain : Plugin
    {
        [Option("enabled", Required = true, HelpText = "'true' to turn on, otherwsie 'false' turn off")]
        public bool State { get; set; }

        public override void Execute()
        {
            var statusCode = PostMessage(
                HWND_BROADCAST, 
                WM_SYSCOMMAND, 
                SC_MONITORPOWER, 
                this.State ? -1 : 2
            );

            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    ErrorMessage = $"PostMessage returned with status code {statusCode}",
                    Success = true
                }
            );
        }
    }
}
