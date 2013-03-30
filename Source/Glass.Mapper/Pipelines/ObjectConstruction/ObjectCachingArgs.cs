using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Configuration;

namespace Glass.Mapper.Pipelines.ObjectConstruction
{
    /// <summary>
    /// Class ObjectCachingArgs that gives extra properties to enable caching
    /// </summary>
    public class ObjectCachingArgs : ObjectConstructionArgs
    {
        /// <summary>
        /// The key for this request so the Cache can be checked
        /// </summary>
        public ICacheKey CacheKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectCachingArgs"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="abstractTypeCreationContext">The abstract type creation context.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="service">The service.</param>
        public ObjectCachingArgs(
            Context context, 
            AbstractTypeCreationContext abstractTypeCreationContext, 
            AbstractTypeConfiguration configuration, 
            IAbstractService service) : base(context, abstractTypeCreationContext, configuration, service)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectCachingArgs"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="abstractTypeCreationContext">The abstract type creation context.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="service">The service.</param>
        /// <param name="cacheKey">The cacheKey for the request</param>
        public ObjectCachingArgs(
            Context context,
            AbstractTypeCreationContext abstractTypeCreationContext,
            AbstractTypeConfiguration configuration,
            IAbstractService service,
            ICacheKey cacheKey)
            : base(context, abstractTypeCreationContext, configuration, service)
        {
            CacheKey = cacheKey;
            CacheKey = cacheKey;
        }

        public ObjectCachingArgs() : base()
        {
        }
    }
}
