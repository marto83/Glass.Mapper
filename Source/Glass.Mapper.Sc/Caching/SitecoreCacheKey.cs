using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Sitecore.Data;

namespace Glass.Mapper.Sc.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class SitecoreCacheKey : CacheKey<ID>
    {
        /// <summary>
        /// A cache key for the Sitecore CMS
        /// </summary>
        /// <param name="id">The ID of the Sitecore Node</param>
        /// <param name="revisionId">The revision of the Sitecore Node</param>
        /// <param name="database">The Database the node is from</param>
        public SitecoreCacheKey(ID id, ID revisionId, string database):base(id, revisionId, database)
        {
            
        }
    }
}
