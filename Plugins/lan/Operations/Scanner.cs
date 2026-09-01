using lan.Types;
using System.Diagnostics;

namespace lan.Operations
{
    public class Scanner : OperationBase
    {
        private readonly Action<string> _updateAvailable;

        public Scanner(Action<string> updateAvailable) : base()
        {
            _updateAvailable = updateAvailable ?? throw new ArgumentNullException(nameof(updateAvailable));
        }

        protected override void RaiseFeedback(string message)
        {
            _updateAvailable?.Invoke(message);
        }

        public override void Execute()
        {
            if (!File.Exists(programuri))
            {
                RaiseFeedback($"❌ Network scanner not found: {programuri}");
                return;
            }

            try
            {
                RaiseFeedback("🔍 Scanning local network...");

                // Configure process to run wnet.exe
                var startInfo = new ProcessStartInfo
                {
                    FileName = programuri,
                    Arguments = $"/sxml \"{scanpath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(startInfo);

                if (process == null)
                {
                    RaiseFeedback("❌ Failed to start network scanner process.");
                    return;
                }

                // Wait maximum 2 minutes for the scan to complete
                var timeout = TimeSpan.FromMinutes(2);
                if (!process.WaitForExit((int)timeout.TotalMilliseconds))
                {
                    RaiseFeedback("⏱️ Scan timed out after 2 minutes.");
                    process.Kill();
                    return;
                }

                // Check if scan file was created
                if (!File.Exists(scanpath))
                {
                    RaiseFeedback("❌ Scan completed but no results file was created.");
                    return;
                }

                // Read and display discovered hosts
                var hosts = ReadHosts(scanpath);
                RaiseDiscovered(hosts);
            }
            catch (Exception ex)
            {
                RaiseFeedback($"❌ Scan failed: {ex.Message}");
            }
        }
    }
}
