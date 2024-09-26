using CommandLine;
using Sdk.Base;
using Sdk.Models;
using System.Diagnostics;

namespace kill
{
    [Verb("/kill", HelpText = "Kill a Process by its ID")]
    public class DllMain : Plugin
    {

        [Option("pid", Required = true, HelpText = "The Process ID, in decimal")]
        public int PID { get; set; }

        public override void Execute()
        {
            Process target;

            var text = "";
            var success = false;

            try
            {
                target = Process.GetProcessById(this.PID);
            }
            catch (Exception e)
            {
                this.ExecuteResultCallback(
                    new ExecuteResult()
                    {
                        StatusText = e.Message,
                        Success = success
                    }
                );

                // exception occured, exit...
                return;
            }

            try
            {
                target.Kill();

                success = true;
                text = $"Process with name {target.ProcessName} terminated";
            }
            catch (Exception e)
            {
                success = false;
                text = e.Message;
            }

            this.ExecuteResultCallback(
                new ExecuteResult()
                {
                    StatusText = text,
                    Success = success
                }
            );
        }
    }
}
