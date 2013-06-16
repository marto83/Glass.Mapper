using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.Exceptions;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching.ObjectCaching
{
    public interface IObjectCache 
    {
        bool ContainsObject(ICacheKey args);

        object GetObject(ICacheKey args);

        void AddObject(ICacheKey args, object objectToAdd);

    }
}
