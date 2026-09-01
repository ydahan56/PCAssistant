using Agent.Startup;
using FluentScheduler;
using Hardware;
using Sdk.Telegram;
using Telegram.Bot.Polling;

namespace Agent
{
    public class Main : ApplicationContext
    {
        private readonly IPCAssistant _client;
        private readonly IUpdateHandler _updateHandler;
        public Main(IPCAssistant client, IBootstrapper bootstrapper, IUpdateHandler updateHandler)
        {
            this._client = client;
            this._updateHandler = updateHandler;

            // init startup and refresh job
            JobManager.Initialize(bootstrapper.GeInstance(), Cpuid64.Instance);

            // start telegram polling
            this._client.StartReceiving(this._updateHandler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._client.Cancel();
            }
            base.Dispose(disposing);
        }
    }
}
