using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Caching
{
    public interface ICacheKey
    {
        string GetKey();
    }
}
