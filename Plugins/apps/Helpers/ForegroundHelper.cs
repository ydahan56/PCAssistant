using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace apps.Helpers
{
    internal class ForegroundHelper
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        [Flags]
        public enum ProcessAccessFlags : uint
        {
            QueryLimitedInformation = 0x1000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern nint OpenProcess(
            ProcessAccessFlags dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(nint hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] int dwFlags, [Out] StringBuilder lpExeName, ref int lpdwSize);


        [DllImport("version.dll")]
        static extern int GetFileVersionInfoSize(string fileName, [Out] IntPtr lpdwHandle);

        [DllImport("version.dll", SetLastError = true)]
        static extern bool GetFileVersionInfo(
        /*__in*/    string lptstrFilename,
        /*__reserved*/  int dwHandleIgnored,
        /*__in      */  int dwLen,
        /*__out     */  byte[] lpData);

        [DllImport("version.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool VerQueryValue(
            byte[] pBlock,
            string lpSubBlock,
            out nint lplpBuffer,
            out uint puLen);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public enum GWL
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        public const uint WS_CAPTION = 0x00C00000;
        public const uint WS_VISIBLE = 0x10000000;

        private readonly StringBuilder _sb;

        public ForegroundHelper()
        {
            _sb = new StringBuilder();
        }

        public override string ToString()
        {
            // clear previous entries
            _sb.Clear();

            EnumWindows(EnumWindowProc, nint.Zero);

            return _sb.ToString();
        }

        private bool isTopLevelWindow(nint hWnd)
        {
            var windowStyle = GetWindowLong(hWnd, (int)GWL.GWL_STYLE);
            var windowStyle64 = windowStyle.ToInt64();

            return isBitSet(windowStyle64, WS_CAPTION) &&
                   isBitSet(windowStyle64, WS_VISIBLE);
        }

        private bool isBitSet(long flags, uint bit)
        {
            return (flags & bit) != 0;
        }

        private bool EnumWindowProc(nint hWnd, nint lParam)
        {
            if (!isTopLevelWindow(hWnd))
                return true;

            GetWindowThreadProcessId(hWnd, out uint processId);

            nint hProcess = OpenProcess(
                ProcessAccessFlags.QueryLimitedInformation,
                false,
                processId);

            if (hProcess == 0)
                return true;

            try
            {
                var pathBuffer = new StringBuilder(1024);
                int pathLength = pathBuffer.Capacity;

                if (!QueryFullProcessImageName(
                        hProcess,
                        0,
                        pathBuffer,
                        ref pathLength))
                {
                    return true;
                }

                string executablePath = pathBuffer.ToString();


                string itemName = GetFileDescription(executablePath)
                                  ?? Path.GetFileNameWithoutExtension(executablePath);

                _sb.AppendLine($"- {itemName} ({processId})");
            }
            finally
            {
                CloseHandle(hProcess);
            }

            return true;
        }

        private string? GetFileDescription(string filePath)
        {
            IntPtr handle = IntPtr.Zero;

            int size = GetFileVersionInfoSize(filePath, handle);

            if (size == 0)
                return null;

            byte[] buffer = new byte[size];

            if (!GetFileVersionInfo(filePath, 0, size, buffer))
                return null;

            // First try the standard FileDescription string.
            if (VerQueryValue(
                    buffer,
                    @"\StringFileInfo\040904B0\FileDescription",
                    out nint value,
                    out uint length) &&
                value != 0 &&
                length > 0)
            {
                return Marshal.PtrToStringUni(value);
            }

            // Fall back to ProductName.
            if (VerQueryValue(
                    buffer,
                    @"\StringFileInfo\040904B0\ProductName",
                    out value,
                    out length) &&
                value != 0 &&
                length > 0)
            {
                return Marshal.PtrToStringUni(value);
            }

            return null;
        }

    }
}
