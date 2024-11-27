using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HybridCacheTest;

public class LocalCache : IDistributedCache
{
    private readonly string _cacheFilePath;
    private readonly TimeSpan _defaultExpiration;

    public LocalCache(string cacheFilePath = "localcache.json", TimeSpan? defaultExpiration = null)
    {
        _cacheFilePath = cacheFilePath;
        _defaultExpiration = defaultExpiration ?? TimeSpan.FromMinutes(30);
    }

    private Dictionary<string, CacheItem> LoadCache()
    {
        if (File.Exists(_cacheFilePath))
        {
            var json = File.ReadAllText(_cacheFilePath);
            return JsonSerializer.Deserialize<Dictionary<string, CacheItem>>(json) ?? new Dictionary<string, CacheItem>();
        }
        return new Dictionary<string, CacheItem>();
    }

    private void SaveCache(Dictionary<string, CacheItem> cache)
    {
        var json = JsonSerializer.Serialize(cache, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_cacheFilePath, json);
    }

    public byte[] Get(string key)
    {
        var cache = LoadCache();
        if (cache.ContainsKey(key))
        {
            var cacheItem = cache[key];
            if (cacheItem.Expiration > DateTime.UtcNow)
            {
                return Convert.FromBase64String(cacheItem.Value);
            }
            else
            {
                cache.Remove(key); // Remove expired item
                SaveCache(cache);
            }
        }
        return null;
    }

    public Task<byte[]?> GetAsync(string key, CancellationToken token = new CancellationToken())
    {
        return Task.FromResult<byte[]?>(Get(key));  
    }

    public async Task<byte[]> GetAsync(string key)
    {
        return await Task.FromResult(Get(key));
    }

    public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
    {
        Remove(key);
        return Task.CompletedTask;
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        var cache = LoadCache();
        var cacheItem = new CacheItem
        {
            Value = Convert.ToBase64String(value),
            Expiration =   DateTime.UtcNow.Add(options.AbsoluteExpirationRelativeToNow??_defaultExpiration)
        };
        cache[key] = cacheItem;
        SaveCache(cache);
    }

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
        CancellationToken token = new CancellationToken())
    {
        Set(key, value, options);
        return Task.CompletedTask;
    }

    public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        Set(key, value, options);
        await Task.CompletedTask;
    }

    public void Refresh(string key)
    {
        var cache = LoadCache();
        if (cache.ContainsKey(key))
        {
            var cacheItem = cache[key];
            cacheItem.Expiration = DateTime.UtcNow.Add(_defaultExpiration);
            cache[key] = cacheItem;
            SaveCache(cache);
        }
    }

    public Task RefreshAsync(string key,
        CancellationToken token = new CancellationToken())
    {
        Refresh(key);
        return Task.CompletedTask;
    }

    public async Task RefreshAsync(string key)
    {
        Refresh(key);
        await Task.CompletedTask;
    }

    public void Remove(string key)
    {
        var cache = LoadCache();
        if (cache.ContainsKey(key))
        {
            cache.Remove(key);
            SaveCache(cache);
        }
    }

    public async Task RemoveAsync(string key)
    {
        Remove(key);
        await Task.CompletedTask;
    }

    // Cache item helper class
    private class CacheItem
    {
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}