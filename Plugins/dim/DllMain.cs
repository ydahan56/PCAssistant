using CommandLine;
using dim;
using Sdk;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Plugins.Dim
{
    public class DllMain : Plugin
    {
        [Verb("dim", HelpText = "Adjust Workstation Brightness.")]
        public class Options
        {
            [Option("level", Required = true, HelpText = "Brightness Level", Min = 1, Max = 100)]
            public int BrightnessLevel { get; set; }
        }

        public Options Args { get; private set; }

        public DllMain(Options args)
        {
            this.Args = args;
            this.Schedule(this).ToRunNow();
        }

        // Default ctor for reflection
        public DllMain()
        {
            
        }

        public override (bool success, Plugin? result) TryGetPlugin(string[] args)
        {
            if (Parser.Default.ParseArguments<Options>(args) is Parsed<Options> parsed)
            {
                var instance = new DllMain(parsed.Value);
                return (true, instance);
            }

            return (false, null);
        }

        public override void Execute()
        {
            var scriptPath = PCManager.CombineExternal(Assembly.GetExecutingAssembly(), "execute.ps1");

            var result = new PowerShellBuilder()
                .BypassExecutionPolicy()
                .SetWindowStyle(PSWindowStyle.Hidden)
                .SetFileScriptPath(scriptPath)
                .SetArgument(this.Args.BrightnessLevel)
                .ExecuteScript();

            this.ExecuteResultCallback?.Invoke(new ExecuteResult()
            {
                ErrorMessage = result,
                Success = true
            });
        }
    }
}
