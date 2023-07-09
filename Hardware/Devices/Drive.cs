using Hardware.Base;
using Sdk.Contracts;
using Sdk.Devices;
using System.Text;
using static Hardware.Sdk.CpuIdSdk64;

namespace Hardware.Devices
{
    public class Drive : DeviceBase, IDrive
    {
        public Drive()
        {

        }

        public Drive(string DeviceName, int DeviceIndex, uint DeviceClass) : base(DeviceName, DeviceIndex, DeviceClass)
        {

        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var sensors = base.GetSensors(SENSOR_CLASS_UTILIZATION);

            foreach (ISensor sensor in sensors)
            {
                var hdd_name = sensor.Name.ToUpper().Replace("SPACE", "Storage used");
                var hdd_utilization = Math.Round(sensor.Value, 0);

                sb.AppendLine($"*{hdd_name}*: {hdd_utilization}%");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
