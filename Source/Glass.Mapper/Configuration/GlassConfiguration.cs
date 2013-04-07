using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Configuration
{
    /// <summary>
    /// Holds configuration for Glass
    /// </summary>  
    public class GlassConfiguration
    {

        private string _cacheName = "Glass.Mapper.Caching.ObjectCaching";

        /// <summary>
        /// The name of the Cache. Default is "Glass.Mapper.Caching.ObjectCaching"
        /// </summary>
        public string CacheName
        {
            get { return _cacheName; }
            set { _cacheName = value; }
        }


        private bool _useWindsorContructor = false;

        /// <summary>
        /// Indicates that classes should be build using the Windsor dependency resolver. Default is False 
        /// </summary>
        public bool UseWindsorContructor
        {
            get { return _useWindsorContructor; }
            set { _useWindsorContructor = value; }
        }

    }
}
