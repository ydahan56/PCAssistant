namespace TempWarnPlugin.Models
{
    public class TempArgs : EventArgs
    {
        public string DeviceName { get; set; }
        public float Temperature { get; set; }
    }
}
