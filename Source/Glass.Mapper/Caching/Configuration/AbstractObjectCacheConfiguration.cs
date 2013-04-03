using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.ObjectCaching;

namespace Glass.Mapper.Caching.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractObjectCacheConfiguration
    {
        /// <summary>
        /// The Object Cache for Glass
        /// </summary>
        public IAbstractObjectCache ObjectCache { get; set; }

        protected AbstractObjectCacheConfiguration()
            : this(Context.Default.DependencyResolver)
        {

        }

        protected AbstractObjectCacheConfiguration(string contextName)
            : this(Context.Contexts[contextName].DependencyResolver)
        {
        }

        protected AbstractObjectCacheConfiguration(IDependencyResolver resolver)
        {
            ObjectCache = resolver.Resolve<IAbstractObjectCache>();
        }
    }
}
