using Sdk.Contracts;
using System.Management;

namespace DimPlugin.Core
{
    public class DimApi : IApi
    {
        private readonly byte brightness;

        public DimApi(byte brightness)
        {
            this.brightness = brightness;

            Action = SetBrightness;
        }

        public void SetBrightness()
        {
            using var mclass = new ManagementClass("WmiMonitorBrightnessMethods")
            {
                Scope = new ManagementScope(@"\\.\root\wmi")
            };

            using var instances = mclass.GetInstances();
            var args = new object[] { 1, brightness };

            foreach (ManagementObject instance in instances)
            {
                instance.InvokeMethod("WmiSetBrightness", args);
            }
        }
    }
}
