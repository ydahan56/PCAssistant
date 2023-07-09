using FluentScheduler;
using pwrcfg.Helpers;

namespace pwrcfg.Jobs
{
    public class LogoffJob : Registry, IJob
    {
        public LogoffJob(int timeout = 0)
        {
            base.Schedule(this).ToRunOnceIn(timeout).Seconds();
        }

        public void Execute()
        {
            User32Helper.ExitWindowsEx(User32Helper.EWX_LOGOFF, 0);
        }
    }
}
