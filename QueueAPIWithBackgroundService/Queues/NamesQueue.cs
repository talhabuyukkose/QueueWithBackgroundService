using System.Threading.Channels;

namespace QueueAPIWithBackgroundService.Queues;

public class NamesQueue : IBackgroundTaskQueue<string>
{
    private readonly Channel<string> queue;

    public NamesQueue(IConfiguration configuration)
    {
        // Kuyruğun kapasitesi belirlenir. Bu kapasitenin üstüne çıkıldığında beklemeye alır ve kuyruk sayısı azaldıkça üstüne ekler.
        int.TryParse(configuration["QueueCapacity"], out int capacity);
        BoundedChannelOptions boundedChannelOptions = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        queue = Channel.CreateBounded<string>(options: boundedChannelOptions);
        
    }
    // Kuyruğa veri ekler.
    public async ValueTask EnQueue(string workerItem)
    {
        ArgumentNullException.ThrowIfNull(workerItem);

        await queue.Writer.WriteAsync(workerItem);
    }

    // Kuyruktan veri okunur.
    public ValueTask<string> DeQueue(CancellationToken cancellationToken)
    {
        var workeditem = queue.Reader.ReadAsync(cancellationToken);

        return workeditem;
    }

    // Kuyruk sayısını verir
    public async ValueTask<int> InstantCount()
    {
        return queue.Reader.Count;
    }
}