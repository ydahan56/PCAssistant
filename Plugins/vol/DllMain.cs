using CommandLine;
using Sdk;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Models;
using System.Diagnostics;
using System.Reflection;
using vol;

namespace Plugins.Vol
{
    public class DllMain : PluginBase
    {
        private class VolumeLevel
        {
            [Value(0)]
            public int level { get; set; }
        }

        private ITGBotClient _telegram;
        private readonly FileInfo _sndvol64;

        public DllMain()
        {
            base.Name = "/vol";
            base.ArgsPattern = "(\\d{1,3})";
            base.Description = "Adjust workstation's volume.";

            this._sndvol64 = new FileInfo(
                PCManager.CombineExternal(
                    Assembly.GetExecutingAssembly(), "sndvol64.exe")
            );
        }

        public override void Dispatch()
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(DispatchData data)
        {
            if (!this._sndvol64.Exists)
            {
                this._telegram.SendTextBackToAdmin($"{this._sndvol64.Name} does not exists");
                return;
            }

            Parser.Default.ParseArguments<VolumeLevel>(data.Args).WithParsed(this.SetVolume);
        }

        private void SetVolume(VolumeLevel arg)
        {
            if (arg.level < 1 || arg.level > 100)
            {
                this._telegram.SendTextBackToAdmin("Volume has to be between 1 to 100.");
                return;
            }

            var success = new SndVol64()
                .SetPath(this._sndvol64.FullName)
                .SetVolume(arg.level)
                .Execute();

            if (success)
            {
                this._telegram.SendTextBackToAdmin($"Volume successfully set to *{arg.level}%*");
            }
        }

        public override void Init(IDependencyService service)
        {
            this._telegram = service.ResolveInstance<ITGBotClient>();
        }
    }
}
