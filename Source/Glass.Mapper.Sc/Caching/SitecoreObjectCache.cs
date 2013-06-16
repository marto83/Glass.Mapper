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
    public class SitecoreObjectCache : IObjectCache
    {
        private readonly Cache _objectCache;
        private readonly TimeSpan _cacheItemSlidingExpiration;

        public const string CacheName = "0E08C19C-39EF-4990-A8C9-BD167334BF84";

        public long CacheSize { get; private set; }

        public SitecoreObjectCache()
            : this(100)
        {
            
           
        }
        public SitecoreObjectCache(long cacheSize)
        {
            CacheSize = cacheSize;
            _objectCache = Cache.GetNamedInstance(CacheName, CacheSize);

            _cacheItemSlidingExpiration = System.Web.Caching.Cache.NoSlidingExpiration;
        }

        public  bool ContainsObject(ICacheKey cacheKey)
        {
            return _objectCache.ContainsKey(cacheKey.GetKey());
        }

        public  void AddObject(ICacheKey cacheKey, object objectForCaching)
        {
            _objectCache.Add(cacheKey.GetKey(), objectForCaching, 10, _cacheItemSlidingExpiration);
        }

        public  object GetObject(ICacheKey cacheKey)
        {
            return _objectCache.GetValue(cacheKey.GetKey());
        }
    }
}
