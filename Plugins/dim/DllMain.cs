using CommandLine;
using dim;
using Sdk;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Models;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Plugins.Dim
{
    public class DllMain : PluginBase
    {
        private class Brightness
        {
            [Value(0)]
            public int BrightnessLevel { get; set; }
        }

        private ITGBotClient _telegram;

        public DllMain()
        {
            this.Name = "/dim";
            this.ArgsPattern = "\\d{1,3}";
            this.Description = "Adjust workstation brightness.";
        }

        public override void Dispatch()
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(DispatchData data)
        {
            var args = Parser.Default.ParseArguments<Brightness>(data.Args);

            if (args.Value.BrightnessLevel < 1 || args.Value.BrightnessLevel > 100)
            {
                this._telegram.SendTextBackToAdmin("Argument must be between 1 to 100.");
                return;
            }

            var brightness = args.Value.BrightnessLevel;

            var scriptPath = PCManager.CombineExternal(Assembly.GetExecutingAssembly(), "execute.ps1");
            var result = new PowerShellBuilder()
                .BypassExecutionPolicy()
                .SetWindowStyle(PSWindowStyle.Hidden)
                .SetFileScriptPath(scriptPath)
                .SetArgument(brightness)
                .ExecuteScript();

            this._telegram.SendTextBackToAdmin(result);
        }

        public override void Init(IDependencyService service)
        {
            this._telegram = service.ResolveInstance<ITGBotClient>();
        }
    }
}
