using lan.Models;
using lan.Types;
using System.Diagnostics;

namespace lan.Operations
{
    /// <summary>
    /// Monitors the local network for device connections and disconnections
    /// Runs continuously in a background thread until stopped
    /// </summary>
    public class Listener : OperationBase
    {
        private readonly Action<string> _updateAvailable;
        private Thread? _workerThread;
        private volatile bool _isRunning;
        private volatile bool _shouldStop;
        private Process? _scanProcess;
        private readonly object _syncLock = new object();

        public Listener(Action<string> updateAvailable)
        {
            _updateAvailable = updateAvailable ?? throw new ArgumentNullException(nameof(updateAvailable));
        }

        /// <summary>
        /// Starts the network listener
        /// </summary>
        public override void Execute()
        {
            lock (_syncLock)
            {
                if (_isRunning)
                {
                    _updateAvailable("Network listener is already running.");
                    return;
                }

                _shouldStop = false;
                _isRunning = true;
            }

            // Start listener in background thread
            _workerThread = new Thread(ListenerWorker)
            {
                Name = "LAN-Listener-Worker",
                IsBackground = true
            };

            _workerThread.Start();
            _updateAvailable("Network listener started. Monitoring for device changes...");
        }

        /// <summary>
        /// Stops the network listener
        /// </summary>
        public void Stop()
        {
            lock (_syncLock)
            {
                if (!_isRunning)
                {
                    _updateAvailable("Network listener is not running.");
                    return;
                }

                _shouldStop = true;
            }

            // Kill the scan process if running
            try
            {
                if (_scanProcess != null && !_scanProcess.HasExited)
                {
                    _scanProcess.Kill();
                }
            }
            catch
            {
                // Ignore process kill errors
            }

            // Wait for worker thread to finish
            try
            {
                if (_workerThread != null && _workerThread.IsAlive)
                {
                    _workerThread.Join(TimeSpan.FromSeconds(5));
                }
            }
            catch
            {
                // Ignore thread join errors
            }

            lock (_syncLock)
            {
                _isRunning = false;
            }

            _updateAvailable("Network listener stopped.");
        }

        /// <summary>
        /// Worker thread that continuously monitors the network
        /// </summary>
        private void ListenerWorker()
        {
            try
            {
                var previousHosts = new List<Host>();
                var currentHosts = new List<Host>();

                while (!_shouldStop)
                {
                    try
                    {
                        // Perform scan
                        currentHosts = PerformScan();

                        if (currentHosts.Count == 0)
                        {
                            // No devices found, wait and retry
                            Thread.Sleep(3000);
                            continue;
                        }

                        // First scan - establish baseline
                        if (previousHosts.Count == 0)
                        {
                            previousHosts.AddRange(currentHosts);
                            _updateAvailable($"Baseline scan: {currentHosts.Count} device(s) found.");
                        }
                        else
                        {
                            // Compare scans to find changes
                            var connectedHosts = GetConnectedHosts(previousHosts, currentHosts);
                            var disconnectedHosts = GetDisconnectedHosts(previousHosts, currentHosts);

                            if (connectedHosts.Count > 0)
                            {
                                var message = $"✅ Connected: {connectedHosts.Count} device(s)\n{FormatHostList(connectedHosts)}";
                                _updateAvailable(message);
                            }

                            if (disconnectedHosts.Count > 0)
                            {
                                var message = $"❌ Disconnected: {disconnectedHosts.Count} device(s)\n{FormatHostList(disconnectedHosts)}";
                                _updateAvailable(message);
                            }

                            // Update previous list
                            previousHosts.Clear();
                            previousHosts.AddRange(currentHosts);
                        }

                        currentHosts.Clear();

                        // Wait before next scan
                        Thread.Sleep(3000);
                    }
                    catch (ThreadAbortException)
                    {
                        // Thread was aborted, exit gracefully
                        break;
                    }
                    catch (Exception ex)
                    {
                        _updateAvailable($"Error during scan: {ex.Message}");
                        Thread.Sleep(3000); // Wait before retrying
                    }
                }
            }
            finally
            {
                _updateAvailable("Network listener worker thread terminated.");
            }
        }

        /// <summary>
        /// Performs a single network scan and returns the results
        /// </summary>
        private List<Host> PerformScan()
        {
            if (!File.Exists(ProgramPath))
            {
                _updateAvailable($"wnet.exe not found at {ProgramPath}");
                return new List<Host>();
            }

            var outputFile = CombineDirectory($"scan_{Guid.NewGuid():N}.xml");

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = ProgramPath,
                    Arguments = $"/sxml {outputFile}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                lock (_syncLock)
                {
                    _scanProcess = Process.Start(startInfo);
                }

                if (_scanProcess == null)
                {
                    return new List<Host>();
                }

                // Wait for scan to complete (max 60 seconds)
                var completed = _scanProcess.WaitForExit((int)TimeSpan.FromSeconds(60).TotalMilliseconds);

                if (!completed)
                {
                    try
                    {
                        _scanProcess.Kill();
                    }
                    catch { }

                    return new List<Host>();
                }

                // Read results
                if (File.Exists(outputFile))
                {
                    var hosts = ReadHosts(outputFile);

                    // Cleanup temp file
                    try
                    {
                        File.Delete(outputFile);
                    }
                    catch { }

                    return hosts;
                }

                return new List<Host>();
            }
            catch (Exception ex)
            {
                _updateAvailable($"Scan error: {ex.Message}");
                return new List<Host>();
            }
            finally
            {
                lock (_syncLock)
                {
                    _scanProcess?.Dispose();
                    _scanProcess = null;
                }
            }
        }

        /// <summary>
        /// Identifies newly connected hosts
        /// </summary>
        private List<Host> GetConnectedHosts(List<Host> previousScan, List<Host> currentScan)
        {
            var comparer = new HostMacAddressComparer();
            return currentScan.Except(previousScan, comparer).ToList();
        }

        /// <summary>
        /// Identifies disconnected hosts
        /// </summary>
        private List<Host> GetDisconnectedHosts(List<Host> previousScan, List<Host> currentScan)
        {
            var comparer = new HostMacAddressComparer();
            return previousScan.Except(currentScan, comparer).ToList();
        }
    }

    /// <summary>
    /// Compares hosts by MAC address for identifying device changes
    /// </summary>
    internal class HostMacAddressComparer : IEqualityComparer<Host>
    {
        public bool Equals(Host? x, Host? y)
        {
            if (x == null || y == null)
                return false;

            return string.Equals(x.Mac_address, y.Mac_address, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Host obj)
        {
            return obj?.Mac_address?.GetHashCode() ?? 0;
        }
    }
}
