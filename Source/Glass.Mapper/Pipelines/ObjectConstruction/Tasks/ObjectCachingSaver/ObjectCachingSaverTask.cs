using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.ObjectCaching;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingSaver
{
    /// <summary>
    /// Saves the requested object in the cache
    /// </summary>
    public class ObjectCachingSaverTask : ObjectCachingTask
    {
        private readonly IObjectCache _cache;

        public ObjectCachingSaverTask(IObjectCache cache )
        {
            _cache = cache;
        }

        public override void Execute(ObjectCachingArgs args)
        {
            _cache.AddObject(args);
        }
    }
}
