using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Caching.Exceptions
{
    public class DuplicatedKeyObjectCacheException : ObjectCacheException
    {
        public DuplicatedKeyObjectCacheException(string message)
            : base(message)
        {
        }
    }
}
