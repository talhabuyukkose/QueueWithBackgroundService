using QueueAPIWithBackgroundService.Queues;
using NotImplementedException = System.NotImplementedException;

namespace QueueAPIWithBackgroundService.Services;

public class QueueHostedService : BackgroundService
{
    private readonly IBackgroundTaskQueue<string> _backgroundTaskQueue;
    private readonly ILogger<QueueHostedService> _logger;

    public QueueHostedService(IBackgroundTaskQueue<string> backgroundTaskQueue, ILogger<QueueHostedService> logger)
    {
        _backgroundTaskQueue = backgroundTaskQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //  Eğer kuyrukta bir indis yok ise beklemeye devam eder ve böylelikle sürekli çalışmayacaktır.
            var name = await _backgroundTaskQueue.DeQueue(stoppingToken);
            
            // Kuyrukta ki sayıyı verir.
            var count = await _backgroundTaskQueue.InstantCount();

            await Task.Delay(1000); // Db insert simulating

            _logger.LogInformation($"Executed name : {name}");
            _logger.LogInformation($"Executed Queue Count : {count}");
        }
    }
}