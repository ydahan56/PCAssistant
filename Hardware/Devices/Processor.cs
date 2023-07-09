using Hardware.Base;
using Sdk.Devices;
using System.Text;
using static Hardware.Sdk.CpuIdSdk64;

namespace Hardware.Devices
{
    public class Processor : DeviceBase, IProcessor
    {
        public Processor()
        {

        }

        public Processor(string DeviceName, int DeviceIndex, uint DeviceClass) : base(DeviceName, DeviceIndex, DeviceClass)
        {

        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var utilization = base.GetSensor(SENSOR_CLASS_UTILIZATION);
            var temperature = base.GetSensor(SENSOR_CLASS_TEMPERATURE);

            sb.AppendLine($"*CPU Usage*: {Math.Round(utilization.Value, 0)}%");
            sb.Append($"*CPU Temp.*: {temperature.Value}°C");

            return sb.ToString();
        }
    }
}
