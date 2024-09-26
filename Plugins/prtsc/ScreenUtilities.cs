namespace prtsc
{
    public class ScreenUtilities
    {
        public IEnumerable<Bitmap> GetScreensBitmap()
        {
            var screens = new List<Bitmap>();

            foreach (Screen screen in Screen.AllScreens)
            {
                int left = screen.Bounds.Left;
                int top = screen.Bounds.Top;

                int width = screen.Bounds.Width;
                int height = screen.Bounds.Height;

                var map = new Bitmap(width, height);

                using (var graphic = Graphics.FromImage(map))
                {
                    graphic.CopyFromScreen(left, top, 0, 0, map.Size);
                }

                screens.Add(map);
            }

            return screens;
        }
    }
}
