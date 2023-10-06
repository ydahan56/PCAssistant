using CommandLine;
using FluentScheduler;
using pwrcfg.Commands;
using pwrcfg.Jobs;
using Sdk.Base;
using Sdk.Clients;
using Sdk.Containers;
using Sdk.Models;

namespace pwrcfg
{
    public class DllMain : PluginBase
    {
        private class TimeoutBase
        {
            [Value(0, Default = 0)]
            public int Timeout { get; set; }
        }

        [Verb("lock")]
        private class Lock : TimeoutBase
        {
        }

        [Verb("logoff")]
        private class Logoff : TimeoutBase
        {
        }

        [Verb("sleep")]
        private class Sleep : TimeoutBase
        {
        }

        [Verb("shutdown")]
        private class Shutdown : TimeoutBase
        {
        }

        [Verb("reboot")]
        private class Reboot : TimeoutBase
        {
        }

        private ITGBotClient _client;
        private readonly Parser _parser;

        public DllMain()
        {
            base.Name = "/pwrcfg";
            base.ArgsPattern = "lock|logoff|sleep|reboot|shutdown (\\d)";
            base.HasArguments = true;
            base.Description = "Lock, logoff, sleep, reboot or shutdown the workstation.";

            this._parser = new Parser(with =>
            {
                with.EnableDashDash = false;
            });
        }

        public override void Dispatch()
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(DispatchData data)
        {
            this._parser.ParseArguments<Lock, Logoff, Sleep, Reboot, Shutdown>(data.Args)
                .WithParsed<Lock>(this.Lock_Event)
                .WithParsed<Logoff>(this.Logoff_Event)
                .WithParsed<Sleep>(this.Sleep_Event)
                .WithParsed<Reboot>(this.Reboot_Event)
                .WithParsed<Shutdown>(this.Shutdown_Event);
        }

        private void Lock_Event(Lock e)
        {
            this._client.SendTextBackToAdmin($"lock called with timeout {e.Timeout}");

            var _Job = new LockJob(e.Timeout);
            JobManager.Initialize(_Job);
        }

        private void Logoff_Event(Logoff e)
        {
            this._client.SendTextBackToAdmin($"logoff called with timeout {e.Timeout}");

            var _Job = new LogoffJob(e.Timeout);
            JobManager.Initialize(_Job);
        }

        private void Sleep_Event(Sleep e)
        {
            this._client.SendTextBackToAdmin($"sleep called with timeout {e.Timeout}");

            var _Job = new SleepJob(e.Timeout);
            JobManager.Initialize(_Job);
        }

        private void Shutdown_Event(Shutdown e)
        {
            this._client.SendTextBackToAdmin($"shutdown called with timeout {e.Timeout}");

            var _Job = new ShutdownJob(e.Timeout);
            JobManager.Initialize(_Job);
        }

        private void Reboot_Event(Reboot e)
        {
            this._client.SendTextBackToAdmin($"reboot called with timeout {e.Timeout}");

            var _Job = new RebootJob(e.Timeout);
            JobManager.Initialize(_Job);
        }

        public override void Init(IDependencyService service)
        {
            this._client = service.ResolveInstance<ITGBotClient>();
        }
    }
}
