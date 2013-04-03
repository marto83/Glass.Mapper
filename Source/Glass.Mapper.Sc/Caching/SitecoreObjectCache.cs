using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Configuration;
using Glass.Mapper.Sc.Configuration;
using Sitecore.Caching;
using Sitecore.Data;

namespace Glass.Mapper.Sc.Caching
{
    public class SitecoreObjectCache : AbstractObjectCache<ID>
    {
        private readonly Cache _objectCache;
        private readonly TimeSpan _cacheItemSlidingExpiration;

        public SitecoreObjectCache(AbstractCacheKeyResolver<ID> cacheKeyResolver, SitecoreGlassConfiguration glassConfiguration)
            : base(cacheKeyResolver, glassConfiguration)
        {
            _objectCache = Cache.GetNamedInstance(GlassConfiguration.CacheName, glassConfiguration.CacheSize);

            _cacheItemSlidingExpiration = System.Web.Caching.Cache.NoSlidingExpiration;
        }


        protected override bool InternalContansObject(string objectKey)
        {
            return _objectCache.ContainsKey(objectKey);
        }

        protected override void InternalAddObject(string objectKey, object objectForCaching)
        {
            _objectCache.Add(objectKey, objectForCaching, 10, _cacheItemSlidingExpiration);
        }

        protected override object InternalGetObject(string objectKey)
        {
            return _objectCache.GetValue(objectKey);
        }
    }
}
