using Components.Models;
using System.Text;

namespace Plugins.NSSpec.Components
{
    public abstract class IComponent
    {
        protected readonly StringBuilder text;

        protected IComponent()
        {
            text = new StringBuilder();
        }

        protected void AppendSensors(string sensorName, IEnumerable<Sensor> sensors)
        {
            text.AppendLine(sensorName);

            foreach (Sensor sensor in sensors)
            {
                string line = $"{sensor.Name} Value:{sensor.Value} Min:{sensor.Min} Max:{sensor.Max}";
                text.AppendLine(line);
            }

            text.AppendLine();
        }

        public abstract override string ToString();
    }
}
