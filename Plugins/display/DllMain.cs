using CommandLine;
using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using static display.Helpers.User32Helper;

namespace Plugins.Display
{
    public class DllMain : Plugin
    {
        private class DisplayArg
        {
            [Value(0)]
            public string State { get; set; }
        }

        private IPCAssistant _telegram;
        private Dictionary<string, int> _dict;

        public DllMain()
        {
            this.Name = "/display";
            this.Args = "on|off";
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

        public override void Dispatch(ExecuteResult data)
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

        public override void Initialize(IServiceLocator service)
        {
            this._telegram = service.ResolveInstance<IPCAssistant>();
        }
    }
}
