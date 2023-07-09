using crontemp.Enums;
using crontemp.Models;
using FluentScheduler;
using Sdk.Devices;
using static Hardware.Sdk.CpuIdSdk64;

namespace crontemp.Jobs
{
    public class CronTempJob : Registry, IJob
    {
        private readonly DateTime deadline;

        private readonly IEnumerable<IDevice> devices;
        private readonly Action<JobUpdateState, UpdateArgs?> update;

        public CronTempJob(
            IEnumerable<IDevice> devices,
            Action<JobUpdateState, UpdateArgs?> update,
            double total,
            int timeout)
        {
            this.devices = devices;
            this.update = update;

            this.deadline = DateTime.Now.AddSeconds(total);
            base.Schedule(this).NonReentrant().WithName(this.GetType().Name).ToRunNow().AndEvery(timeout).Seconds();
        }

        public void Execute()
        {
            if (DateTime.Now >= this.deadline)
            {
                this.update(JobUpdateState.Elapsed, null);
                return;
            }

            foreach (IDevice device in devices)
            {
                var sensor = device.GetSensor(SENSOR_CLASS_TEMPERATURE);

                var args = new UpdateArgs
                {
                    DeviceName = device.DeviceName,
                    Temperature = sensor.Value
                };

                this.update(JobUpdateState.Append, args);
            }

            this.update(JobUpdateState.Send, null);
        }
    }
}
