using lan.Models;
using lan.Types;
using System.Diagnostics;

namespace lan.Operations
{
    public class Listener : OperationBase
    {
        private Process? scanner;
        private bool _cancel;
        private bool _active;

        private Thread? worker;
        private Thread? _cancelthr;

        private readonly Action<string> _updateAvailable;
        private readonly object _cancel_lock = new object();

        public Listener(Action<string> updateAvailable) : base()
        {
            _updateAvailable = updateAvailable ?? throw new ArgumentNullException(nameof(updateAvailable));
        }

        protected override void RaiseFeedback(string message)
        {
            _updateAvailable?.Invoke(message);
        }

        private void CancelProc()
        {
            while (true)
            {
                if (_cancel)
                {
                    lock (_cancel_lock)
                    {
                        if (scanner != null)
                        {
                            if (!scanner.HasExited)
                            {
                                try
                                {
                                    scanner.Kill();
                                }
                                catch { }
                            }
                        }
                        break; // exit the thread
                    }
                }

                Thread.Sleep(50); // prevent cooking the cpu
            }
        }

        private void WorkerProc()
        {
            if (!File.Exists(programuri))
            {
                RaiseFeedback($"❌ Network scanner not found: {programuri}");
                return;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = programuri,
                Arguments = $"/sxml \"{scanPath}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            RaiseFeedback("👂 Network monitor is now listening...");

            List<Host> prevScan = new List<Host>();
            List<Host> lastScan = new List<Host>();

            while (!_cancel)
            {
                try
                {
                    lock (_cancel_lock)
                    {
                        if (_cancel) break;

                        scanner = Process.Start(startInfo);
                        scanner?.WaitForExit(60000); // 1 minute timeout
                    }

                    // initiate a first scan so we have a list (prevScan) to compare 
                    // the newly scanned list (lastScan)
                    if (prevScan.Count == 0)
                    {
                        prevScan.AddRange(ReadHosts(scanPath));
                    }
                    else
                    {
                        lastScan.AddRange(ReadHosts(scanPath));

                        var connectedHosts = GetConnectedClients(prevScan, lastScan);
                        var disconnectedHosts = GetDisconnectedClients(prevScan, lastScan);

                        if (connectedHosts.Count > 0)
                            RaiseConnected(connectedHosts);

                        if (disconnectedHosts.Count > 0)
                            RaiseDisconnected(disconnectedHosts);

                        prevScan.Clear();
                        prevScan.AddRange(lastScan);

                        lastScan.Clear();
                    }
                }
                catch (Exception ex)
                {
                    RaiseFeedback($"⚠️ Monitoring error: {ex.Message}");
                }

                Thread.Sleep(3000); // Wait 3 seconds between scans
            }

            _active = false;
            RaiseFeedback("🛑 Network monitoring stopped.");
        }

        private List<Host> GetConnectedClients(List<Host> prevScan, List<Host> lastScan)
        {
            return lastScan.Except(prevScan, new HostComparison()).ToList();
        }

        private List<Host> GetDisconnectedClients(List<Host> prevScan, List<Host> lastScan)
        {
            return prevScan.Except(lastScan, new HostComparison()).ToList();
        }

        public void Disable()
        {
            if (!_active)
            {
                RaiseFeedback("ℹ️ The network is not being monitored.");
                return;
            }

            _cancel = true;

            if (scanner != null && !scanner.HasExited)
            {
                try
                {
                    scanner.Kill();
                }
                catch { }
            }

            // Wait for threads to finish
            worker?.Join(5000);
            _cancelthr?.Join(5000);

            _active = false;
            RaiseFeedback("🛑 Network monitoring disabled.");
        }

        public override void Execute()
        {
            if (_active)
            {
                RaiseFeedback("ℹ️ The network is already being monitored.");
                return;
            }

            _active = true;
            _cancel = false;

            worker = new Thread(WorkerProc) { IsBackground = true };
            _cancelthr = new Thread(CancelProc) { IsBackground = true };

            worker.Start();
            _cancelthr.Start();
        }
    }
}
