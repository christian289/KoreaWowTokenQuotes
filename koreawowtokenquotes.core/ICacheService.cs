namespace koreawowtokenquotes.core;

public interface ICacheService
{
    string SetCachingValue(string key, string value);
    
    T SetCachingValue<T>(string key, T value);

    string SetCachingValue(string key, string value, TimeSpan expiration);

    T SetCachingValue<T>(string key, T value, TimeSpan expiration);
    
    string? GetCachedValue(string key);
}