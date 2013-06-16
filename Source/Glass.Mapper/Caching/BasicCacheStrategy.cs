using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// Caching strategy that will only cache classes that 
    /// done have any constructor parameters
    /// </summary>
    public class BasicCacheStrategy : AbstractCacheStrategy
    {
        public override bool CanCache(Pipelines.ObjectConstruction.ObjectConstructionArgs args)
        {
            return base.CanCache(args)
                   && !args.AbstractTypeCreationContext.ConstructorParameters.Any();
        }
    }
}
