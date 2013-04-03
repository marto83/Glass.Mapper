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
        /// The name of the Cache
        /// </summary>
        public string CacheName
        {
            get { return _cacheName; }
            set { _cacheName = value; }
        }
    }
}
