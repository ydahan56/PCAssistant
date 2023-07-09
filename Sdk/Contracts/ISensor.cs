namespace Sdk.Contracts
{
    public interface ISensor
    {
        string Name { get; }
        float Value { get; }
        float Min { get; }
        float Max { get; }
    }
}
