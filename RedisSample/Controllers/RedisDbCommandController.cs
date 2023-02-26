using Microsoft.AspNetCore.Mvc;
using RedisSample.Services.Redis;

namespace RedisSample.Controllers;

[ApiController]
[Route("[controller]")]
public class RedisDbCommandController : ControllerBase
{
    private readonly RedisService _redisService;
    public RedisDbCommandController(RedisService redisService)
    {
        _redisService = redisService;
    }

    [HttpGet(nameof(SetItem))]
    public async Task<bool> SetItem(string key = "myKey", string value = "myValue")
    {
        return await _redisService.SetAsync(key, value);
    }

    [HttpGet(nameof(GetItem))]
    public async Task<IActionResult> GetItem(string key = "myKey")
    {
        var item = await _redisService.GetAsync(key);
        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}
