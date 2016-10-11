using System;
using System.Runtime.Caching;
using Service.Interfaces;

namespace Services
{
   public class UserCache : IUserCache
   {
      private static readonly TimeSpan DEFAULT_CACHE_EXPIRATION = new TimeSpan(0, 20, 0);

      private readonly MemoryCache _cache = new MemoryCache(nameof(UserCache));
      private readonly TimeSpan _cacheExpiration;


      public T Get<T>(string iKey)
      {
         return (T)_cache.Get(iKey);
      }

      public void Set<T>(string iKey, T iValue)
      {
         _cache.Set(iKey, iValue, GetCacheItemPolicy());
      }

      public UserCache() : this(DEFAULT_CACHE_EXPIRATION)
      { }

      public UserCache(TimeSpan cacheExpiration)
      {
         _cacheExpiration = cacheExpiration;
      }

      private CacheItemPolicy GetCacheItemPolicy()
      {
         return new CacheItemPolicy
         {
            SlidingExpiration = _cacheExpiration,
            RemovedCallback = i => (i.CacheItem.Value as IDisposable)?.Dispose()
         };
      }
   }
}
