using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching
{
    public interface ICacheKeyFactory
    {
         ICacheKey GetKey(ObjectConstructionArgs args);
    }
}
