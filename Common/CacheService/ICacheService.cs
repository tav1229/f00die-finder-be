﻿namespace f00die_finder_be.Common.CacheService
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, T value) where T : class;
        Task RemoveAsync(string key);
        Task RemoveByPrefixAsync(string keyPrefix);
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory) where T : class;
    }
}
