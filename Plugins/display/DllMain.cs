using System;
using System.Collections.Generic;
using CommandLine;
using Sdk.Models;
using Sdk.Plugins;

namespace display
{
    [Verb("/display", HelpText = "Control the physical state of the display hardware via DDC/CI")]
    public class DllMain : Plugin
    {
        [Option("enabled", Required = true, HelpText = "Turn the display on or off (true|false)")]
        public string Enabled { get; set; }

        public override void Execute()
        {
            bool turnOn = Convert.ToBoolean(this.Enabled);
            bool success = MonitorPowerController.SetPowerState(turnOn);

            this.ExecuteContextCallback(new TextContext()
            {
                ErrorMessage = success
                    ? $"Successfully sent DDC/CI hardware power command: {(turnOn ? "ON" : "OFF")}"
                    : "Failed to send DDC/CI commands. Ensure monitors support DDC/CI and are connected via HDMI/DP.",
                IsErrorSuccess = success,
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });
        }
    }
}