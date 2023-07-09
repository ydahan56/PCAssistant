using System.Runtime.InteropServices;

namespace DisplayPlugin.Native
{
    public static partial class user32
    {
        [DllImport("user32.dll")]
        public static extern int PostMessage(int hWnd, int hMsg, int wParam, int lParam);
    }

    public static partial class user32
    {
        public const int HWND_BROADCAST = 0xFFFF;
        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MONITORPOWER = 0xF170;
    }
}
