using System.Runtime.InteropServices;

namespace msinfo32.Helpers
{
    public static class Kernel32Helper
    {
        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount64();
    }
}
