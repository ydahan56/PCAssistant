using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;

namespace Sdk.Contracts
{
    public interface IPlugin
    {
        // This is used to get the plugin and invoke on a separate thread
        (bool success, Plugin? result) TryGetPlugin(string[] args);

        // todo - remove? maybe we dont need to pass services from Agent to plugins
        void Initialize(IServiceLocator services);

      //  void SetExecuteArgs(string[] args);
        void SetExecuteResultCallback(Action<ExecuteResult> callback);
     

        string ToString();
    }
}
