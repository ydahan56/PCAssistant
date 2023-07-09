using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Models;
using System.Text;

namespace Plugins.Help
{
    public class DllMain : PluginBase
    {
        private string text;
        private ITGBotClient _client;

        public DllMain()
        {
            this.Name = "/help";
            this.HasArguments = false;
            this.Description = "List of available commands.";
        }

        public override void Dispatch()
        {
            this._client.SendText(this.text);
        }

        public override void Dispatch(DispatchData data)
        {
            throw new NotImplementedException();
        }

        public override void Init(IDependencyService service)
        {
            StringBuilder builder = new();
            this._client = service.ResolveInstance<ITGBotClient>();
            var _Plugins = service.ResolveInstances<IPlugin>();

            foreach (IPlugin _P in _Plugins)
            {
                builder.AppendLine(_P.ToString());
            }

            this.text = builder.ToString();
        }
    }
}
