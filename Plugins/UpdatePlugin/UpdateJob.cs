using AutoUpdaterDotNET;
using FluentScheduler;

namespace UpdatePlugin
{
    public class UpdateJob : Registry, IJob
    {
        public UpdateJob()
        {
            Schedule(this).WithName(GetType().Name).ToRunNow().AndEvery(5).Hours();
        }
        public void Execute()
        {
            AutoUpdater.Start();
        }
    }
}
