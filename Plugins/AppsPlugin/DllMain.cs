using AppsPlugin.Core;
using AppsPlugin.Enums;

namespace Plugins.Apps
{
    public class DllMain : IModule
    {
        private readonly Dictionary<string, Session> types;

        public DllMain()
        {
            base.Name = "/apps";
            base.Arguments = "(fg|all)";
            base.HasArguments = true;
            base.Description = "List of active applications.";

            this.types = new Dictionary<string, Session>
            {
                { "fg", Session.Foreground },
                { "all", Session.Background }
            };
        }

        public override void Execute(ExecData data)
        {
            var key = data.Arguments[1].Value;
            var type = this.types[key];
            var api = new AppsApi(type);

            api.Invoke(async s =>
            {
                await this.Client.SendTextMessageAsync(ChatId, s, replyToMessageId: data.FromMessageId);
            });
        }
    }
}
