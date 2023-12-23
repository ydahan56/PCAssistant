using CommandLine;
using crontemp.Enums;
using crontemp.Jobs;
using crontemp.Models;
using FluentScheduler;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Contracts;
using Sdk.Devices;
using Sdk.Extensions;
using Sdk.Models;
using System.Text;

namespace crontemp
{
    public class DllMain : PluginBase
    {
        [Verb("off")]
        public class Disable
        {
            //[Value(0)]
            //public string Value { get; set; }
        }

        [Verb("/crontemp", isDefault: true)]
        public class Enable
        {
            [Value(0)]
            public double Total { get; set; }

            [Value(1)]
            public int Timeout { get; set; }
        }

        private ITGBotClient _telegram;
        private IEnumerable<IDevice> _devices;

        private JobState state;
        private readonly string jobName;

        private readonly StringBuilder sb;
        private readonly Parser parser;

        private const string SUCCESS_MESSAGE = "Temperature monitor has been scheduled to run {0} sec for every {1} sec.";
        private const string APPEND_MESSAGE = "*{0}*: {1}°C";

        public DllMain()
        {
            base.Name = "/crontemp";
            base.ArgsPattern = "(\\d+) (\\d+)|off";
            base.Description = "Schedules temperature monitor.";

            this.sb = new StringBuilder();
            this.parser = new Parser(with =>
            {
                with.EnableDashDash = false;
            });

            // init default values
            this.state = JobState.Stopped;
            this.jobName = nameof(CronTempJob);
        }

        public override void Dispatch()
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(DispatchData data)
        {
            this.parser.ParseArguments<Enable, Disable>(data.Args)
                .WithParsed<Enable>(this.Enable_Event)
                .WithParsed<Disable>(this.Disable_Event);
        }

        private void Disable_Event(Disable e)
        {
            if (this.state == JobState.Started)
            {
                JobManager.RemoveJob(this.jobName);
                this.state = JobState.Stopped;
                this._telegram.SendTextBackToAdmin($"{base.Name} has been cancelled.");
                return;
            }
        }

        private void Enable_Event(Enable e)
        {
            if (this.state == JobState.Started)
            {
                this._telegram.SendTextBackToAdmin($"{base.Name} is already running.");
                return;
            }

            // create new job
            var _cron_job = new CronTempJob(
                this._devices,
                this.OnJobUpdate,
                e.Total,
                e.Timeout
            );

            // init job
            JobManager.Initialize(_cron_job);

            // set state
            this.state = JobState.Started;

            // report to client
            this._telegram.SendTextBackToAdmin(string.Format(SUCCESS_MESSAGE, e.Total, e.Timeout));
        }

        private void OnJobUpdate(JobUpdateState status, UpdateArgs? args)
        {
            if (status == JobUpdateState.Elapsed)
            {
                JobManager.RemoveJob(this.jobName);
                this.state = JobState.Stopped;
                this._telegram.SendTextBackToAdmin($"{base.Name} has elapsed.");
                return;
            }

            if (status == JobUpdateState.Append)
            {
                this.sb.AppendLine(string.Format(APPEND_MESSAGE, args.DeviceName, args.Temperature));
                return;
            }

            if (status == JobUpdateState.Send)
            {
                this.sb.AppendLine("\nFrom *Telebot*");
                this._telegram.SendTextBackToAdmin(this.sb.ToString());

                this.sb.Clear();
                return;
            }
        }

        public override void Init(IDependencyService service)
        {
            var cpuid = service.ResolveInstance<ICpuidHelper>();

            this._telegram = service.ResolveInstance<ITGBotClient>();
            this._devices = cpuid.GetProcessors().Concat(cpuid.GetDisplayAdapters()).ToList();
        }

        public string GetStatus()
        {
            return $"*{base.Name}*: {(this.state == JobState.Started).ToReadable()}";
        }
    }
}
