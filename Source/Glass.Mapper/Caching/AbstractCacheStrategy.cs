using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// The base cache strategy, all other strategies 
    /// should inherit from this. Ignores objects 
    /// that inherit ICacheIgnore
    /// </summary>
    public class AbstractCacheStrategy
    {
        public virtual bool CanCache(ObjectConstructionArgs args)
        {
            //we don't want to cache lazy objects
            //we also don't want to cache object explicitly detailed for exclusion
            return !(args.Result is ICacheIgnore) && args.AbstractTypeCreationContext.IsLazy == false;
        }
    }
}
