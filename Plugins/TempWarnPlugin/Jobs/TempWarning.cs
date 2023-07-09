using Components.Base;
using Components.Models;
using DotNetEnv;
using FluentScheduler;
using Sdk.Jobs;
using TempWarnPlugin.Models;
using static Components.Sdk.CpuIdSdk64;

namespace TempWarnPlugin.Jobs
{
    public class TempWarning : IWorker<TempArgs>, IJob
    {
        private readonly float cpuLimit;
        private readonly float gpuLimit;

        private readonly Dictionary<uint, float> limits;
        private readonly IEnumerable<IDevice> devices;

        public TempWarning(IEnumerable<IDevice> devices)
        {
            Schedule(this).WithName(GetType().Name).NonReentrant().ToRunNow().AndEvery(7).Seconds();

            this.devices = devices;

            cpuLimit = Convert.ToSingle(Env.GetDouble("CPU_WARNING_LIMIT"));
            gpuLimit = Convert.ToSingle(Env.GetDouble("GPU_WARNING_LIMIT"));

            limits = new Dictionary<uint, float>
            {
                { CLASS_DEVICE_PROCESSOR, cpuLimit },
                { CLASS_DEVICE_DISPLAY_ADAPTER, gpuLimit }
            };

            Active = Env.GetBool("enabled");

            if (Active)
            {
                JobManager.Initialize(this);
            }
        }

        public override void Start()
        {
            if (Active)
            {
                RaiseFeedback("Temperature is already being monitored.");
                return;
            }

            JobManager.Initialize(this);
            Active = true;
        }

        public override void Stop()
        {
            if (!Active)
            {
                RaiseFeedback("Temperature is not being monitored.");
                return;
            }

            JobManager.RemoveJob(GetType().Name);
            Active = false;
        }

        public void Execute()
        {
            foreach (IDevice device in devices)
            {
                Sensor sensor = device.GetSensor(SENSOR_CLASS_TEMPERATURE);

                bool success = limits.TryGetValue(device.DeviceClass, out float limit);

                if (success && sensor.Value >= limit)
                {
                    var args = new TempArgs
                    {
                        DeviceName = device.DeviceName,
                        Temperature = sensor.Value
                    };

                    RaiseUpdate(args);
                }
            }
        }
    }
}
