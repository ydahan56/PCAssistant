﻿using FluentScheduler;
using Sdk.Hub;

namespace New.Jobs
{
    public class RestartJob : Registry, IJob
    {

        public RestartJob()
        {
            this.Schedule(this).ToRunOnceIn(5).Seconds();
        }

        public void Execute()
        {
            Mediator.Instance.MessageHub.Publish(ApplicationEvent.Restart);
        }
    }
}
