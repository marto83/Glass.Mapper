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
        private readonly AbstractObjectCache _cache;

        public ObjectCachingSaverTask(AbstractObjectCache cache, ICacheKeyFactory factory):base(factory)
        {
            _cache = cache;
        }


        public override void Execute(ObjectConstructionArgs args, ICacheKey cacheKey)
        {
            if(args.Result != null && cacheKey != null)
                _cache.AddObject(cacheKey, args.Result);

        }
    }
}
