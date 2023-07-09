using FluentScheduler;
using pwrcfg.Helpers;

namespace pwrcfg.Jobs
{
    public class SleepJob : Registry, IJob
    {
        public SleepJob(int timeout = 0)
        {
            base.Schedule(this).ToRunOnceIn(timeout).Seconds();
        }

        public void Execute()
        {
            PowrprofHelper.SetSuspendState(false, false, false);
        }
    }
}
