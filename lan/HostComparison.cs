using lan.Models;

namespace lan
{
    /// <summary>
    /// Compares hosts based on their MAC address for set operations
    /// Used by Listener to identify device changes
    /// </summary>
    public class HostComparison : IEqualityComparer<Host>
    {
        public bool Equals(Host? x, Host? y)
        {
            if (x == null || y == null)
                return false;

            return string.Equals(x.Mac_address, y.Mac_address, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Host obj)
        {
            return obj?.Mac_address?.GetHashCode() ?? 0;
        }
    }
}
