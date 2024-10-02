using crontemp.Enums;
using crontemp.Models;
using FluentScheduler;
using Sdk.Devices;
using static Hardware.Sdk.CpuIdSdk64;

namespace crontemp.Jobs
{
    public class CronTempJob : Registry, IJob
    {
        private readonly DateTime elapsedDateTime;

        private readonly IEnumerable<IDevice> devices;
        private readonly Action<UpdateStatus, UpdateArgs?> update;

        public CronTempJob(
            IEnumerable<IDevice> devices,
            Action<UpdateStatus, UpdateArgs?> update,
            int total,
            int timeout
            )
        {
            this.devices = devices;
            this.update = update;

            this.elapsedDateTime = DateTime.Now.AddSeconds(total);
            this.Schedule(this).NonReentrant()
                .WithName(this.GetType().Name)
                .ToRunNow().AndEvery(timeout).Seconds();
        }

        public void Execute()
        {
            if (DateTime.Now >= this.elapsedDateTime)
            {
                this.update(UpdateStatus.Elapsed, null);
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

                this.update(UpdateStatus.Append, args);
            }

            this.update(UpdateStatus.Send, null);
        }
    }
}
