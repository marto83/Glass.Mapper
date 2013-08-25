using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Caching.Exceptions;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// An abstract class responsible for dealing with the common logic for caching
    /// </summary>
    public abstract class AbstractObjectCache
    {
        protected readonly List<AbstractCacheKey> CacheKeys;
        protected readonly Dictionary<string, List<AbstractCacheKey>> Groups;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractObjectCache"/> class.
        /// </summary>
        protected AbstractObjectCache()
        {
            CacheKeys= new List<AbstractCacheKey>();
            Groups = new Dictionary<string, List<AbstractCacheKey>>();
        }

        /// <summary>
        /// Adds the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="objectForCaching">The object for caching.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Add(AbstractCacheKey cacheKey, object objectForCaching)
        {
            if (cacheKey == null)
                throw new NullCacheKeyException();

            CacheKeys.Add(cacheKey);

            Groups.Add(cacheKey.GroupIdentifier, new List<AbstractCacheKey> {cacheKey});
        }

        /// <summary>
        /// Contains the cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public bool ContainCacheKey(AbstractCacheKey cacheKey)
        {
            return CacheKeys.Any(x => x.Equals(cacheKey));
        }

        /// <summary>
        /// Contains the group key.
        /// </summary>
        /// <param name="groupIdentifier">The group identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ContainGroupKey(string groupIdentifier)
        {
            return Groups.Any(x => x.Key.Equals(groupIdentifier));
        }

        /// <summary>
        /// Gets the grouped cache keys.
        /// </summary>
        /// <param name="groupIdentifier">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<AbstractCacheKey> GetGroupedCacheKeys(string groupIdentifier)
        {
            if (Groups.ContainsKey(groupIdentifier))
                return Groups[groupIdentifier];
            
            return new List<AbstractCacheKey>();
        }
    }
}