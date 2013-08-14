using System;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// Abstract base class that represents the key of a object in cache
    /// </summary>
    public abstract class AbstractCacheKey
    {

        /// <summary>
        /// Gets the base key.
        /// </summary>
        /// <value>
        /// The base key.
        /// </value>
        public string BaseKey { get; private set; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            return InternalGetKey() + BaseKey;
        }

        /// <summary>
        /// Internals the get key.
        /// </summary>
        /// <returns>The key</returns>
        public abstract string InternalGetKey();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCacheKey"/> class.
        /// </summary>
        protected AbstractCacheKey()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCacheKey"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <exception cref="System.NullReferenceException"></exception>
        protected AbstractCacheKey(Type objectType)
        {
            if(objectType == null)
                throw  new NullReferenceException();

            BaseKey = objectType.FullName;
        }
    }
}
