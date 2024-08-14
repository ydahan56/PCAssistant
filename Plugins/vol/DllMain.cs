using CommandLine;
using Sdk;
using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Diagnostics;
using System.Reflection;
using vol;

namespace Plugins.Vol
{
    [Verb("/volume", HelpText = "Adjust the volume level")]
    public class DllMain : Plugin
    {
        private readonly FileInfo _utility;

        [Option("value", Required = true, HelpText = "The volume level (1-100)")]
        public int VolumeValue { get; set; }

        public DllMain()
        {
            this._utility = new FileInfo(
                PCManager.CombineAssembly(
                    Assembly.GetExecutingAssembly(), "SoundVolumeView.exe")
            );
        }

        public override void Execute()
        {
            if (!this._utility.Exists)
            {
                // return answer back to caller
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        Success = false,
                        ErrorMessage = $"{this._utility.Name} does not exists"
                    }
                );

                // exit
                return;
            }

            if (VolumeValue < 1 || VolumeValue > 100)
            {
                // return answer back to caller
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        Success = false,
                        ErrorMessage = $"Value cannot be {this.VolumeValue}, must be between 1-100"
                    }
                );

                // exit
                return;
            }

            var success = VolumeUtilities
                .Create(this._utility.FullName)
                .SetVolume(this.VolumeValue)
                .Execute();

            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    Success = success,
                    ErrorMessage = $"Volume has been set to value {this.VolumeValue}"
                }
            );
        }
    }
}
