using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace f00die_finder_be.Common.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private static readonly ConcurrentDictionary<string, bool> _cacheKeys = new();

        public CacheService(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            var cacheValue = await _cache.GetStringAsync(key);
            if (cacheValue is null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(cacheValue);
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            var cacheValue = JsonConvert.SerializeObject(value);
            await _cache.SetStringAsync(key, cacheValue);

            _cacheKeys.TryAdd(key, true);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);

            _cacheKeys.TryRemove(key, out _);
        }

        public async Task RemoveByPrefixAsync(string keyPrefix)
        {
            var keysToRemove = _cacheKeys.Keys
                .Where(key => key.StartsWith(keyPrefix))
                .ToList();

            foreach (var key in keysToRemove)
            {
                await _cache.RemoveAsync(key);
                _cacheKeys.TryRemove(key, out _);
            }
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory) where T : class
        {
            var cacheValue = await GetAsync<T>(key);
            if (cacheValue is not null)
            {
                return cacheValue;
            }

            var value = await factory();
            await SetAsync(key, value);

            _cacheKeys.TryAdd(key, true);

            return value;
        }
    }
}