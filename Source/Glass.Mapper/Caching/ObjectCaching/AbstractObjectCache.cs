using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.Exceptions;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace Glass.Mapper.Caching.ObjectCaching
{
    public abstract class AbstractObjectCache 
    {
        public const string CacheName = "0E08C19C-39EF-4990-A8C9-BD167334BF84";

        protected abstract void InternalAddObject(ICacheKey args, object objectForCaching);

        public abstract bool ContainsObject(ICacheKey args);

        public abstract object GetObject(ICacheKey args);

        public void AddObject(ICacheKey args, object objectToAdd)
        {
            if (ContainsObject(args))
            {
                throw new DuplicatedKeyObjectCacheException("Key exists in object cache already");
            }
            //TODO: need to add strategy object

            InternalAddObject(args, objectToAdd);
        }
    }
}
