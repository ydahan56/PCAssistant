using Sdk.Contracts;
using System.Diagnostics;

namespace VolPlugin.Core
{
    public class VolApi : IApi
    {
        private readonly int level;

        public VolApi(int level)
        {
            this.level = level;

            Action = SetVolume;
        }

        public void SetVolume()
        {
            var si = new ProcessStartInfo(
                "..\\Plugins\\Vol\\Debug\\net6.0-windows\\sndvol64.exe",
                $"/SetVolume \"Speakers\" {level}"
            )
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            Process.Start(si);
        }
    }
}
