namespace LanPlugin.Intranet
{
    public class LanMonitor : IINetMonitor
    {
        private Process scanner;
        private readonly BackgroundWorker worker;

        public LanMonitor()
        {
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += DoWork;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var si = new ProcessStartInfo(
                wnetPath, $"/sxml {scanPath}"
            );

            RaiseFeedback("Network monitor is now listening...");

            List<Host> prevScan = new List<Host>();
            List<Host> lastScan = new List<Host>();

            while (!worker.CancellationPending)
            {
                scanner = Process.Start(si);
                scanner.WaitForExit();

                if (worker.CancellationPending)
                    break;

                // initiate a first scan so we have a list (prevScan) to compare 
                // the newely scanned list (lastScan)
                if (prevScan.Count == 0)
                {
                    prevScan.AddRange(ReadHosts(scanPath));
                }
                else
                {
                    lastScan.AddRange(ReadHosts(scanPath));

                    var connectedHosts = GetConnected(prevScan, lastScan);
                    var disconnectedHosts = GetDisconnected(prevScan, lastScan);

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

        private List<Host> GetConnected(List<Host> prevScan, List<Host> lastScan)
        {
            return lastScan.Except(prevScan, new HostComparison()).ToList();
        }

        private List<Host> GetDisconnected(List<Host> prevScan, List<Host> lastScan)
        {
            return prevScan.Except(lastScan, new HostComparison()).ToList();
        }

        public override void Disconnect()
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

        public override void Listen()
        {
            if (Active)
            {
                RaiseFeedback("The network is already being monitored.");
                return;
            }

            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
                Active = true;
            }
        }
    }
}
