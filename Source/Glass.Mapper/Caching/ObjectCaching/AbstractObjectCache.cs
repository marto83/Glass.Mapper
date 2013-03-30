using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Caching.ObjectCaching
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractObjectCache<TIdType> : IAbstractObjectCache
    {
        protected AbstractCacheKeyResolver<TIdType> CacheKeyResolver;

        protected abstract bool InternalContansObject(string objectKey);

        public bool ContansObject(Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            return InternalContansObject(CacheKeyResolver.GetKey(args).ToString());
        }


        public object GetObject(Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKeyResolver"></param>
        protected AbstractObjectCache(AbstractCacheKeyResolver<TIdType> cacheKeyResolver)
        {
            CacheKeyResolver = cacheKeyResolver;
        }
    }
}
