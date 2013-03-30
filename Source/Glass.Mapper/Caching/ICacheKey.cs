using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Caching
{
    /// <summary>
    /// An interface for Cache keys
    /// </summary>
    public interface ICacheKey
    {
        /// <summary>
        /// Requests the Id of the content referenced by this key as an object
        /// </summary>
        /// <returns>The Id of the key</returns>
        object GetId();
    }
}
