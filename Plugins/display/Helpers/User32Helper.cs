using System.Runtime.InteropServices;

namespace display.Helpers
{
    public static class User32Helper
    {
        public const int HWND_BROADCAST = 0xFFFF;
        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MONITORPOWER = 0xF170;

        [DllImport("user32.dll")]
        public static extern int PostMessage(int hWnd, int hMsg, int wParam, int lParam);
    }
}
