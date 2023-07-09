namespace Plugins.NSSpec.Sensors.Contracts
{
    public abstract class ISensor
    {
        public string Name { get; protected set; }
        public int Class { get; protected set; }
    }
}
