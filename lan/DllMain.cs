using CommandLine;
using lan.scanner;
using lan.Types;
using Sdk.Models;
using Sdk.Plugins;

namespace lan
{
    [Verb("/lan", HelpText = "Scan or listen for devices on the local network")]
    public class DllMain : Plugin
    {
        [Option("operation", HelpText = "The operation to execute, scan or listen")]
        public OperationType Operation { get; set; }

        public override void Execute()
        {
            if (this.Operation == OperationType.scan)
            {
                var scan = new Scanner(this.UpdateAvailable);
                scan.Execute();

                return;
            }

            if (this.Operation == OperationType.listen)
            {

            }
        }

        private void UpdateAvailable(string update)
        {
            var result = new ExecuteResult()
            {
                StatusText = update,
                ResultType = ExecuteResultType.Text,
                Success = true
            };

            this.ExecuteResultCallback(result);
        }
    }
}
