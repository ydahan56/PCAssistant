using Sdk.Base;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Text;

namespace Plugins.Help
{
    public class DllMain : Plugin
    {
        private string text;
        private IPCAssistant _telegram;

        public DllMain()
        {
            this.Name = "/help";
            this.Description = "List of available commands.";
        }

        public override void Dispatch()
        {
            this._telegram.SendTextBackToAdmin(this.text);
        }

        public override void Dispatch(ExecuteResult data)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IServiceLocator service)
        {
            StringBuilder builder = new();
            this._telegram = service.ResolveInstance<IPCAssistant>();
            var _Plugins = service.ResolveInstances<IPlugin>();

            foreach (IPlugin _P in _Plugins)
            {
                builder.AppendLine(_P.ToString());
            }

            this.text = builder.ToString().TrimEnd();
        }
    }
}
