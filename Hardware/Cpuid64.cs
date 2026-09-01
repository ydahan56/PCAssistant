using FluentScheduler;
using Hardware.Sdk;
using System.Text;

namespace Hardware
{
    public class Cpuid64 : Registry, IJob
    {
        public CpuIdSdk64 Sdk64 { get; private set; }


        private static object _mutex = new object();

        public static Cpuid64 Instance { get; } = new Cpuid64();

        private Cpuid64()
        {
            this.Schedule(this).NonReentrant().ToRunEvery(1).Seconds();
        }

        public void InitSDK(string workingDirectory)
        {
            this.Sdk64 = new CpuIdSdk64(workingDirectory, "cpuidsdk64.dll", out bool sdkloaded);

            var sb = new StringBuilder();
            sb.AppendLine("CPUID failed to initialize.");
            sb.AppendLine("Disable memory integrity in windows defender.");

            if (!sdkloaded)
            {
                System.Diagnostics.Debug.WriteLine(sb.ToString());
            }
        }

        public void Dispose()
        {
            lock (_mutex)
            {
                this.Sdk64.UninitSDK();
            }

        }

        public void Execute()
        {
            lock (_mutex)
            {
                this.Sdk64.RefreshInformation();
            }
        }
    }
}
