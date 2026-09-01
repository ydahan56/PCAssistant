using System.Collections.Concurrent;

namespace Common.Queue
{
    public abstract class SimpleMessageQueue<T> : ISimpleMessageQueue<T>
    {
        private readonly ConcurrentQueue<T> _queue;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Task _processorTask;

        protected SimpleMessageQueue()
        {
            _queue = new ConcurrentQueue<T>();
            _cancellationTokenSource = new CancellationTokenSource();
            _processorTask = Task.Run(ProcessQueueAsync, _cancellationTokenSource.Token);
        }

        public void Enqueue(T message)
        {
            _queue.Enqueue(message);
        }

        private async Task ProcessQueueAsync()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                while (_queue.TryDequeue(out var item))
                {
                    try
                    {
                        HandleMessage(item);
                    }
                    catch (Exception ex)
                    {
                        OnProcessingError(item, ex);
                    }
                }

                await Task.Delay(100);
            }
        }

        public void StopAndWait()
        {
            _cancellationTokenSource.Cancel();
            _processorTask.Wait(); // Ensure the task completes before stopping
        }

        // 👇 ABSTRACT method: forces subclass to implement this
        protected abstract void HandleMessage(T message);

        // 👇 VIRTUAL method: optional override for error handling
        protected virtual void OnProcessingError(T message, Exception ex)
        {
            Console.Error.WriteLine($"Error processing message: {ex.Message}");
        }
    }
}
