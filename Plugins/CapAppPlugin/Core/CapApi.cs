using Sdk.Contracts;
using System.Drawing;
using static CapAppPlugin.Native.user32;

namespace CapAppPlugin.Core
{
    public class CapApi : IApi<Bitmap>
    {
        private readonly IntPtr hWnd;

        public CapApi(IntPtr hWnd)
        {
            this.hWnd = hWnd;

            Func = GetWindowBitmap;
        }

        public Bitmap GetWindowBitmap()
        {
            bool isWindowMinized(IntPtr wnd)
            {
                IntPtr wndStyles = GetWindowLong(wnd, (int)GWL.GWL_STYLE);

                return (wndStyles.ToInt64() & WS_MINIMIZE) != 0;
            }

            bool minimize = false;

            if (isWindowMinized(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
                minimize = true;
            }
            else
            {
                SetForegroundWindow(hWnd);
            }

            Thread.Sleep(250);

            Rect rect = new Rect();

            GetWindowRect(hWnd, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            Bitmap result = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(rect.left, rect.top, 0, 0, result.Size);
            }

            if (minimize)
                ShowWindow(hWnd, SW_MINIMIZE);

            return result;
        }
    }
}
