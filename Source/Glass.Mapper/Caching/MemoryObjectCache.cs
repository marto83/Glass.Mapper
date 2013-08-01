using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching
{
    public class MemoryObjectCache : IObjectCache
    {
        public const string CacheName = "0E08C19C-39EF-4990-A8C9-BD167334BF84";

        private volatile MemoryCache _objectCache;

        public TimeSpan SlidingExpiration { get; set; }

        public MemoryObjectCache()
        {
            _objectCache = new MemoryCache(CacheName);
            SlidingExpiration = new TimeSpan(0, 2, 0, 0);
        }

        public bool ContainsObject(ICacheKey cacheKey)
        {
            return _objectCache.Contains(cacheKey.GetKey());
        }

        public void AddObject(ICacheKey cacheKey, object objectForCaching)
        {
            var policy = new CacheItemPolicy();
            policy.SlidingExpiration = SlidingExpiration;

            _objectCache.Set(cacheKey.GetKey(), objectForCaching, policy);
        }

        public void ClearCache()
        {
            var oldCache = _objectCache;
            _objectCache = new MemoryCache(CacheName);

            if (oldCache != null)
            {
                oldCache.Dispose();
            }
        }

        public object GetObject(ICacheKey cacheKey)
        {
            return _objectCache.Get(cacheKey.GetKey());
        }
    }
}
