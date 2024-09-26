using CommandLine;
using msinfo32.Components;
using Sdk;
using Sdk.Base;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Text;

namespace msinfo32
{
    [Verb("msinfo32", HelpText = "Print workstation information.")]
    public class DllMain : Plugin
    {
        private IEnumerable<IComponent> _components;

        public override void Execute()
        {
            var sb = new StringBuilder();

            foreach (IComponent _component in this._components)
            {
                sb.AppendLine(_component.GetInformation());
            }

            // get file path
            var filePath = PCManager.CombineAssembly(this.GetType().Assembly, "msinfo32.txt");

            // commit text to file
            System.IO.File.WriteAllText(filePath, sb.ToString());

            // obtain handle to file
            var fs = new FileStream(filePath, FileMode.Open);

            this.ExecuteResultCallback(
                new ExecuteStreamResult()
                {
                    FileName = Path.GetFileName(filePath),
                    Stream = fs,
                    Success = true
                }
            );

            // release fs
            fs.Close();
            fs.Dispose();
        }

        public override void Initialize(IServiceLocator service)
        {
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
