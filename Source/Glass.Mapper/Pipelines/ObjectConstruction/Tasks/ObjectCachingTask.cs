using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks
{
    public abstract class ObjectCachingTask : IObjectConstructionTask
    {
        private readonly ICacheKeyFactory _factory;
        private IObjectCache _objectCache = new MemoryObjectCache();

        public IObjectCache ObjectCache
        {
            get { return _objectCache; }
            set { _objectCache = value; }
        }

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

        protected abstract void Execute(ObjectConstructionArgs args, ICacheKey cacheKey);

    }
}
