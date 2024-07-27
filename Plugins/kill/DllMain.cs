using CommandLine;
using Sdk.Base;
using Sdk.Dependencies;
using Sdk.Models;
using Sdk.Telegram;
using System.Diagnostics;

namespace kill
{
    public class DllMain : Plugin
    {
        [Verb("/kill")]
        public class Args
        {
            [Value(0)]
            public int PID { get; set; }
        }

        private IPCAssistant _telegram;

        public DllMain()
        {
            this.Name = "/kill";
            this.Args = "(\\d+)";
            this.Description = "Kill a task by its id.";
        }

        public override void Dispatch()
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(ExecuteResult data)
        {
            Parser.Default.ParseArguments<Args>(data.Args).WithParsed(this.KillEvent);
        }

        public override void Initialize(IServiceLocator service)
        {
            this._telegram = service.ResolveInstance<IPCAssistant>();
        }

        private void KillEvent(Args args)
        {
            Process target;
            string text;

            try
            {
                target = Process.GetProcessById(args.PID);
            }
            catch (Exception e)
            {
                this._telegram.SendTextBackToAdmin(e.Message);
                return;
            }

            try
            {
                target.Kill();
                text = $"{target.ProcessName} killed.";
            }
            catch (Exception e)
            {
                text = e.Message;
            }

            this._telegram.SendTextBackToAdmin(text);
        }
    }
}
