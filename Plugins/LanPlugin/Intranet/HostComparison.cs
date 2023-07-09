namespace LanPlugin.Intranet
{
    public class HostComparison : IEqualityComparer<Host>
    {
        public bool Equals(Host x, Host y)
        {
            return x.Mac_address.Equals(y.Mac_address);
        }

        public int GetHashCode(Host obj)
        {
            return base.GetHashCode();
        }
    }
}
