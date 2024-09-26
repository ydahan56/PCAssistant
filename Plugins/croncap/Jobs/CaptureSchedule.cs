using CapTimePlugin.Core;

namespace Telebot.Capture
{
    public class CaptureSchedule : IWorker<CaptureArgs>, IScheduled
    {
        private DateTime timeStop;

        private void Elapsed()
        {
            if (DateTime.Now >= timeStop)
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
