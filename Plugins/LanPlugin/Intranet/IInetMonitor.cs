namespace LanPlugin.Intranet
{
    public abstract class IINetMonitor : IInetBase
    {
        protected const string scanPath = "..\\Plugins\\Lan\\Debug\\net6.0\\wnetmon.xml";

        public EventHandler<HostsArg> Connected;
        public EventHandler<HostsArg> Disconnected;

        public bool Active { get; protected set; }

        protected void RaiseConnected(List<Host> hosts)
        {
            var arg = new HostsArg { Hosts = hosts, State = "Connected" };
            Connected?.Invoke(this, arg);
        }

        protected void RaiseDisconnected(List<Host> hosts)
        {
            var arg = new HostsArg { Hosts = hosts, State = "Disconnected" };
            Disconnected?.Invoke(this, arg);
        }

        public abstract void Listen();
        public abstract void Disconnect();
    }
}