using lan.Types;
using System.Diagnostics;

namespace lan.Operations
{
    /// <summary>
    /// Scans the local network for connected devices using wnet.exe
    /// Executes a single scan and returns the results
    /// </summary>
    public class Scanner : OperationBase
    {
        private readonly Action<string> _updateAvailable;

        public Scanner(Action<string> updateAvailable)
        {
            _updateAvailable = updateAvailable ?? throw new ArgumentNullException(nameof(updateAvailable));
        }

        /// <summary>
        /// Executes a network scan operation
        /// </summary>
        public override void Execute()
        {
            try
            {
                _updateAvailable("Starting network scan...");

                // Validate wnet.exe exists
                if (!File.Exists(ProgramPath))
                {
                    _updateAvailable($"Error: {ProgramPath} not found.");
                    return;
                }

                // Execute network scan
                ExecuteScan();
            }
            catch (Exception ex)
            {
                _updateAvailable($"Error during scan: {ex.Message}");
            }
        }

        /// <summary>
        /// Executes the wnet.exe scan and processes results
        /// </summary>
        private void ExecuteScan()
        {
            // Generate output file path
            var outputFile = CombineDirectory($"network_scan_{DateTime.Now:yyyyMMdd_HHmmss}.xml");

            // Configure process
            var startInfo = new ProcessStartInfo
            {
                FileName = ProgramPath,
                Arguments = $"/sxml {outputFile}",
                UseShellExecute = false,
                RedirectStandardOutput = false,
                CreateNoWindow = true
            };

            try
            {
                // Start the scan process
                using var process = Process.Start(startInfo);

                if (process == null)
                {
                    _updateAvailable("Failed to start scan process.");
                    return;
                }

                // Wait for completion (max 2 minutes)
                var completed = process.WaitForExit((int)TimeSpan.FromMinutes(2).TotalMilliseconds);

                if (!completed)
                {
                    process.Kill();
                    _updateAvailable("Scan timeout (2 minutes exceeded).");
                    return;
                }

                // Check if output file was created
                if (!File.Exists(outputFile))
                {
                    _updateAvailable("Scan completed but output file was not created.");
                    return;
                }

                // Read and process results
                var hosts = ReadHosts(outputFile);

                if (hosts.Count == 0)
                {
                    _updateAvailable("Scan completed. No devices found.");
                    return;
                }

                // Format and report results
                var result = FormatHostList(hosts);
                _updateAvailable(result);

                // Cleanup: delete temporary XML file
                try
                {
                    File.Delete(outputFile);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
            catch (Exception ex)
            {
                _updateAvailable($"Scan process error: {ex.Message}");
            }
        }
    }
}
