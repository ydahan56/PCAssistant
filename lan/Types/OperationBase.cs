using lan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace lan.Types
{
    public abstract class OperationBase
    {
        protected readonly string directoryuri;
        protected readonly string programuri;

        protected OperationBase()
        {
            this.directoryuri = AppDomain.CurrentDomain.BaseDirectory;
            this.programuri = Path.Combine(this.directoryuri, "wnet.exe");
        }

        protected string CombineDirectory(string fileName)
        {
            return Path.Combine(this.directoryuri, fileName);
        }

        protected List<Host> ReadHosts(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open);
            var serializer = new XmlSerializer(typeof(HostsArg));
            var arg = (HostsArg)serializer.Deserialize(fileStream);
            return arg.Hosts;
        }

        public abstract void Execute();
    }
}
