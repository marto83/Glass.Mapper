using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Glass.Mapper.Sc.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class SitecoreCacheKey : ICacheKey
    {
        public const string RevisionField = "__Revision";
        /// <summary>
        /// A cache key for the Sitecore CMS
        /// </summary>
        public SitecoreCacheKey(Item item, Type objectType)
        {
            Id = item.ID;
            RevisionId = item[RevisionField];
            Database = item.Database.Name;
            ObjectType = objectType;
        }


        public Type ObjectType { get; private set; }

        /// <summary>
        /// The id of the content
        /// </summary>
        public ID Id { get; private set; }

        /// <summary>
        /// The version number of the content if any
        /// </summary>
        public string RevisionId { get; private set; }

        /// <summary>
        /// The database that the content had come form
        /// </summary>
        public string Database { get; private set; }

        public string GetKey()
        {
            return "{0},{1},{2},{3}".Formatted(Id, RevisionId, Database, ObjectType);
        }
    }
}
