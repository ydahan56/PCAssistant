using Sdk.Devices;
using System.Text;

namespace msinfo32.Components
{
    public class SystemComponent : IComponent
    {
        private readonly IEnumerable<IDevice> devices;

        public SystemComponent(IEnumerable<IDevice> devices)
        {
            this.devices = devices;
        }

        public string GetInformation()
        {
            var sb = new StringBuilder();

            foreach (IDevice device in devices)
            {
                sb.AppendLine(device.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
