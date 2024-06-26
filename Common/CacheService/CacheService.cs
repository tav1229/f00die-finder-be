﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace f00die_finder_be.Common.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ConcurrentDictionary<string, bool> _cacheKeys;
        private readonly JsonSerializerSettings _jsonSettings;
        private readonly IConfiguration _configuration;
        private readonly int _cacheExpiryTimeInMinutes;
        public CacheService(IDistributedCache cache, IOptions<MvcNewtonsoftJsonOptions> jsonOptions, IConfiguration configuration)
        {
            _cache = cache;
            _cacheKeys = new ConcurrentDictionary<string, bool>();
            _jsonSettings = jsonOptions.Value.SerializerSettings;
            _configuration = configuration;
            _cacheExpiryTimeInMinutes = _configuration.GetValue<int>("Cache:ExpiryTimeInMinutes");
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            var cacheValue = await _cache.GetStringAsync(key);
            if (cacheValue is null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(cacheValue, _jsonSettings);
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            var cacheValue = JsonConvert.SerializeObject(value, _jsonSettings);
            await _cache.SetStringAsync(key, cacheValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpiryTimeInMinutes)
            });
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