using System;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// An Exception that indicates that an object with the same key already exists in the cache
    /// </summary>
    public class DuplicatedKeyException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicatedKeyException"/> class.
        /// </summary>
        public DuplicatedKeyException ()
        {}
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicatedKeyException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DuplicatedKeyException (string message)
            : base(message)
        {
        }
    }
}
