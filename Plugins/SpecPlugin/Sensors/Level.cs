using Plugins.NSSpec.Sensors.Contracts;
using static Components.Sdk.CpuIdSdk64;

namespace Plugins.NSSpec.Sensors
{
    public class Level : ISensor
    {
        public Level()
        {
            Name = "Levels:";
            Class = SENSOR_CLASS_LEVEL;
        }
    }
}
