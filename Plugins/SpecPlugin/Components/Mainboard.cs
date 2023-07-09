using Components.Base;
using Plugins.NSSpec.Sensors.Contracts;

namespace Plugins.NSSpec.Components
{
    public class Mainboard : IComponent
    {
        private readonly IEnumerable<IDevice> devices;
        private readonly IEnumerable<ISensor> sensors;

        public Mainboard(IEnumerable<IDevice> devices, IEnumerable<ISensor> sensors)
        {
            this.devices = devices;
            this.sensors = sensors;
        }

        public override string ToString()
        {
            foreach (IDevice device in devices)
            {
                text.AppendLine($"+ {device.DeviceName}");

                foreach (ISensor sensor in sensors)
                {
                    var result = device.GetSensors(sensor.Class);
                    AppendSensors(sensor.Name, result);
                }
            }

            return text.ToString();
        }
    }
}
