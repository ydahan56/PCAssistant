using System.Drawing;
using System.Drawing.Imaging;

namespace Sdk.Extensions
{
    public static class BitmapExtensions
    {
        public static MemoryStream ToMemStream(this Bitmap bitmap)
        {
            var memStream = new MemoryStream();
            bitmap.Save(memStream, ImageFormat.Jpeg);
            memStream.Position = 0;
            return memStream;
        }
    }
}
