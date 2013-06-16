using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks
{
    public abstract class ObjectCachingTask : IObjectConstructionTask
    {
        private readonly ICacheKeyFactory _factory;

        public ObjectCachingTask(ICacheKeyFactory factory)
        {
            _factory = factory;
        }

        public void Execute(ObjectConstructionArgs args)
        {
            var key = _factory.GetKey(args);

            if(key != null)
                Execute(args, key);
        }

        public abstract void Execute(ObjectConstructionArgs args, ICacheKey cacheKey);

    }
}
