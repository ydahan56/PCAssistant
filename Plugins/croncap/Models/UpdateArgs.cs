using System.Drawing;

namespace croncap.Models
{
    public class UpdateArgs : EventArgs
    {
        public Bitmap Capture { get; set; }
    }
}
