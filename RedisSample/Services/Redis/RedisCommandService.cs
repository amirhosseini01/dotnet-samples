using StackExchange.Redis;

namespace RedisSample.Services.Redis;

public class RedisCommandService
{
    private readonly IDatabase _db;
    public RedisCommandService(RedisConnectionFactory redisConnectionFactory)
    {
        _db = redisConnectionFactory.ConnectionMultiplexer.GetDatabase();
    }

    public async Task<bool> SetAsync(string key, string value)
    {
        return await _db.StringSetAsync(key, value);
    }

    public async Task<bool> SetAsync(byte[] key, string value)
    {
        return await _db.StringSetAsync(key, value);
    }

    public async Task<string?> GetAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }
}