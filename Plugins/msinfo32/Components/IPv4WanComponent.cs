using Nito.AsyncEx;

namespace msinfo32.Components
{
    public class IPv4WanComponent : IComponent
    {
        public string GetInformation()
        {
            var _ipv4 = "*WAN IPv4*: Error";

            _ipv4 = AsyncContext.Run(async () =>
            {
                using HttpClient client = new HttpClient();

                var _ipAddr = await client.GetStringAsync("https://ipv4.icanhazip.com/");
                return _ipAddr.TrimEnd();
            });

            return $"*WAN IPv4*: {_ipv4}";
        }
    }
}
