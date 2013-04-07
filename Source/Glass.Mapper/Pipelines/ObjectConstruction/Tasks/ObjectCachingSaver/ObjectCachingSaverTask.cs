using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingSaver
{
    /// <summary>
    /// Saves the requested object in the cache
    /// </summary>
    public class ObjectCachingSaverTask : ObjectCachingTask
    {
        public override void Execute(ObjectCachingArgs args)
        {
            args.Context.ObjectCacheConfiguration.ObjectCache.AddObject(args);
        }
    }
}
