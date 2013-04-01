using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.Configuration;
using Glass.Mapper.Caching.ObjectCaching;

namespace Glass.Mapper.Sc.Caching.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class SitecoreObjectCacheConfiguration : AbstractObjectCacheConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public SitecoreObjectCacheConfiguration()
            : base(Context.Default.DependencyResolver)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextName"></param>
        public SitecoreObjectCacheConfiguration(string contextName)
            : base(contextName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolver"></param>
        public SitecoreObjectCacheConfiguration(IDependencyResolver resolver)
            : base(resolver)
        {
        }
    }
}
