using Hardware.Models;
using Sdk.Contracts;
using Sdk.Devices;

namespace Hardware.Base
{
    public abstract class DeviceBase : IDevice
    {
        public string DeviceName { get; private set; }

        protected int DeviceIndex { get; private set; }
        protected uint DeviceClass { get; private set; }

        public DeviceBase()
        {

        }

        public DeviceBase(string deviceName, int deviceIndex, uint deviceClass)
        {
            this.DeviceName = deviceName;
            this.DeviceIndex = deviceIndex;
            this.DeviceClass = deviceClass;
        }

        public ISensor GetSensor(int cls_sensor)
        {
            var sensorId = 0;
            var sensorName = "";
            var rvalue = 0;
            var value = 0.0f;
            var minVal = 0.0f;
            var maxVal = 0.0f;

            Cpuid64.Instance.Sdk64.GetSensorInfos(
                this.DeviceIndex, 0, cls_sensor,
                ref sensorId, ref sensorName,
                ref rvalue, ref value,
                ref minVal, ref maxVal
             );

            return new Sensor(sensorName, value, minVal, maxVal);
        }

        public IEnumerable<ISensor> GetSensors(int cls_sensor)
        {
            var cnt_sensor = Cpuid64.Instance.Sdk64.GetNumberOfSensors(this.DeviceIndex, cls_sensor);

            var sensors = new List<ISensor>(cnt_sensor);

            for (int sensorIndex = 0; sensorIndex < cnt_sensor; sensorIndex++)
            {
                var sensorId = 0;
                var sensorName = "";
                var rvalue = 0;
                var value = 0.0f;
                var minVal = 0.0f;
                var maxVal = 0.0f;

                Cpuid64.Instance.Sdk64.GetSensorInfos(
                    this.DeviceIndex, sensorIndex, cls_sensor,
                    ref sensorId, ref sensorName,
                    ref rvalue, ref value,
                    ref minVal, ref maxVal
                 );

                var sensor = new Sensor(sensorName, value, minVal, maxVal);

                sensors.Add(sensor);
            }

            return sensors;
        }

        public abstract override string ToString();
    }
}
