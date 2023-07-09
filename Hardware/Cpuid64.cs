using Hardware.Sdk;
using FluentScheduler;

namespace Hardware
{
    public sealed class Cpuid64 : Registry, IJob
    {
        public CpuIdSdk64 Sdk64 { get; private set; }

        public static Cpuid64 Instance = new Cpuid64();

        private Cpuid64()
        {
            var dllDirPath = Path.GetFullPath(".\\");
            this.Sdk64 = new CpuIdSdk64(dllDirPath, out bool success);

            if (!success)
                throw new Exception("CpuIdSdk64 failed to initialize.");

            this.Schedule(this).NonReentrant().ToRunEvery(1).Seconds();
        }

        public void Execute()
        {
            this.Sdk64.RefreshInformation();
        }
    }
}
