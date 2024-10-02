using FluentScheduler;
using Sdk.Devices;
using croncap.Enums;
using croncap.Models;
using System.Drawing;

namespace croncap.Jobs
{
    public class CronCapJob : Registry, IJob
    {
        private readonly ScreenUtilities screenUtilities;
        private readonly DateTime elapsedDateTime;
        private readonly Action<UpdateStatus, UpdateArgs?> update;

        public CronCapJob(
            Action<UpdateStatus, UpdateArgs?> update,
            int total,
            int timeout
            )
        {
            this.update = update;
            this.elapsedDateTime = DateTime.Now.AddSeconds(total);
            this.screenUtilities = new ScreenUtilities();
            
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

            var screens = this.screenUtilities.GetDesktopsBitmap();

            foreach (Bitmap screen in screens)
            {
                var args = new UpdateArgs
                {
                    Capture = screen
                };

                this.update(UpdateStatus.Send, args);
            }
        }
    }
}
