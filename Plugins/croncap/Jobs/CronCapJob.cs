using FluentScheduler;
using Sdk.Devices;
using croncap.Enums;
using croncap.Models;

namespace croncap.Jobs
{
    public class CronCapJob : Registry, IJob
    {
        private readonly DateTime ElapsedDateTime;
        private readonly Action<UpdateStatus, UpdateArgs?> update;

        public CronCapJob(
            Action<UpdateStatus, UpdateArgs?> update,
            int total,
            int timeout
            )
        {
            this.update = update;

            this.ElapsedDateTime = DateTime.Now.AddSeconds(total);
            base.Schedule(this).NonReentrant().WithName(this.GetType().Name).ToRunNow().AndEvery(timeout).Seconds();
        }

        public void Execute()
        {
            if (DateTime.Now >= ElapsedDateTime)
            {
                Stop();
                RaiseFeedback("scheduled capture has finished.");
                return;
            }

            var api = new DesktopApi();

            api.Invoke((screens) =>
            {
                foreach (Bitmap screen in screens)
                {
                    var result = new CaptureArgs
                    {
                        Capture = screen
                    };

                    RaiseUpdate(result);
                }
            });
        }

        public void Start(int durationSec, int intervalSec)
        {
            if (Active)
            {
                RaiseFeedback("Screen capture has already been scheduled.");
                return;
            }

            timeStop = DateTime.Now.AddSeconds(durationSec);

            JobManager.AddJob(
                Elapsed,
                s => s.WithName(GetType().Name).ToRunNow().AndEvery(intervalSec).Seconds()
            );
            Active = true;
        }

        public override void Stop()
        {
            if (!Active)
            {
                RaiseFeedback("Screen capture has not been scheduled.");
                return;
            }

            JobManager.RemoveJob(GetType().Name);
            Active = false;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}
