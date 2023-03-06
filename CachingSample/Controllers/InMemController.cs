using CachingSample.MemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CachingSample.Controllers;

[ApiController]
[Route("[controller]")]
public class InMemController : ControllerBase
{
    private readonly InMemServices _inMemServices;
    public InMemController(InMemServices inMemServices)
    {
        _inMemServices = inMemServices;
    }

    [HttpGet(nameof(TryGetCacheValue))]
    public ActionResult TryGetCacheValue(CacheKeys cacheKey)
    {
        var cacheValue = _inMemServices.TryGetCacheValue<string>(cacheKey);
        if (cacheValue is null)
            return NotFound();

        return Ok(cacheValue);
    }

    [HttpGet(nameof(SetAbsoluteExpiration))]
    public ActionResult SetAbsoluteExpiration(CacheKeys cacheKey, string cacheValue,
        int expirationSecond, long? cacheSize)
    {
        _inMemServices.SetAbsoluteExpiration(cacheKey: cacheKey,
            cacheValue: cacheValue,
             expiration: TimeSpan.FromSeconds(expirationSecond), cacheSize: cacheSize);
        return Ok();
    }

    [HttpGet(nameof(SetSlidingExpiration))]
    public ActionResult SetSlidingExpiration(CacheKeys cacheKey, string cacheValue,
        int expirationSecond, int absoluteExpirationSecond, long? cacheSize)
    {
        _inMemServices.SetSlidingExpiration(cacheKey: cacheKey,
            cacheValue: cacheValue, expiration: TimeSpan.FromSeconds(expirationSecond),
             absoluteExpiration: TimeSpan.FromSeconds(absoluteExpirationSecond),
              cacheSize: cacheSize);

        return Ok();
    }

    [HttpGet(nameof(TestOutputCache))]
    [OutputCache]
    public ActionResult TestOutputCache()
    {
        return Ok(DateTime.Now);
    }

    [HttpGet(nameof(TestResponseCache))]
    [ResponseCache(Duration = 10)]
    public ActionResult TestResponseCache()
    {
        return Ok(DateTime.Now);
    }
}
