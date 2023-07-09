using FluentScheduler;
using System.Diagnostics;

namespace pwrcfg.Commands
{
    public class ShutdownJob : Registry, IJob
    {
        private readonly ProcessStartInfo _si;

        public ShutdownJob(int timeout = 0)
        {
            this._si = new ProcessStartInfo("shutdown", $"/s /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };

            base.Schedule(this).ToRunOnceIn(timeout).Seconds();
        }

        public void Execute()
        {
            Process.Start(this._si);
        }
    }
}
