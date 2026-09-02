using CommandLine;
using Sdk;
using Sdk.Models;
using Sdk.Plugins;
using System.Reflection;
using vol;

namespace volume
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
                    Assembly.GetExecutingAssembly(),
                    "SoundVolumeView.exe"
                )
            );
        }

        public override void Execute()
        {
            if (!this._utility.Exists)
            {
                this.ExecuteContextCallback(new TextContext()
                {
                    ErrorMessage = $"{this._utility.Name} does not exists",
                    IsErrorSuccess = false,
                    ChatId = this.Parameters.ChatId,
                    ReplyParameters = this.Parameters.ReplyParameters
                });
                return;
            }

            if (VolumeValue < 1 || VolumeValue > 100)
            {
                // return answer back to caller
                this.ExecuteContextCallback(new TextContext()
                {
                    IsErrorSuccess = false,
                    ErrorMessage = $"Value cannot be {this.VolumeValue}, must be between 1-100",
                    ChatId = this.Parameters.ChatId,
                    ReplyParameters = this.Parameters.ReplyParameters
                });

                // exit
                return;
            }

            var success = VolumeUtilities
                .Create(this._utility.FullName)
                .SetVolume(this.VolumeValue)
                .Execute();

            this.ExecuteContextCallback(new TextContext()
            {
                IsErrorSuccess = success,
                ErrorMessage = $"Volume has been set to value {this.VolumeValue}",
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });
        }
    }
}
