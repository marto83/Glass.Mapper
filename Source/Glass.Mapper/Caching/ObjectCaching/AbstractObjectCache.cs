using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.Exceptions;

namespace Glass.Mapper.Caching.ObjectCaching
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractObjectCache<TIdType> : IAbstractObjectCache
    {
        protected AbstractCacheKeyResolver<TIdType> CacheKeyResolver;

        protected abstract bool InternalContansObject(string objectKey);
        protected abstract void InternalAddObject(string objectKey, object objectForCaching);
        protected abstract object InternalGetObject(string objectKey);

        public bool ContansObject(Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            return InternalContansObject(CacheKeyResolver.GetKey(args).ToString());
        }

        public object GetObject(Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            return InternalGetObject(CacheKeyResolver.GetKey(args).ToString());
        }

        public void AddObject(Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            var objectKey = CacheKeyResolver.GetKey(args).ToString();

            if (InternalContansObject(objectKey))
            {
                throw new DuplicatedKeyObjectCacheException("Key exists in object cache already");
            }

            InternalAddObject(objectKey, args.Result);
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
