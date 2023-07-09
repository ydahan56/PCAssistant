namespace LanPlugin.Intranet
{
    public abstract class IInetScanner : IInetBase
    {
        protected const string scanPath = "..\\Plugins\\Lan\\Debug\\net6.0\\wnetscan.xml";

        public EventHandler<HostsArg> Discovered;

        protected void RaiseDiscovered(List<Host> hosts)
        {
            var arg = new HostsArg { Hosts = hosts, State = "Discovered" };
            Discovered?.Invoke(this, arg);
        }

        public abstract void Discover();
    }
}
