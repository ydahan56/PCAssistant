using Sdk.Devices;

namespace Sdk.Contracts
{
    public interface ICpuidHelper
    {
        IEnumerable<IDevice> GetAll();
        IEnumerable<IDevice> GetSelected(uint[] _device_classes);
        IEnumerable<IDevice> GetProcessors();
        IEnumerable<IDevice> GetDisplayAdapters();
        IEnumerable<IDevice> GetMainboards();
        IEnumerable<IDevice> GetDrives();
        IEnumerable<IDevice> GetBatteries();
    }
}
