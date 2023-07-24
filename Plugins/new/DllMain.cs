﻿using FluentScheduler;
using New.Jobs;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Models;

namespace New
{
    public class DllMain : PluginBase
    {
        private ITGBotClient _client;

        public DllMain()
        {
            this.Name = "/new";
            this.HasArguments = false;
            this.Description = "Create a new instance of PCAssistant.";
        }

        public override void Dispatch()
        {
            this._client.SendText("PCAssistant is restarting...");

            // we run the job in 5 seconds to allow
            // the bot client to observe the message
            // and prevent it from going into an endless loop

            var _Job = new RestartJob();
            JobManager.Initialize(_Job);
        }

        public override void Dispatch(DispatchData data)
        {
            throw new NotImplementedException();
        }

        public override void Init(IDependencyService service)
        {
            this._client = service.ResolveInstance<ITGBotClient>();
        }
    }
}
