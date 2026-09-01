using FluentScheduler;
using Sdk.Hub;

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
