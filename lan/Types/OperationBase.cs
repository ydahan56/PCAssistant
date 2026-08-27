using lan.Models;
using System.Xml.Serialization;

namespace lan.Types
{
    /// <summary>
    /// Base class for LAN operations (scan, listen, etc.)
    /// Provides common functionality for network device discovery and monitoring.
    /// </summary>
    public abstract class OperationBase
    {
        protected readonly string DirectoryPath;
        protected readonly string ProgramPath;

        // Events for operation feedback
        public event Action<List<Host>>? DiscoveredEvent;
        public event Action<List<Host>>? ConnectedEvent;
        public event Action<List<Host>>? DisconnectedEvent;
        public event Action<string>? FeedbackEvent;

        protected OperationBase()
        {
            this.DirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            this.ProgramPath = Path.Combine(this.DirectoryPath, "wnet.exe");
        }

        /// <summary>
        /// Executes the operation (scan, listen, etc.)
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Combines a filename with the plugin directory path
        /// </summary>
        protected string CombineDirectory(string fileName)
        {
            return Path.Combine(this.DirectoryPath, fileName);
        }

        /// <summary>
        /// Reads XML output from wnet.exe and deserializes to Host objects
        /// </summary>
        protected List<Host> ReadHosts(string xmlPath)
        {
            try
            {
                if (!File.Exists(xmlPath))
                {
                    RaiseFeedback($"XML file not found: {xmlPath}");
                    return new List<Host>();
                }

                using var fileStream = new FileStream(xmlPath, FileMode.Open);
                var serializer = new XmlSerializer(typeof(HostsArg));
                var result = serializer.Deserialize(fileStream) as HostsArg;

                return result?.Hosts ?? new List<Host>();
            }
            catch (Exception ex)
            {
                RaiseFeedback($"Error reading hosts from XML: {ex.Message}");
                return new List<Host>();
            }
        }

        /// <summary>
        /// Raises the discovered event with the list of discovered hosts
        /// </summary>
        protected void RaiseDiscovered(List<Host> hosts)
        {
            DiscoveredEvent?.Invoke(hosts);
        }

        /// <summary>
        /// Raises the connected event with newly connected hosts
        /// </summary>
        protected void RaiseConnected(List<Host> hosts)
        {
            ConnectedEvent?.Invoke(hosts);
        }

        /// <summary>
        /// Raises the disconnected event with disconnected hosts
        /// </summary>
        protected void RaiseDisconnected(List<Host> hosts)
        {
            DisconnectedEvent?.Invoke(hosts);
        }

        /// <summary>
        /// Raises the feedback event with a status message
        /// </summary>
        protected void RaiseFeedback(string message)
        {
            FeedbackEvent?.Invoke(message);
        }

        /// <summary>
        /// Formats a list of hosts as a readable string
        /// </summary>
        protected string FormatHostList(List<Host> hosts)
        {
            if (hosts.Count == 0)
                return "No hosts found.";

            var lines = new System.Text.StringBuilder();
            lines.AppendLine($"Found {hosts.Count} host(s):");
            lines.AppendLine(new string('-', 50));

            foreach (var host in hosts)
            {
                lines.AppendLine($"IP: {host.Ip_address}");
                lines.AppendLine($"Device: {host.Device_name}");
                lines.AppendLine($"Info: {host.Device_information}");
                lines.AppendLine($"MAC: {host.Mac_address}");
                lines.AppendLine(new string('-', 50));
            }

            return lines.ToString();
        }
    }
}
