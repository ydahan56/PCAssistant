using Sdk.Devices;
using System.Text;

namespace msinfo32.Components
{
    public class SYSTEM_INFO : IComponent
    {
        private readonly IEnumerable<IDevice> devices;

        public SYSTEM_INFO(IEnumerable<IDevice> devices)
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
