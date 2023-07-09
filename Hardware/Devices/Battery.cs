using Hardware.Base;
using Sdk.Devices;

namespace Hardware.Devices
{
    public class Battery : DeviceBase, IBattery
    {
        public Battery()
        {

        }

        public Battery(string DeviceName, int DeviceIndex, uint DeviceClass) : base(DeviceName, DeviceIndex, DeviceClass)
        {

        }

        public override string ToString()
        {
            return "";
        }
    }
}
