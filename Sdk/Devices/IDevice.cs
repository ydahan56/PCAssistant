using Sdk.Contracts;

namespace Sdk.Devices
{
    public interface IDevice
    {
        string DeviceName { get; }
        ISensor GetSensor(int cls_sensor);
        IEnumerable<ISensor> GetSensors(int cls_sensor);
        string ToString();
    }
}
