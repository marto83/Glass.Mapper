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
        protected Dictionary<GroupCacheKey, List<AbstractCacheKey>> Groups { get; private set; }

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

            if (!IsKeyUsedInCache(cacheKey.UniqueIdentifier))
            {
                AddToCache(cacheKey.UniqueIdentifier, objectForCaching);
                _cacheKeyLock.EnterWriteLock();
                CacheKeys.Add(cacheKey);
                _cacheKeyLock.ExitWriteLock();

                AddCacheKeyToGroup(cacheKey);
            }
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
        public bool IsGroupKeyContained(string groupIdentifier)
        {
            _cacheKeyLock.EnterReadLock();
            var result = Groups.Any(x => x.Key.GroupIdentifier.Equals(groupIdentifier));
            _cacheKeyLock.ExitReadLock();
            return result;
        }

        /// <summary>
        /// Gets the grouped cache keys.
        /// </summary>
        /// <param name="groupIdentifier">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<AbstractCacheKey> GetGroupedCacheKeys(string groupIdentifier)
        {
            var group = Groups.Keys.SingleOrDefault(key => key.GroupIdentifier == groupIdentifier);
            if (group != null)
                return Groups[group];
            
            return new List<AbstractCacheKey>();
        }

        /// <summary>
        /// Adds the automatic cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="objectForCaching">The object for caching.</param>
        /// <returns>Returns true if implantation seceded to added object to cache</returns>
        protected abstract void AddToCache(string cacheKey, object objectForCaching);

        /// <summary>
        /// Checks the cache for key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        protected abstract bool IsKeyUsedInCache(string cacheKey);

        private static void CheckCacheKey(AbstractCacheKey cacheKey)
        {
            if (cacheKey == null)
                throw new NullCacheKeyException();
        }

        private void AddCacheKeyToGroup(AbstractCacheKey cacheKey)
        {
            _cacheKeyLock.EnterReadLock();
            var groupKey = Groups.SingleOrDefault(x => x.Key.GroupIdentifier.Equals(cacheKey.GroupIdentifier));
            _cacheKeyLock.ExitReadLock();

            var group = groupKey.Key;

            if (group == null)
            {
                group = new GroupCacheKey(cacheKey);

                _cacheKeyLock.EnterWriteLock();
                Groups.Add(group, new List<AbstractCacheKey>());
                _cacheKeyLock.ExitWriteLock();
            }

            group.GroupKeyLock.EnterWriteLock();
            Groups[group].Add(cacheKey);
            group.GroupKeyLock.ExitWriteLock();

        }

        private void InitialiseCache()
        {
            CacheKeys = new List<AbstractCacheKey>();
            Groups = new Dictionary<GroupCacheKey, List<AbstractCacheKey>>();
        }
    }
}