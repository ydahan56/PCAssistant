using lan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace lan
{
    public class OperationBase
    {
        protected readonly string programuri;

        public OperationBase()
        {
            this.programuri = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wnet.exe");
        }

        protected List<Host> ReadHosts(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open);
            var serializer = new XmlSerializer(typeof(HostsArg));
            var arg = (HostsArg)serializer.Deserialize(fileStream);
            return arg.Hosts;
        }
    }
}
