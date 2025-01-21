namespace koreawowtokenquotes.server.Services;

using koreawowtokenquotes.core;

public class CacheService(IMemoryCache cache) : ICacheService
{
    private IMemoryCache _cache => cache;

    public string SetCachingValue(string key, string value)
    {
        _cache.Set(key, value);

        return value;
    }

    public T SetCachingValue<T>(string key, T value)
    {
        _cache.Set(key, value);

        return value;
    }

    public string SetCachingValue(string key, string value, TimeSpan expiration)
    {
        _cache.Set(key, value, expiration);

        return value;
    }

    public T SetCachingValue<T>(string key, T value, TimeSpan expiration)
    {
        _cache.Set(key, value, expiration);

        return value;
    }

    public string? GetCachedValue(string key)
    {
        if (!_cache.TryGetValue(key, out string? cachedValue))
            cachedValue = string.Empty;

        return cachedValue;
    }

    public T? GetCachedValue<T>(string key)
    {
        if (!_cache.TryGetValue(key, out T? cachedValue))
            cachedValue = default;

        return cachedValue;
    }
}