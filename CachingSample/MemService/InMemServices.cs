using Microsoft.Extensions.Caching.Memory;

namespace CachingSample.MemService;

public class InMemServices
{
    private readonly IMemoryCache _memoryCache;
    public InMemServices(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T? TryGetCacheValue<T>(CacheKeys cacheKey)
    {
        if (_memoryCache.TryGetValue(cacheKey, out T? cacheValue))
            return cacheValue;

        return default;
    }

    public void SetAbsoluteExpiration<T>(CacheKeys cacheKey, T cacheValue,
        TimeSpan expiration, long? cacheSize)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetAbsoluteExpiration(expiration);

        cacheEntryOptions.Size = cacheSize;

        _memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
    }

    public void SetSlidingExpiration<T>(CacheKeys cacheKey, T cacheValue,
        TimeSpan expiration, TimeSpan absoluteExpiration, long? cacheSize)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(expiration);

        cacheEntryOptions.AbsoluteExpirationRelativeToNow = absoluteExpiration;
        cacheEntryOptions.Size = cacheSize;

        _memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
    }

    public void Remove(CacheKeys cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }
}