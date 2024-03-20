using Microsoft.Extensions.Caching.Memory;

namespace SWECVI.ApplicationCore.Interfaces
{
    public interface ICacheProvider
    {
        void PutToCache<T>(string key, T cacheModel, CacheItemPriority priority = CacheItemPriority.Low);

        void RemoveKey(string key);

        T GetCache<T>(string key);
    }
}