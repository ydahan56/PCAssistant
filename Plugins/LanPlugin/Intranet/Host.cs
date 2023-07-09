using System.Xml.Serialization;

namespace LanPlugin.Intranet
{
    [XmlRoot(ElementName = "item")]
    public class Host
    {
        [XmlElement(ElementName = "ip_address")]
        public string Ip_address { get; set; }

        [XmlElement(ElementName = "device_name")]
        public string Device_name { get; set; }

        [XmlElement(ElementName = "mac_address")]
        public string Mac_address { get; set; }

        [XmlElement(ElementName = "network_adapter_company")]
        public string Network_adapter_company { get; set; }

        [XmlElement(ElementName = "device_information")]
        public string Device_information { get; set; }

        [XmlElement(ElementName = "user_text")]
        public string User_text { get; set; }

        [XmlElement(ElementName = "first_detected_on")]
        public string First_detected_on { get; set; }

        [XmlElement(ElementName = "last_detected_on")]
        public string Last_detected_on { get; set; }

        [XmlElement(ElementName = "detection_count")]
        public string Detection_count { get; set; }

        [XmlElement(ElementName = "active")]
        public string Active { get; set; }

        public override string ToString()
        {
            string res = Ip_address;

            if (!string.IsNullOrEmpty(Device_information))
                res += $"\n{Device_information}";

            if (!string.IsNullOrEmpty(Device_name))
                res += $"\n{Device_name}";

            res += $"\n{Network_adapter_company}";

            return res;
        }
    }
}