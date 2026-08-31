using lan.Types;
using System.Diagnostics;

namespace lan.Operations
{
    public class Scanner : OperationBase
    {
        private readonly ProcessStartInfo si;
        private readonly Action<string> _updateAvailable;

        public Scanner(Action<string> updateAvailable)
        {
            this._updateAvailable = updateAvailable;
        }

        public override void Discover()
        {
            var programuri = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wnet.exe");
            var startinfo = new ProcessStartInfo(programuri, $"/sxml {AppDomain.CurrentDomain.BaseDirectory}");

            if (!File.Exists(programuri))
            {
                this._updateAvailable($"{programuri} does not exist.");
                return;
            };

            var wnet = Process.Start(si);

            // we wait maximum 2 minutes
            wnet.WaitForExit(TimeSpan.FromMinutes(2));

            RaiseDiscovered(ReadHosts(scanPath));
        }
    }
}
