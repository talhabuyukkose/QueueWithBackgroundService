namespace QueueAPIWithBackgroundService.Queues;

public interface IBackgroundTaskQueue<T>
{
    ValueTask EnQueue(T workerItem);

    ValueTask<T> DeQueue(CancellationToken cancellationToken);

    ValueTask<int> InstantCount();
}