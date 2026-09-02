using Sdk.Dependencies;
using Sdk.Models;

namespace Sdk.Contracts
{
    public interface IPlugin
    {
        // todo - remove? maybe we dont need to pass services from Agent to plugins
        IPlugin Initialize(IServiceResolver services);

        IPlugin SetExecuteContext(ExecuteParameters parameters);

        IPlugin SetExecuteResultCallback(Action<ExecuteContext> callback);

        IPlugin SetExecutionSchedule();

        string ToString();
    }
}
