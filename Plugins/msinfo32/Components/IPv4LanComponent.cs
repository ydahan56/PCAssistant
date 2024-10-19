using System.Net;
using System.Net.Sockets;

namespace msinfo32.Components
{
    public class IPv4LanComponent : IComponent
    {
        public string GetInformation()
        {
            try
            {
                var _hostName = Dns.GetHostName();
                var _hostEntry = Dns.GetHostEntry(_hostName);

                // set default value
                var result = $"*LAN IPv4*: Error";

                foreach (var address in _hostEntry.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        result = $"*LAN IPv4*: {address}";
                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
