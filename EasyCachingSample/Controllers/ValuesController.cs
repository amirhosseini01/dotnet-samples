using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;

namespace EasyCachingSample.Controllers;

[ApiController]
[Route("[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IEasyCachingProviderFactory _factory;
    public ValuesController(IEasyCachingProviderFactory factory)
    {
        _factory = factory;
    }

    [HttpGet(nameof(HandleDefault))]
    public async Task<ActionResult> HandleDefault()
    {
        var provider = _factory.GetCachingProvider("default");

        CacheValue<string> cacheValue = await provider.GetAsync<string>("demo");
        if (cacheValue.HasValue)
            return Ok(cacheValue);

        await provider.SetAsync("demo", "123", TimeSpan.FromSeconds(15));

        return Ok("cache set successfully");
    }

    [HttpGet(nameof(HandleRedis))]
    public async Task<ActionResult> HandleRedis()
    {
        var provider = _factory.GetCachingProvider("redis1");

        CacheValue<string> cacheValue = await provider.GetAsync<string>("demo");
        if (cacheValue.HasValue)
            return Ok(cacheValue);

        await provider.SetAsync("demo", "123", TimeSpan.FromSeconds(15));

        return Ok("cache set successfully");
    }
}
