using System;
using Glass.Mapper.Caching.Exceptions;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// Abstract base class that represents the key of a object in cache
    /// </summary>
    public abstract class AbstractCacheKey : IEquatable<AbstractCacheKey>
    {
        /// <summary>
        /// The default group identifier
        /// </summary>
        public const string DefaultGroupIdentifier = "DefaultGroupIdentifier";

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public string UniqueIdentifier { get; private set; }

        /// <summary>
        /// Gets the group identifier.
        /// </summary>
        /// <value>
        /// The group identifier.
        /// </value>
        public string GroupIdentifier { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCacheKey"/> class.
        /// </summary>
        protected AbstractCacheKey()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCacheKey" /> class.
        /// </summary>
        /// <param name="uniqueIdentifier">The unique identifier.</param>
        /// <param name="groupIdentifier">The group identifier.</param>
        /// <exception cref="Glass.Mapper.Caching.Exceptions.NullGroupIdentifierException"></exception>
        protected AbstractCacheKey(string uniqueIdentifier, string groupIdentifier)
        {
            CheckId(uniqueIdentifier);
            SetUniqueIdentifier(uniqueIdentifier);
            
            CheckGroupIdentifier(groupIdentifier);
            SetGroupIdentifier(groupIdentifier);

        }

        private void SetGroupIdentifier(string groupIdentifier)
        {
            GroupIdentifier = string.IsNullOrEmpty(groupIdentifier) ? DefaultGroupIdentifier : groupIdentifier;
        }

        private static void CheckGroupIdentifier(string groupIdentifier)
        {
            if (groupIdentifier == null)
                throw new NullGroupIdentifierException();
        }

        private void SetUniqueIdentifier(string uniqueIdentifier)
        {
            UniqueIdentifier = uniqueIdentifier;
        }

        private void CheckId(string uniqueIdentifier)
        {
            if (string.IsNullOrEmpty(uniqueIdentifier))
                if (uniqueIdentifier == null)
                    throw new NullUniqueIdentifierException();
                else
                    throw new EmptyUniqueIdentifierException();

            

        }

        public bool Equals(AbstractCacheKey other)
        {
            return UniqueIdentifier == other.UniqueIdentifier;
        }
    }
}
