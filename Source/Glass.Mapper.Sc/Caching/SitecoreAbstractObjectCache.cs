using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines.ObjectConstruction;
using Glass.Mapper.Sc.Configuration;
using Sitecore.Caching;
using Sitecore.Data;

namespace Glass.Mapper.Sc.Caching
{
    public class SitecoreAbstractObjectCache : AbstractObjectCache
    {
        private readonly Cache _objectCache;
        private readonly TimeSpan _cacheItemSlidingExpiration;

        public long CacheSize { get; private set; }

        public SitecoreAbstractObjectCache()
            : this(100)
        {
            
           
        }
        public SitecoreAbstractObjectCache(long cacheSize)
        {
            CacheSize = cacheSize;
            _objectCache = Cache.GetNamedInstance(CacheName, CacheSize);

            _cacheItemSlidingExpiration = System.Web.Caching.Cache.NoSlidingExpiration;
        }

        public override bool ContainsObject(ICacheKey cacheKey)
        {
            return _objectCache.ContainsKey(cacheKey.GetKey());
        }

        protected override void InternalAddObject(ICacheKey cacheKey, object objectForCaching)
        {
            _objectCache.Add(cacheKey.GetKey(), objectForCaching, 10, _cacheItemSlidingExpiration);
        }

        public override object GetObject(ICacheKey cacheKey)
        {
            return _objectCache.GetValue(cacheKey.GetKey());
        }
    }
}
