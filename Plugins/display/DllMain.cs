using CommandLine;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Models;
using static display.Helpers.User32Helper;

namespace Plugins.Display
{
    public class DllMain : PluginBase
    {
        private class DisplayArg
        {
            [Value(0)]
            public string State { get; set; }
        }

        private ITGBotClient _telegram;
        private Dictionary<string, int> _dict;

        public DllMain()
        {
            this.Name = "/display";
            this.ArgsPattern = "on|off";
            this.Description = "Turn the display on or off.";

            this._dict = new Dictionary<string, int>()
            {
                { "on", -1 },
                { "off", 2 }
            };
        }

        public override void Dispatch()
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(DispatchData data)
        {
            var args = Parser.Default.ParseArguments<DisplayArg>(data.Args).Value;

            var success = this._dict.TryGetValue(args.State, out int lparam);
            if (!success)
            {
                this._telegram.SendTextBackToAdmin($"state {args.State} does not exist.");
                return;
            }

            var error = PostMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, lparam);
            this._telegram.SendTextBackToAdmin($"error returned with exit code {error}");
        }

        public override void Init(IDependencyService service)
        {
            this._telegram = service.ResolveInstance<ITGBotClient>();
        }
    }
}
