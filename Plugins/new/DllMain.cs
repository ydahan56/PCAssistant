using FluentScheduler;
using New.Jobs;
using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;

namespace New
{
    public class DllMain : Plugin
    {
        private IPCAssistant _telegram;

        public DllMain()
        {
            this.Name = "/new";
            this.Description = "Create a new instance of PCAssistant.";
        }

        public override void Dispatch()
        {
            this._telegram.SendTextBackToAdmin("PCAssistant is restarting...");

            // we run the job in 5 seconds to allow
            // the bot client to observe the message
            // and prevent it from going into an endless loop

            var _Job = new RestartJob();
            JobManager.Initialize(_Job);
        }

        public override void Dispatch(ExecuteResult data)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IServiceLocator service)
        {
            this._telegram = service.ResolveInstance<IPCAssistant>();
        }
    }
}
