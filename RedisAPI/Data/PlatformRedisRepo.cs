using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data;
public class PlatformRedisRepo : IPlatformRepo
{
    public const string hashKey = "hashPlatform";
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
        // await db.StringSetAsync(platform.Id, platStr);

        await db.HashSetAsync(hashKey, new HashEntry[]
        {new HashEntry(platform.Id, platStr)});
    }

    public async Task<IEnumerable<Platform?>?> GetAll()
    {
        var db = _connectionMultiplexer.GetDatabase();

        var completeHash = await db.HashGetAllAsync(hashKey);

        if (completeHash.Length > 0)
        {
            var obj = Array.ConvertAll(
                completeHash,
                val => JsonSerializer.Deserialize<Platform>(val.Value)
            ).ToList();

            return obj;
        }

        return null;
    }

    public async Task<Platform?> GetById(string id)
    {
        var db = _connectionMultiplexer.GetDatabase();

        // var platStr = await db.StringGetAsync(id);
        var platStr = await db.HashGetAsync(hashKey, id);

        if (!string.IsNullOrEmpty(platStr))
        {
            return JsonSerializer.Deserialize<Platform>(platStr);
        }

        return null;
    }
}