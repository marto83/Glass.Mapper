using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Glass.Mapper.Caching.Exceptions;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// An abstract class responsible for dealing with the common logic for caching
    /// </summary>
    public abstract class AbstractObjectCache
    {
        private readonly ReaderWriterLockSlim _cacheKeyLock = new ReaderWriterLockSlim();
        protected List<AbstractCacheKey> CacheKeys { get; private set; }
        protected Dictionary<string, List<AbstractCacheKey>> Groups { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractObjectCache"/> class.
        /// </summary>
        protected AbstractObjectCache()
        {
            InitialiseCache();
        }

        /// <summary>
        /// Adds the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="objectForCaching">The object for caching.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Add(AbstractCacheKey cacheKey, object objectForCaching)
        {
            CheckCacheKey(cacheKey);
            
            _cacheKeyLock.EnterWriteLock();
            CacheKeys.Add(cacheKey);
            _cacheKeyLock.ExitWriteLock();
            
            AddCacheKeyToGroup(cacheKey);
        }

        /// <summary>
        /// Contains the cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public bool ContainCacheKey(AbstractCacheKey cacheKey)
        {
            _cacheKeyLock.EnterReadLock();
            var result = CacheKeys.Any(x => x.Equals(cacheKey));
            _cacheKeyLock.ExitReadLock();

            return result;
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

        private static void CheckCacheKey(AbstractCacheKey cacheKey)
        {
            if (cacheKey == null)
                throw new NullCacheKeyException();
        }

        private void AddCacheKeyToGroup(AbstractCacheKey cacheKey)
        {
            if (!ContainGroupKey(cacheKey.GroupIdentifier))
                Groups.Add(cacheKey.GroupIdentifier, new List<AbstractCacheKey> {cacheKey});
            else
                Groups[cacheKey.GroupIdentifier].Add(cacheKey);
        }

        private void InitialiseCache()
        {
            CacheKeys = new List<AbstractCacheKey>();
            Groups = new Dictionary<string, List<AbstractCacheKey>>();
        }
    }
}