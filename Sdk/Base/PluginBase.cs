using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Models;

namespace Sdk.Base
{
    public abstract class PluginBase : IPlugin
    {
        public string? Name { get; protected set; }
        public string? ArgsPattern { get; protected set; } = string.Empty;
        public bool HasArguments => !string.IsNullOrWhiteSpace(ArgsPattern);
        public string? Description { get; protected set; }

        public abstract void Dispatch();
        public abstract void Dispatch(DispatchData data);
        public abstract void Init(IDependencyService service);

        public override string ToString()
        {
            return $"*{(HasArguments ? Name + " " + ArgsPattern : Name)}* - {Description}";
        }
    }
}
