using RedisSample.Properties;
using StackExchange.Redis;

namespace RedisSample.Services.Redis;

public class RedisConnectionFactory
{
    private readonly IConfiguration _configuration;

    public ConnectionMultiplexer ConnectionMultiplexer { get; }

    public RedisConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        ConnectionMultiplexer = ConnectionMultiplexer.Connect(
            _configuration.Get<AppSettings>()!.RedisConnectionString);

        SubscribeToChannel();
    }

    public void SubscribeToChannel()
    {
        var sub = ConnectionMultiplexer.GetSubscriber();
        try
        {
            sub.Subscribe("myChannel").OnMessage(async channelMessage =>
                Console.WriteLine((string)channelMessage.Message));
        }
        catch (System.Exception ex)
        {
            string errorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            throw;
        }
    }
}