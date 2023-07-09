using Components.Base;
using Plugins.NSSpec.Components;
using Plugins.NSSpec.Sensors;
using Plugins.NSSpec.Sensors.Contracts;
using SimpleInjector;
using System.Text;

namespace Plugins.NSSpec
{
    public class Spec
    {
        private readonly IEnumerable<IDevice> cpus;
        private readonly IEnumerable<IDevice> hdds;
        private readonly IEnumerable<IDevice> gpus;
        private readonly IEnumerable<IDevice> bats;
        private readonly IEnumerable<IDevice> rams;

        public Spec(IServiceProvider ioc)
        {
            var container = (Container)ioc.GetService(typeof(Container));

            cpus = container.GetAllInstances<IProcessor>();
            hdds = container.GetAllInstances<IDrive>();
            gpus = container.GetAllInstances<IDisplay>();
            bats = container.GetAllInstances<IBattery>();
            rams = container.GetAllInstances<IMainboard>();
        }

        public string GetInfo()
        {
            var text = new StringBuilder();

            IComponent[] components = {
                new Processor(
                    cpus,
                    new ISensor[]
                    {
                        new Voltage(),
                        new Temperature(),
                        new Power(),
                        new Utilization(),
                        new ClockSpeed()
                    }
                ),
                new Storage(
                    hdds,
                    new ISensor[]
                    {
                        new Temperature(),
                        new Utilization()
                    }
                ),
                new Display(
                    gpus,
                    new ISensor[]
                    {
                        new Temperature()
                    }
                ),
                new Battery(
                    bats,
                    new ISensor[]
                    {
                        new Voltage(),
                        new Capacity(),
                        new Level()
                    }
                ),
                new Mainboard(
                    rams,
                    new ISensor[]
                    {
                        new Utilization()
                    }
                )
            };

            foreach (IComponent component in components)
            {
                text.Append(component);
            }

            return text.ToString();
        }
    }
}
