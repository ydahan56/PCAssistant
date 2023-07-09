using Plugins.NSSpec.Sensors.Contracts;
using static Components.Sdk.CpuIdSdk64;

namespace Plugins.NSSpec.Sensors
{
    public class Utilization : ISensor
    {
        public Utilization()
        {
            Name = "Utilization:";
            Class = SENSOR_CLASS_UTILIZATION;
        }
    }
}
