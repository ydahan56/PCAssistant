using Sdk.Contracts;
using System.Drawing;
using System.Windows.Forms;

namespace CapTimePlugin.Core
{
    public class DesktopApi : IApi<IEnumerable<Bitmap>>
    {
        public DesktopApi()
        {
            Func = GetDesktopBitmap;
        }

        public List<Bitmap> GetDesktopBitmap()
        {
            List<Bitmap> items = new();

            foreach (Screen screen in Screen.AllScreens)
            {
                int left = screen.Bounds.Left;
                int top = screen.Bounds.Top;

                int width = screen.Bounds.Width;
                int height = screen.Bounds.Height;

                Bitmap result = new Bitmap(width, height);

                using (var graphic = Graphics.FromImage(result))
                {
                    graphic.CopyFromScreen(left, top, 0, 0, result.Size);
                }

                items.Add(result);
            }

            return items;
        }
    }
}
