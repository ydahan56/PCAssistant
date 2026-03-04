using lan.Models;
using lan.Types;
using System.ComponentModel;
using System.Diagnostics;

namespace lan.Operations
{
    public class Listener : OperationBase
    {
        private Process scanner;
        private bool _cancel;

        private Thread worker;
        private Thread _cancelthr;

        private readonly Action<string> _updateAvailable;


        private readonly object _cancel_lock = new object();

        public Listener(Action<string> updateAvailable)
        {
            this._updateAvailable = updateAvailable;
            this.worker = new Thread(this.WorkerProc);
            this._cancelthr = new Thread(this.CancelProc);
        }

        private void CancelProc(object? obj)
        {
            while (true) 
            {
                if (this._cancel)
                {
                    lock (this._cancel_lock)
                    {
                        if (this.scanner.HasExited)
                            break;

                        this.scanner.Kill();
                        break; // exit the thread
                    }
                }

                Thread.Sleep(50); // prevent cooking the cpu
            }
        }

        private void WorkerProc(object? obj)
        {
            var startinfo = new ProcessStartInfo(programuri, $"/sxml {AppDomain.CurrentDomain.BaseDirectory}");

            this._updateAvailable("Network monitor is now listening...");

            List<Host> prevScan = new List<Host>();
            List<Host> lastScan = new List<Host>();

            while (!this._cancel)
            {
                lock (this._cancel_lock)
                {
                    scanner = Process.Start(startinfo);
                    scanner.WaitForExit();
                }
               
                // initiate a first scan so we have a list (prevScan) to compare 
                // the newely scanned list (lastScan)
                if (prevScan.Count == 0)
                {
                    prevScan.AddRange(ReadHosts(this.programuri));
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

                Thread.Sleep(3000);
            }

            RaiseFeedback("Network monitoring disconnected.");
        }

        private List<Host> GetConnectedClients(List<Host> prevScan, List<Host> lastScan)
        {
            return lastScan.Except(prevScan, new HostComparison()).ToList();
        }

        private List<Host> GetDisconnectedClients(List<Host> prevScan, List<Host> lastScan)
        {
            return prevScan.Except(lastScan, new HostComparison()).ToList();
        }

        public override void Disable()
        {
            if (!Active)
            {
                RaiseFeedback("The network is not being monitored.");
                return;
            }

            if (!scanner.HasExited)
                scanner.Kill();

            if (worker.IsBusy)
                worker.CancelAsync();

            Active = false;
        }

        public override void Execute()
        {
            if (this._cancel)
            {
                this._updateAvailable("The network is already being monitored.");
                return;
            }

            this.worker.Start();
            this._cancelthr.Start();
        }
    }
}
