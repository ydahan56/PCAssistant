using System.Diagnostics;

namespace vol
{
    public class VolumeUtilities
    {
        private string _filePath;
        private readonly List<string> _argsList;

        private VolumeUtilities(string filePath)
        {
            this._filePath = filePath;
            this._argsList = new List<string>();
        }

        public static VolumeUtilities Create(string filePath)
        {
            return new VolumeUtilities(filePath);
        }

        public VolumeUtilities SetVolume(int value)
        {
            this._argsList.Add($"/SetVolume \"Speakers\" {value}");
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
