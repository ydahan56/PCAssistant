using Hardware;
using Hardware.Devices;
using Sdk.Contracts;
using Sdk.Devices;
using static Hardware.Sdk.CpuIdSdk64;

namespace Agent.Helpers
{
    public class CpuidHelper : ICpuidHelper
    {
        private readonly int deviceCount;

        private readonly Dictionary<uint, Type> deviceTypes;
        private readonly Dictionary<uint, IList<IDevice>> deviceMap;

        private const uint CLASS_DEVICE_ALL = 0;

        public CpuidHelper()
        {
            this.deviceCount = Cpuid64.Instance.Sdk64.GetNumberOfDevices();

            // device types
            this.deviceTypes = new Dictionary<uint, Type>()
            {
                { CLASS_DEVICE_PROCESSOR, typeof(Processor) },
                { CLASS_DEVICE_DISPLAY_ADAPTER, typeof(Display) },
                { CLASS_DEVICE_MAINBOARD, typeof(Mainboard) },
                { CLASS_DEVICE_DRIVE, typeof(Drive) },
                { CLASS_DEVICE_BATTERY, typeof(Battery) }
            };

            // device lists
            this.deviceMap = new Dictionary<uint, IList<IDevice>>
            {
                { CLASS_DEVICE_ALL, new List<IDevice>() },
                { CLASS_DEVICE_PROCESSOR, new List<IDevice>() },
                { CLASS_DEVICE_DISPLAY_ADAPTER, new List<IDevice>() },
                { CLASS_DEVICE_MAINBOARD, new List<IDevice>() },
                { CLASS_DEVICE_DRIVE, new List<IDevice>() },
                { CLASS_DEVICE_BATTERY, new List<IDevice>() }
            };

            this.InitDeviceMap();
        }

        public IEnumerable<IDevice> GetAll()
        {
            return this.deviceMap[CLASS_DEVICE_ALL];
        }

        public IEnumerable<IDevice> GetSelected(uint[] _device_classes)
        {
            var list = new List<IDevice>();

            foreach (var key in this.deviceMap.Keys)
            {
                if (_device_classes.Contains(key))
                {
                    list.AddRange(this.deviceMap[key]);
                }
            }

            return list;
        }

        public IEnumerable<IDevice> GetProcessors()
        {
            return this.deviceMap[CLASS_DEVICE_PROCESSOR];
        }

        public IEnumerable<IDevice> GetDisplayAdapters()
        {
            return this.deviceMap[CLASS_DEVICE_DISPLAY_ADAPTER];
        }

        public IEnumerable<IDevice> GetMainboards()
        {
            return this.deviceMap[CLASS_DEVICE_MAINBOARD];
        }

        public IEnumerable<IDevice> GetDrives()
        {
            return this.deviceMap[CLASS_DEVICE_DRIVE];
        }

        public IEnumerable<IDevice> GetBatteries()
        {
            return this.deviceMap[CLASS_DEVICE_BATTERY];
        }

        private void InitDeviceMap()
        {
            for (int _index = 0; _index < deviceCount; _index++)
            {
                var _class = (uint)Cpuid64.Instance.Sdk64.GetDeviceClass(_index);

                if (this.deviceMap.ContainsKey(_class))
                {
                    // grab device type and list
                    var type = this.deviceTypes[_class];
                    var list = this.deviceMap[_class];
                    var _all = this.deviceMap[CLASS_DEVICE_ALL];

                    // grab device name
                    string _name = Cpuid64.Instance.Sdk64.GetDeviceName(_index);

                    // create device instance
                    var instance = (IDevice)Activator.CreateInstance(type, _name, _index, _class);

                    // add to list
                    list.Add(instance);
                    _all.Add(instance);
                }
            }
        }
    }
}