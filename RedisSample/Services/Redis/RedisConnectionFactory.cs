using RedisSample.Properties;
using StackExchange.Redis;

namespace RedisSample.Services.Redis;

public class RedisConnectionFactory
{
    private readonly IConfiguration _configuration;

    public ConnectionMultiplexer ConnectionMultiplexer { get;}

    public RedisConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        ConnectionMultiplexer =  ConnectionMultiplexer.Connect(
            _configuration.Get<AppSettings>()!.RedisConnectionString);
    }
}