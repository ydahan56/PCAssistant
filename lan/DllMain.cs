using CommandLine;
using lan.scanner;
using Sdk.Plugins;

namespace lan
{
    [Verb("/lan", HelpText = "Scan or listen for devices on the local network")]
    public class DllMain : Plugin
    {
        [Option("operation", HelpText = "The operation to execute, scan or listen")]
        public Operation Operation { get; set; }

        public override void Execute()
        {
            if (this.Operation == Operation.scan)
            {
                var scan = new Scanner();
                scan.Execute();

                return;
            }


        }
    }
}
