using System.Threading;
using Glass.Mapper.Caching.Exceptions;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// Class that represents a group in the cache
    /// </summary>
    public class GroupCacheKey
    {
        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>
        /// The group identifier.
        /// </value>
        public string GroupIdentifier{ get; private set; }

        /// <summary>
        /// The group key lock
        /// </summary>
        public readonly ReaderWriterLockSlim GroupKeyLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupCacheKey"/> class.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <exception cref="NullCacheKeyException"></exception>
        public GroupCacheKey(AbstractCacheKey cacheKey)
        {
            if(cacheKey == null)
                throw  new NullCacheKeyException();

            GroupIdentifier = cacheKey.GroupIdentifier;
        }
    }
}
