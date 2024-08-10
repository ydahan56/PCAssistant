using CommandLine;
using dim;
using Sdk;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Models;
using System.Reflection;


namespace Plugins.Dim
{
    [Verb("/dim", HelpText = "Adjust Workstation Brightness.")]
    public class DllMain : Plugin
    {
        [Value(0, Required = true, HelpText = "Brightness Level (1-100)")]
        public int BrightnessLevel { get; set; }

        public DllMain()
        {
            this.Schedule(this).ToRunNow();
        }

        public override void Execute()
        {
            var scriptPath = PCManager.CombineExternal(Assembly.GetExecutingAssembly(), "execute.ps1");

            var result = new PowerShellBuilder()
                .BypassExecutionPolicy()
                .SetWindowStyle(PSWindowStyle.Hidden)
                .SetFileScriptPath(scriptPath)
                .SetArgument(this.BrightnessLevel)
                .ExecuteScript();

            this.ExecuteResultCallback?.Invoke(
                new ExecuteResult()
                {
                    ErrorMessage = result,
                    Success = true
                }
            );
        }
    }
}
