using CommandLine;
using lan.Operations;
using lan.Types;
using Sdk.Models;
using Sdk.Plugins;

namespace lan
{
    [Verb("/lan", HelpText = "Scan or listen for devices on the local network")]
    public class DllMain : Plugin
    {
        [Option("operation", Required = true, HelpText = "The operation to execute: scan, listen, or disable")]
        public OperationType Operation { get; set; }

        private static Listener? _listener;

        public override void Execute()
        {
            switch (Operation)
            {
                case OperationType.scan:
                    ExecuteScan();
                    break;

                case OperationType.listen:
                    ExecuteListen();
                    break;

                case OperationType.disable:
                    ExecuteDisable();
                    break;

                default:
                    UpdateAvailable($"❌ Unknown operation: {Operation}");
                    break;
            }
        }

        private void ExecuteScan()
        {
            var scanner = new Scanner(UpdateAvailable);
            scanner.Execute();
        }

        private void ExecuteListen()
        {
            if (_listener == null)
            {
                _listener = new Listener(UpdateAvailable);
            }

            _listener.Execute();
        }

        private void ExecuteDisable()
        {
            if (_listener == null)
            {
                UpdateAvailable("ℹ️ Network monitoring is not active.");
                return;
            }

            _listener.Disable();
        }

        private void UpdateAvailable(string update)
        {
            var result = new ExecuteResult
            {
                StatusText = update,
                ResultType = ExecuteResultType.Text,
                Success = true
            };

            ExecuteResultCallback(result);
        }
    }
}
