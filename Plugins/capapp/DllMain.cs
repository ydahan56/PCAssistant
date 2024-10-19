using Sdk.Plugins;
using CommandLine;
using System.Diagnostics;
using Sdk.Models;
using capapp.Helpers;
using System.Drawing.Imaging;

namespace Plugins.CapApp
{
    [Verb("/capapp", HelpText = "Capture the window of a program")]
    public class DllMain : Plugin
    {
        [Option("id", Required = true, HelpText = "The Porcess Id of the program to capture")]
        public int ProcessId { get; set; }

        private readonly CaptureHelper _capture;

        public DllMain()
        {
            this._capture = new CaptureHelper();
        }

        public override void Execute()
        {
            Process item;

            try
            {
                item = Process.GetProcessById(this.ProcessId);
            }
            catch (Exception e)
            {
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = e.Message,
                        Success = false,
                        ResultType = ExecuteResultType.Text
                    }
                );

                return;
            }

            var fileName = Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".jpg";
            var bitmap = this._capture.CaptureWindow(item.MainWindowHandle);

            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);

            ms.Position = 0;

            this.ExecuteResultCallback(
                new ExecuteImageResult()
                {
                    Success = true,
                    Stream = ms,
                    FileName = fileName
                }
            );

            ms.Dispose();
        }
    }
}
