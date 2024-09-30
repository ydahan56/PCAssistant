


using CommandLine;
using Sdk.Base;
using Sdk.Models;
using System.Windows.Forms;

namespace Alert
{
    [Verb("alert", HelpText = "Show an alert dialog to the user.")]
    public class DllMain : Plugin
    {
        [Option("text", Required = true, HelpText = "The text message to show")]
        public string Text { get; set; }

        [Option("caption", Required = false, HelpText = "The dialog's caption")]
        public string Caption { get; set; }

        public override void Execute()
        {
            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = "Alert has been displayed.",
                    Success = true
                }
            );

            MessageBox.Show(
                this.Text,
                String.IsNullOrWhiteSpace(this.Caption) ? this.Caption : "PCAssistant",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );
        }
    }
}
