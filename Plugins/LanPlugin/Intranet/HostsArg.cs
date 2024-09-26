namespace LanPlugin.Intranet
{
    [XmlRoot(ElementName = "devices_connected_to_your_network")]
    public class HostsArg
    {
        [XmlElement(ElementName = "item")]
        public List<Host> Hosts { get; set; }

        public string State { get; set; }
    }
}