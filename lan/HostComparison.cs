using lan.Models;

namespace lan
{
    public class HostComparison : IEqualityComparer<Host>
    {
        public bool Equals(Host? x, Host? y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            // Compare by MAC address (primary identifier)
            if (!string.IsNullOrWhiteSpace(x.Mac_address) && !string.IsNullOrWhiteSpace(y.Mac_address))
            {
                return x.Mac_address.Equals(y.Mac_address, StringComparison.OrdinalIgnoreCase);
            }

            // Fallback to IP address if MAC is not available
            if (!string.IsNullOrWhiteSpace(x.Ip_address) && !string.IsNullOrWhiteSpace(y.Ip_address))
            {
                return x.Ip_address.Equals(y.Ip_address, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public int GetHashCode(Host obj)
        {
            if (obj == null)
                return 0;

            // Use MAC address for hash if available
            if (!string.IsNullOrWhiteSpace(obj.Mac_address))
            {
                return obj.Mac_address.ToLowerInvariant().GetHashCode();
            }

            // Use IP address for hash if MAC is not available
            if (!string.IsNullOrWhiteSpace(obj.Ip_address))
            {
                return obj.Ip_address.ToLowerInvariant().GetHashCode();
            }

            return 0;
        }
    }
}
