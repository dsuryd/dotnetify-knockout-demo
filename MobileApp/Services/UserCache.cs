using System;
using Microsoft.Extensions.Caching.Memory;
using Domain.Service.Interfaces;

namespace Services
{
   public class UserCache : IUserCache
   {
      private static readonly TimeSpan DEFAULT_CACHE_EXPIRATION = new TimeSpan(0, 20, 0);

      private readonly IMemoryCache _cache;

      public TimeSpan CacheExpiration { get; set; } = DEFAULT_CACHE_EXPIRATION;

      public UserCache(IMemoryCache memoryCache)
      {
         _cache = memoryCache;
      }

      public T Get<T>(string iKey) => (T)_cache.Get(iKey);

      public void Set<T>(string iKey, T iValue) => _cache.Set(iKey, iValue, GetCacheEntryOptions());

      private MemoryCacheEntryOptions GetCacheEntryOptions() => new MemoryCacheEntryOptions()
         .SetSlidingExpiration(CacheExpiration)
         .RegisterPostEvictionCallback((key, value, reason, substate) => (value as IDisposable)?.Dispose());
   }
}
