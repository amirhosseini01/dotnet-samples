using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data;
public class PlatformRedisRepo : IPlatformRepo
{
    public PlatformRedisRepo(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public IConnectionMultiplexer _connectionMultiplexer { get; }

    public Task Create(Platform platform)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Platform> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Platform> GetById(string id)
    {
        throw new NotImplementedException();
    }
}