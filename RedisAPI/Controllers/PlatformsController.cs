using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _platformRepo;
    public PlatformsController(IPlatformRepo platformRepo)
    {
        _platformRepo = platformRepo;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Platform>> Get(string id)
    {
        var plat = await _platformRepo.GetById(id);

        if (plat is null)
            return NotFound();

        return Ok(plat);
    }
    [HttpPost]
    public async Task<ActionResult<Platform>> Post([FromBody] Platform platform)
    {
        await _platformRepo.Create(platform);

        return CreatedAtRoute(nameof(Get), new { id = platform.Id }, platform);
    }

}