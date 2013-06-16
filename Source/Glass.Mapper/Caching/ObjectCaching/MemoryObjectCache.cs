using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching.ObjectCaching
{
    public class MemoryObjectCache : AbstractObjectCache
    {

        private volatile MemoryCache _objectCache;

        public TimeSpan SlidingExpiration { get; set; }

        public MemoryObjectCache()
        {
            _objectCache = new MemoryCache(CacheName);
            SlidingExpiration = new TimeSpan(0, 2, 0, 0);
        }

        public override bool ContainsObject(ICacheKey cacheKey)
        {
            return _objectCache.Contains(cacheKey.GetKey());
        }

        protected override void InternalAddObject(ICacheKey cacheKey, object objectForCaching)
        {
            var policy = new CacheItemPolicy();
            policy.SlidingExpiration = SlidingExpiration;

            _objectCache.Set(cacheKey.GetKey(), objectForCaching, policy);
        }

        public override object GetObject(ICacheKey cacheKey)
        {
            return _objectCache.Get(cacheKey.GetKey());
        }
    }
}
