using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching.ObjectCaching
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    public abstract class AbstractCacheKeyResolver<TIdType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract CacheKey<TIdType> GetKey(ObjectCachingArgs args);
    }
}
