using Plugins.NSSpec.Sensors.Contracts;
using static Components.Sdk.CpuIdSdk64;

namespace Plugins.NSSpec.Sensors
{
    public class Capacity : ISensor
    {
        public Capacity()
        {
            Name = "Capacities:";
            Class = SENSOR_CLASS_CAPACITY;
        }
    }
}
