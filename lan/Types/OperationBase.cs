using lan.Models;
using System.Text;
using System.Xml.Serialization;

namespace lan.Types
{
    public abstract class OperationBase
    {
        protected readonly string directoryuri;
        protected readonly string programuri;
        protected readonly string scanPath;

        protected OperationBase()
        {
            this.directoryuri = AppDomain.CurrentDomain.BaseDirectory;
            this.programuri = Path.Combine(this.directoryuri, "wnet.exe");
            this.scanPath = Path.Combine(this.directoryuri, "networkscan.xml");
        }

        protected string CombineDirectory(string fileName)
        {
            return Path.Combine(this.directoryuri, fileName);
        }

        protected List<Host> ReadHosts(string path)
        {
            if (!File.Exists(path))
                return new List<Host>();

            try
            {
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var serializer = new XmlSerializer(typeof(HostsArg));
                var arg = (HostsArg)serializer.Deserialize(fileStream);
                return arg?.Hosts ?? new List<Host>();
            }
            catch
            {
                return new List<Host>();
            }
        }

        protected void RaiseDiscovered(List<Host> hosts)
        {
            if (hosts == null || hosts.Count == 0)
            {
                RaiseFeedback("No devices found on the network.");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"📡 Found {hosts.Count} device(s) on the network:");
            sb.AppendLine();

            foreach (var host in hosts)
            {
                sb.AppendLine($"🖥️ {host.Device_name ?? "Unknown"}");
                sb.AppendLine($"   IP: {host.Ip_address}");
                if (!string.IsNullOrWhiteSpace(host.Mac_address))
                    sb.AppendLine($"   MAC: {host.Mac_address}");
                if (!string.IsNullOrWhiteSpace(host.Network_adapter_company))
                    sb.AppendLine($"   Vendor: {host.Network_adapter_company}");
                sb.AppendLine();
            }

            RaiseFeedback(sb.ToString());
        }

        protected void RaiseConnected(List<Host> hosts)
        {
            if (hosts == null || hosts.Count == 0)
                return;

            var sb = new StringBuilder();
            sb.AppendLine($"✅ {hosts.Count} device(s) connected:");
            foreach (var host in hosts)
            {
                sb.AppendLine($"  • {host.Device_name ?? host.Ip_address}");
            }

            RaiseFeedback(sb.ToString());
        }

        protected void RaiseDisconnected(List<Host> hosts)
        {
            if (hosts == null || hosts.Count == 0)
                return;

            var sb = new StringBuilder();
            sb.AppendLine($"❌ {hosts.Count} device(s) disconnected:");
            foreach (var host in hosts)
            {
                sb.AppendLine($"  • {host.Device_name ?? host.Ip_address}");
            }

            RaiseFeedback(sb.ToString());
        }

        protected abstract void RaiseFeedback(string message);

        public abstract void Execute();
    }
}
