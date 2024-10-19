using FluentScheduler;
using Sdk.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exit
{
    internal class ExitJob : Registry, IJob
    {
        public ExitJob()
        {
            this.Schedule(this).ToRunOnceIn(5).Seconds();
        }

        public void Execute()
        {
            EventAggregator.Instance.MessageHub.Publish(ApplicationEvent.Exit);
        }
    }
}
