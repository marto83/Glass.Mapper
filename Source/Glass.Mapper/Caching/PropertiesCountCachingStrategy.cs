using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Caching
{
    public class PropertiesCountCachingStrategy : BasicCacheStrategy
    {
        public int MinProperties { get; set; }

        public PropertiesCountCachingStrategy()
        {
            MinProperties = 10;
        }

        public override bool CanCache(Pipelines.ObjectConstruction.ObjectConstructionArgs args)
        {
            return base.CanCache(args) &&
                   args.AbstractTypeCreationContext.RequestedType.GetProperties().Count() >= MinProperties;
        }
    }
}
