using System.Runtime.InteropServices;

namespace pwrcfg.Helpers
{
    public static partial class User32Helper
    {
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
    }

    public static partial class User32Helper
    {
        public const int EWX_LOGOFF = 0;
    }
}
