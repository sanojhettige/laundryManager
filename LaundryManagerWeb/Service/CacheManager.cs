using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace LaundryManagerWeb.Services
{
    public interface ICacheManager
    {
        void Add<TItem>(List<TItem> items, string key, TimeSpan cacheLifetime);
        void Add<TItem>(TItem item, string key, TimeSpan cacheLifetime);
        TItem Get<TItem>(string key) where TItem : class;
        void ClearCache();
    }


    public class MemoryCacheManager : ICacheManager
    {
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly IMemoryCache _memoryCache;


        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

        }

        public void Add<TItem>(List<TItem> items, string key, TimeSpan timespan)
        {
            var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(timespan);
            options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _memoryCache.Set(key, items, options);
        }

        public void Add<TItem>(TItem item, string key, TimeSpan timespan)
        {
            var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(timespan);
            options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _memoryCache.Set(key, item, options);
        }

        public TItem Get<TItem>(string key) where TItem : class
        {
            return _memoryCache.TryGetValue(key, out TItem value) ? value : null;
        }

        public void ClearCache()
        {

            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();

        }
    }
}
