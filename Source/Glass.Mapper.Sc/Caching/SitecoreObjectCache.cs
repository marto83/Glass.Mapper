using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Glass.Mapper.Caching.ObjectCaching;
using Sitecore.Caching;
using Sitecore.Data;

namespace Glass.Mapper.Sc.Caching
{
    public class SitecoreObjectCache : AbstractObjectCache<ID>
    {
        private readonly Cache _objectCache;
        private readonly TimeSpan _cacheItemSlidingExpiration;

        public SitecoreObjectCache(AbstractCacheKeyResolver<ID> cacheKeyResolver)
            : base(cacheKeyResolver)
        {
            _objectCache = Cache.GetNamedInstance("Glass.Mapper.Sc.Caching", GetCacheSize());

            _cacheItemSlidingExpiration = System.Web.Caching.Cache.NoSlidingExpiration;
        }

        private static long GetCacheSize()
        {
            return Sitecore.StringUtil.ParseSizeString(
                Sitecore.Configuration.Settings.GetSetting(
                    "Glass.Mapper.Sc.Caching.CacheSize", "100MB"
                    )
                );
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
