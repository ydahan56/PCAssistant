using System.Drawing;

namespace Telebot.Capture
{
    public class CaptureArgs : EventArgs
    {
        public Bitmap Capture { get; set; }
    }
}
