using Sdk.Containers;
using Sdk.Models;

namespace Sdk.Contracts
{
    public interface IPlugin
    {
        string? Name { get; }
        string? ArgsPattern { get; }
        bool HasArguments { get; }
        string? Description { get; }

        void Dispatch();
        void Dispatch(DispatchData data);
        void Init(IDependencyService service);

        string ToString();
    }
}
