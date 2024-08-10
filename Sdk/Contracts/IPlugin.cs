using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;

namespace Sdk.Contracts
{
    public interface IPlugin
    {
        // todo - remove? maybe we dont need to pass services from Agent to plugins
        void Initialize(IServiceLocator services);

        void SetExecuteResultCallback(Action<ExecuteResult> callback);

        string ToString();
    }
}
