using CommandLine;
using lan.Operations;
using lan.Types;
using Sdk.Models;
using Sdk.Plugins;

namespace lan
{
    /// <summary>
    /// LAN scanning and monitoring plugin
    /// Supports operations: scan (single network scan) and listen (continuous monitoring)
    /// </summary>
    [Verb("/lan", HelpText = "Scan or listen for devices on the local network")]
    public class DllMain : Plugin
    {
        [Option("operation", Required = true, HelpText = "The operation to execute: 'scan' (single network scan) or 'listen' (continuous monitoring)")]
        public OperationType Operation { get; set; }

        private static Listener? _activeListener;

        public override void Execute()
        {
            try
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
                        SendResult("Invalid operation. Use 'scan' or 'listen'.", false);
                        break;
                }
            }
            catch (Exception ex)
            {
                SendResult($"Plugin error: {ex.Message}", false);
            }
        }

        /// <summary>
        /// Executes a single network scan
        /// </summary>
        private void ExecuteScan()
        {
            var scanner = new Scanner(UpdateCallback);
            scanner.Execute();
        }

        /// <summary>
        /// Starts continuous network monitoring
        /// </summary>
        private void ExecuteListen()
        {
            // Stop any existing listener
            if (_activeListener != null)
            {
                _activeListener.Stop();
            }

            // Start new listener
            _activeListener = new Listener(UpdateCallback);
            _activeListener.Execute();
        }

        /// <summary>
        /// Stops the active network listener
        /// </summary>
        private void ExecuteDisable()
        {
            if (_activeListener == null)
            {
                SendResult("No active network listener to disable.", true);
                return;
            }

            _activeListener.Stop();
            _activeListener = null;
        }

        /// <summary>
        /// Callback for operation updates/results
        /// </summary>
        private void UpdateCallback(string message)
        {
            SendResult(message, true);
        }

        /// <summary>
        /// Sends a result back through the plugin callback system
        /// </summary>
        private void SendResult(string message, bool success)
        {
            var result = new ExecuteResult()
            {
                StatusText = message,
                ResultType = ExecuteResultType.Text,
                Success = success
            };

            ExecuteResultCallback(result);
        }
    }
}
