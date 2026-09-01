namespace Common.Queue
{
    public interface ISimpleMessageQueue<T>
    {
        void Enqueue(T message);
        void StopAndWait();
    }
}