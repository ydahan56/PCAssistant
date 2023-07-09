using Hardware.Base;
using Sdk.Devices;
using static Hardware.Sdk.CpuIdSdk64;

namespace Hardware.Devices
{
    public class Display : DeviceBase, IDisplay
    {
        public Display()
        {

        }

        public Display(string DeviceName, int DeviceIndex, uint DeviceClass) : base(DeviceName, DeviceIndex, DeviceClass)
        {

        }

        public override string ToString()
        {
            var sensor = base.GetSensor(SENSOR_CLASS_TEMPERATURE);

            string name = DeviceName.Split(' ')[0];
            float value = sensor.Value;

            return $"*GPU {name}*: {value}°C";
        }
    }
}
