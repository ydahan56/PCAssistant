using System.Diagnostics;
using System.Text;

namespace apps.Helpers
{
    internal class ForegroundHelper
    {
        private readonly StringBuilder _sb;

        public ForegroundHelper()
        {
            _sb = new StringBuilder();
        }

        public override string ToString()
        {
            // clear previous entries
            _sb.Clear();

            User32Helper.EnumWindows(EnumWindowProc, nint.Zero);

            return _sb.ToString();
        }

        private bool isTopLevelWindow(nint hWnd)
        {
            var windowStyle = User32Helper.GetWindowLong(hWnd, (int)User32Helper.GWL.GWL_STYLE);
            var windowStyle64 = windowStyle.ToInt64();

            return isBitSet(windowStyle64, User32Helper.WS_CAPTION) && 
                   isBitSet(windowStyle64, User32Helper.WS_VISIBLE);
        }

        private bool isBitSet(long flags, uint bit)
        {
            return (flags & bit) != 0;
        }

        private bool EnumWindowProc(nint hWnd, nint lParam)
        {
            if (this.isTopLevelWindow(hWnd))
            {
                User32Helper.GetWindowThreadProcessId(hWnd, out uint id);
                var item = Process.GetProcessById((int)id);

                var versionInfo = item.MainModule.FileVersionInfo;

                string itemName = string.
                    IsNullOrWhiteSpace(versionInfo.FileDescription) ?
                    versionInfo.ProductName : 
                    versionInfo.FileDescription;

                this._sb.AppendLine($"- {itemName} ({id})");
            }

            return true;
        }
    }
}
