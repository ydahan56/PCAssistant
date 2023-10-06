using CommandLine;
using ORMi;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Models;

namespace Plugins.Dim
{
    public class DllMain : PluginBase
    {
        private class Brightness
        {
            [Value(0)]
            public byte Level { get; set; }
        }

        private ITGBotClient _client;
        private WmiMonitorBrightnessMethods _brightnessMethods;

        public DllMain()
        {
            this.Name = "/dim";
            this.ArgsPattern = "\\d{1,3}";
            this.HasArguments = true;
            this.Description = "Adjust workstation brightness.";
        }

        public override void Dispatch()
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(DispatchData data)
        {
            var args = Parser.Default.ParseArguments<Brightness>(data.Args);
            var level = args.Value.Level;

            var error = this._brightnessMethods.WmiSetBrightness(level);
            if (error == 0)
            {
                this._client.SendTextBackToAdmin($"Successfully adjusted brightness to {level}%.");
                return;
            }

            this._client.SendTextBackToAdmin($"WmiSetBrightness returned with an error {error}");
        }

        public override void Init(IDependencyService service)
        {
            this._client = service.ResolveInstance<ITGBotClient>();
            this._brightnessMethods = new WmiMonitorBrightnessMethods();
        }
    }

    [WMIClass("WmiMonitorBrightnessMethods")]
    internal class WmiMonitorBrightnessMethods : WMIInstance
    {
        public int WmiSetBrightness(byte level)
        {
            return WMIMethod.ExecuteMethod<int>(this, new { Timeout = 1, Brightness = level });
        }
    }
}
