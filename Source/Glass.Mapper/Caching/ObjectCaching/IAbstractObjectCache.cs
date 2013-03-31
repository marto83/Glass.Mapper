using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching.ObjectCaching
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAbstractObjectCache
    {
        /// <summary>
        /// Checks the Cache for the request object
        /// </summary>
        /// <param name="args">The arguments for the request</param>
        /// <returns>whether the object is in the cache</returns>
        bool ContansObject(ObjectCachingArgs args);

        /// <summary>
        /// Gets an object form Cache
        /// </summary>
        /// <param name="args">The arguments for the request</param>
        /// <returns>the object from Cache</returns>
        object GetObject(ObjectCachingArgs args);

        /// <summary>
        /// Adds and object to the Cache
        /// </summary>
        /// <param name="args">The arguments for the request</param>
        void AddObject(ObjectCachingArgs args);
    }
}
