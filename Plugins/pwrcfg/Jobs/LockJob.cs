using FluentScheduler;
using pwrcfg.Helpers;

namespace pwrcfg.Jobs
{
    public class LockJob : Registry, IJob
    {
        public LockJob(int timeout = 0)
        {
            base.Schedule(this).ToRunOnceIn(timeout).Seconds();
        }

        public void Execute()
        {
            User32Helper.LockWorkStation();
        }
    }
}
