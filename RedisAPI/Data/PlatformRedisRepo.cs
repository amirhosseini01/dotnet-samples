using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data;
public class PlatformRedisRepo : IPlatformRepo
{
    public PlatformRedisRepo(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public async Task Create(Platform platform)
    {
        if (platform is null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        var db = _connectionMultiplexer.GetDatabase();

        string platStr = JsonSerializer.Serialize(platform);

        await db.StringSetAsync(platform.Id, platStr);
    }

    public IEnumerable<Platform> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Platform?> GetById(string id)
    {
        var db = _connectionMultiplexer.GetDatabase();

        var platStr = await db.StringGetAsync(id);

        if(!string.IsNullOrEmpty(platStr))
        {
            return JsonSerializer.Deserialize<Platform>(platStr);
        }

        return null;
    }
}