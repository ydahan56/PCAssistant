using Easy.MessageHub;

namespace Sdk.Hub
{
    public sealed class EventAggregator
    {
        public static EventAggregator Instance { get; } = new EventAggregator();

        public IMessageHub MessageHub;

        private EventAggregator()
        {
            this.MessageHub = new MessageHub();
        }
    }
}
