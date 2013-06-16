using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingSaver
{
    /// <summary>
    /// Saves the requested object in the cache
    /// </summary>
    public class ObjectCachingSaverTask : ObjectCachingTask
    {
        public AbstractCacheStrategy CacheStrategy { get; set; }

        public ObjectCachingSaverTask(IObjectCache cache, ICacheKeyFactory factory)
            :base(cache, factory)
        {
            CacheStrategy = new BasicCacheStrategy();
        }

        protected override void Execute(ObjectConstructionArgs args, ICacheKey cacheKey)
        {
            if (CacheStrategy.CanCache(args))
            {
                if (args.Result != null && cacheKey != null)
                    ObjectCache.AddObject(cacheKey, args.Result);
            }
        }
    }
}
