using Microsoft.AspNetCore.Mvc;
using QueueAPIWithBackgroundService.Queues;

namespace QueueAPIWithBackgroundService.Controllers;

[ApiController]
[Route("[controller]")]
public class QueueController : ControllerBase
{
    private readonly IBackgroundTaskQueue<string> _backgroundTaskQueue;

    public QueueController(IBackgroundTaskQueue<string> backgroundTaskQueue)
    {
        _backgroundTaskQueue = backgroundTaskQueue;
    }

    /// <summary>
    /// Buraya eklenen string array verisi kuyruğa eklenir ve db insert edildiği simüle edilir.
    /// Veriler eklenmeden response 200 durum kodu dönülür işlemler arka tarafta çalışmaya devam etmektedir.
    /// Hemen arkasına yeni bir string array verisi eklenirse yine kabul edilir.
    /// Belirlenen kuyruk kapasitesine ulaşılması halinde endpoint kuyruğa veri eklenene kadar 200 durum kodunu dönmeyecektir.
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Names(string[] names)
    {
        foreach (var name in names)
        {
            await _backgroundTaskQueue.EnQueue(name);
        }

        return Ok();
    }

    /// <summary>
    /// Aynı kuyruğu başka bir endpoint ile veri eklenebildiği sunulmak amaçlanmıştır.
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
    [HttpPost("Second")]
    public async Task<IActionResult> NamesSecond(string[] names)
    {
        foreach (var name in names)
        {
            await _backgroundTaskQueue.EnQueue(name);
        }

        return Ok();
    }
}