using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Configuration;

namespace Glass.Mapper.Sc.Configuration
{
    public class SitecoreGlassConfiguration : GlassConfiguration
    {
        private long _cacheSize = 100;

        /// <summary>
        /// The name of the Cache
        /// </summary>
        public long CacheSize
        {
            get { return _cacheSize; }
            set { _cacheSize = value; }
        }
    }
}
