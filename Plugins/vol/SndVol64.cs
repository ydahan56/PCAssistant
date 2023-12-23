using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vol
{
    public class SndVol64
    {
        private string _filePath;
        private readonly List<string> _argsList;

        public SndVol64()
        {
            this._argsList = new List<string>();
        }

        public SndVol64 SetPath(string filePath)
        {
            this._filePath = filePath;
            return this;
        }

        public SndVol64 SetVolume(int volumeLevel)
        {
            this._argsList.Add($"/SetVolume \"Speakers\" {volumeLevel}");
            return this;
        }

        public bool Execute()
        {
            var args = string.Join(" ", this._argsList);
            var processStartInfo = new ProcessStartInfo(this._filePath, args)
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            var instance = Process.Start(processStartInfo);
            return instance.Id > 0;
        }
    }
}
