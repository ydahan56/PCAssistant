using CommandLine;
using Sdk;
using Sdk.Models;
using Sdk.Plugins;
using System.Reflection;


namespace dim
{
    [Verb("/brightness", HelpText = "Adjust Workstation Brightness.")]
    public class DllMain : Plugin
    {

        [Option("value", Required = true, HelpText = "Brightness Level (1-100)")]
        public int BrightnessValue { get; set; }

        public DllMain()
        {
            // this.Schedule(this).ToRunNow();
        }

        public override void Execute()
        {
            var scriptPath = PCManager.CombineAssembly(
                Assembly.GetExecutingAssembly(),
                "execute.ps1"
            );

            var result = new PowerShellBuilder()
                .BypassExecutionPolicy()
                .SetWindowStyle(PSWindowStyle.Hidden)
                .SetFileScriptPath(scriptPath)
                .SetArgument(this.BrightnessValue)
                .ExecuteScript();

            this.ExecuteContext.ErrorMessage = result;
            this.ExecuteContext.IsErrorSuccess = true;
            this.ExecuteContextCallback(this.ExecuteContext);
        }
    }
}
