using System.Runtime.InteropServices;

namespace pwrcfg.Helpers
{
    public static class PowrprofHelper
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern uint SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
    }
}
