using StackExchange.Redis;

namespace RedisSample.Services.Redis;

public class RedisBusService
{
    private readonly ISubscriber _sub;
    public RedisBusService(RedisConnectionFactory redisConnectionFactory)
    {
        _sub = redisConnectionFactory.ConnectionMultiplexer.GetSubscriber();
    }

    public async Task PublishToChannel(string channel, string message)
    {
        try
        {
            await _sub.PublishAsync(channel, message);
        }
        catch (System.Exception ex)
        {
            string errorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            throw;
        }
    }
}