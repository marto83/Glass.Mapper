using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Sc.Caching
{
    public class SitecoreCacheKeyFactory : ICacheKeyFactory
    {
        public ICacheKey GetKey(ObjectConstructionArgs args)
        {
            var context = args.AbstractTypeCreationContext as SitecoreTypeCreationContext;
            return new SitecoreCacheKey(context.Item, context.RequestedType);
        }
    }
}
