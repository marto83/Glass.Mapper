using System;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractObjectCache
    {
        /// <summary>
        /// The default base key
        /// </summary>
        public static string DefaultAbstractObjectCacheBaseKey = "GlassCache";

        /// <summary>
        /// Gets the base key.
        /// </summary>
        /// <value>
        /// The base key.
        /// </value>
        public string AbstractObjectCacheBaseKey { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractObjectCache"/> class.
        /// </summary>
        protected AbstractObjectCache()
            : this(DefaultAbstractObjectCacheBaseKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractObjectCache"/> class.
        /// </summary>
        /// <param name="baseKey">The base key.</param>
        protected AbstractObjectCache(string baseKey)
        {
            SetBaseKey(baseKey);
        }


        /// <summary>
        /// Internals the add.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="objectForCaching">The object for caching.</param>
        protected abstract void InternalAdd(string key, object objectForCaching);

        /// <summary>
        /// Checks to see if the key has already been added to the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected abstract bool CheckKey(string key);


        /// <summary>
        /// Adds the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="objectForCaching">The object for caching.</param>
        /// <exception cref="DuplicatedKeyException"></exception>
        public void Add(AbstractCacheKey cacheKey, object objectForCaching)
        {
            CheckObjectForCaching(objectForCaching);

            var key = GetCacheKey(cacheKey);

            if (CheckKey(key))
                throw new DuplicatedKeyException();

            InternalAdd(key, objectForCaching);
        }

        /// <summary>
        /// Adds to group.
        /// </summary>
        /// <param name="groupKey">The group key.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="objectForGrouping">The object for grouping.</param>
        /// <exception cref="System.NullReferenceException"></exception>
        public void AddToGroup(string groupKey, string cacheKey, object objectForGrouping)
        {
            if (string.IsNullOrEmpty(groupKey) || string.IsNullOrEmpty(cacheKey))
                throw new NullReferenceException();
        }

        private string GetCacheKey(AbstractCacheKey cacheKey)
        {
            return cacheKey.GetKey() + AbstractObjectCacheBaseKey;
        }

        private static void CheckObjectForCaching(object objectForCaching)
        {
            if (objectForCaching == null)
                throw new NullReferenceException();
        }

        private void SetBaseKey(string baseKey)
        {
            AbstractObjectCacheBaseKey = string.IsNullOrEmpty(baseKey) ? DefaultAbstractObjectCacheBaseKey : baseKey;
        }
    }
}