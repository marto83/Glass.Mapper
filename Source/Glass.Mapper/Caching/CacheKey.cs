using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIdType">The type of the id.  Where the id is the primary identifier for content in a CMS </typeparam>
    public abstract class CacheKey<TIdType> : ICacheKey
    {
        protected CacheKey(TIdType id, TIdType revisionId, string database)
            : this()
        {
            Id = id;
            RevisionId = revisionId;
            Database = database;
        }

        protected CacheKey()
        {
        }

        /// <summary>
        /// The id of the content
        /// </summary>
        public TIdType Id { get; private set; }

        /// <summary>
        /// The version number of the content if any
        /// </summary>
        public TIdType RevisionId { get; private set; }

        /// <summary>
        /// The database that the content had come form
        /// </summary>
        public string Database { get; private set; }

        /// <summary>
        /// The type of the Id and RevisionId
        /// </summary>
        public object KeyType
        {
            get { return typeof (TIdType); }
        }

        public override string ToString()
        {
            return "{0},{1},{2},{3}".Formatted(Id, RevisionId, Database, KeyType);
        }

        /// <summary>
        /// Requests the Id of the content referenced by this key as an object
        /// </summary>
        /// <returns>The Id of the key</returns>
        public object GetId()
        {
            return Id;
        }

        /// <summary>
        /// Compares two keys
        /// </summary>
        /// <param name="objectToCompare">The key that we are comparing with</param>
        /// <returns>whether the keys match</returns>
        public override bool Equals(object objectToCompare)
        {
            var key = objectToCompare as CacheKey<TIdType>;

            if (ReferenceEquals(null, key)) 
                return false;

            if (ReferenceEquals(this, key)) 
                return true;
            
            return key.GetType() == GetType() && Equals(key);
        }

        protected bool Equals(CacheKey<TIdType> other)
        {
            return EqualityComparer<TIdType>.Default.Equals(Id, other.Id)
                && EqualityComparer<TIdType>.Default.Equals(RevisionId, other.RevisionId) 
                && string.Equals(Database, other.Database);
        }
    }
}
