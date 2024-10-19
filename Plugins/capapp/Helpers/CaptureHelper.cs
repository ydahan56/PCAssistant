using System.Drawing;

namespace capapp.Helpers
{
    internal class CaptureHelper
    {
        public Bitmap CaptureWindow(IntPtr hWnd)
        {
            var isMinimised = this.GetIsWindowMinimized(hWnd);
            var bitmap = this.GetWindowBitmap(hWnd);

            // minimise back
            if (isMinimised)
            {
                User32Helper.ShowWindow(hWnd, User32Helper.SW_MINIMIZE);
            }

            return bitmap;
        }

        private Bitmap GetWindowBitmap(IntPtr hWnd)
        {
            var rect = new User32Helper.Rect();
            User32Helper.GetWindowRect(hWnd, ref rect);

            var width = rect.right - rect.left;
            var height = rect.bottom - rect.top;

            var bitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rect.left, rect.top, 0, 0, bitmap.Size);
            }

            return bitmap;
        }

        private bool GetIsWindowMinimized(IntPtr hWnd)
        {
            var flag = this.isWindowMinized(hWnd);

            if (flag) {
                User32Helper.ShowWindow(hWnd, User32Helper.SW_RESTORE);

                // let the window appear..
                Thread.Sleep(250);
            }
            else {
                User32Helper.SetForegroundWindow(hWnd);
            }

            return flag;
        }

        private bool isWindowMinized(IntPtr wnd)
        {
            var windowStyle = User32Helper.GetWindowLong(wnd, (int)User32Helper.GWL.GWL_STYLE);

            return (windowStyle.ToInt64() & User32Helper.WS_MINIMIZE) != 0;
        }
    }
}
