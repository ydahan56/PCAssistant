using FluentScheduler;
using Sdk.Contracts;
using Sdk.Dependencies;
using Sdk.Models;

namespace Sdk.Base
{
    public abstract class Plugin : Registry, IPlugin, IJob
    {
        protected Action<ExecuteResult> ExecuteResultCallback;

        public abstract void Execute();

        public virtual void Initialize(IServiceLocator services) // todo - remove?
        {
            throw new NotImplementedException();
        }

        public virtual void SetExecuteResultCallback(Action<ExecuteResult> callback)
        {
            this.ExecuteResultCallback = callback;
        }

        public override string ToString()
        {
            return "";//$"*{this.Name + " " + this.Args.ToString()}* - {this.Description}";
        }
    }
}
