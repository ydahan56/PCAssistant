using Hardware.Base;
using Sdk.Devices;
using static Hardware.Sdk.CpuIdSdk64;

namespace Hardware.Devices
{
    public class Mainboard : DeviceBase, IMainboard
    {
        public Mainboard()
        {

        }

        public Mainboard(string DeviceName, int DeviceIndex, uint DeviceClass) : base(DeviceName, DeviceIndex, DeviceClass)
        {

        }

        public override string ToString()
        {
            var sensor = base.GetSensor(SENSOR_CLASS_UTILIZATION);

            return $"*RAM Used.*: {sensor.Value}%";
        }
    }
}
