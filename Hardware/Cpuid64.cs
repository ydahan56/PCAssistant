using FluentScheduler;
using Hardware.Sdk;
using System.Text;

namespace Hardware
{
    public class Cpuid64 : IDisposable
    {
        public class CPUID64Refresh : Registry, IJob
        {
            private readonly CpuIdSdk64 _cpuid64;

            public CPUID64Refresh(CpuIdSdk64 cpuid64)
            {
                this._cpuid64 = cpuid64;

                this.Schedule(this).NonReentrant().ToRunEvery(1).Seconds();
            }

            public void Execute()
            {
                //this._cpuid64.RefreshInformation();
            }
        }

        public CpuIdSdk64 Sdk64 { get; private set; }


        private static Cpuid64 _instance = null;
        private static readonly object _mutex = new object();

        private readonly CPUID64Refresh _refreshJOB;

        public static Cpuid64 Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new Cpuid64();
                        }
                    }
                }

                return _instance;
            }
        }

        private Cpuid64()
        {
            this._refreshJOB = new CPUID64Refresh(this.Sdk64);
        }

        public CPUID64Refresh GetRefreshJob()
        {
            return this._refreshJOB;
        }

        public void InitSDK(string workingDirectory)
        {
            this.Sdk64 = new CpuIdSdk64(
                workingDirectory,
                "cpuidsdk64.dll",
                out bool success
            );

            var sb = new StringBuilder();
            sb.AppendLine("CPUID failed to initialize.");
            sb.AppendLine("Check AV settings preventing the driver from loading");

            //if (!success)
            //    throw new Exception(sb.ToString());
        }

        public void Dispose()
        {
            this.Sdk64.UninitSDK();
        }

        public void Execute()
        {
            this.Sdk64.RefreshInformation();
        }
    }
}
