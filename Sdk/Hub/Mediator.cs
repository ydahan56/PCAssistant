using Easy.MessageHub;

namespace Sdk.Hub
{
    public sealed class Mediator
    {
        public static Mediator Instance { get; } = new Mediator();

        public IMessageHub MessageHub;

        private Mediator()
        {
            this.MessageHub = new MessageHub();
        }
    }
}
