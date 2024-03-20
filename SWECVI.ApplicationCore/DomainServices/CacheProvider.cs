using Microsoft.Extensions.Caching.Memory;
using SWECVI.ApplicationCore.Interfaces;

namespace SWECVI.ApplicationCore.DomainServices
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        private static object _lock = new object();

        public CacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void PutToCache<T>(string key, T cacheModel, CacheItemPriority priority = CacheItemPriority.Low)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions() {
                Priority = priority,
                SlidingExpiration = new TimeSpan(48, 0, 0)
            };
            _memoryCache.Set(key, cacheModel, cacheEntryOptions);
        }

        public void RemoveKey(string key)
        {
            _memoryCache.Remove(key);
        }

        public T GetCache<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }
    }
}