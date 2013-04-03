using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Glass.Mapper.Configuration;

namespace Glass.Mapper.Caching.ObjectCaching
{
    public class MemoryObjectCache<TIdType> : AbstractObjectCache<TIdType>
    {
        private volatile MemoryCache _objectCache;

        public MemoryObjectCache(AbstractCacheKeyResolver<TIdType> cacheKeyResolver, GlassConfiguration glassConfiguration)
            : base(cacheKeyResolver, glassConfiguration)
        {
            _objectCache = new MemoryCache(GlassConfiguration.CacheName);
        }

        protected override bool InternalContansObject(string objectKey)
        {
            return _objectCache.Contains(objectKey);
        }

        protected override void InternalAddObject(string objectKey, object objectForCaching)
        {
            var policy = new CacheItemPolicy();
            policy.SlidingExpiration = new TimeSpan(0, 2, 0, 0);

            _objectCache.Set(objectKey, objectForCaching, policy);
        }

        protected override object InternalGetObject(string objectKey)
        {
            return _objectCache.Get(objectKey);
        }
    }
}
