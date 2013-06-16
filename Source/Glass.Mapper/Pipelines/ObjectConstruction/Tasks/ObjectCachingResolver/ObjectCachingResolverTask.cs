/*
   Copyright 2012 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/
//-CRE-

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingResolver
{
    /// <summary>
    /// Checks the Cache for requested object
    /// </summary>
    public class ObjectCachingResolverTask : ObjectCachingTask
    {
        private readonly AbstractObjectCache _cache;

        public ObjectCachingResolverTask(AbstractObjectCache cache, ICacheKeyFactory factory):base(factory)
        {
            _cache = cache;
        }

        public override void Execute(ObjectConstructionArgs args, ICacheKey cacheKey)
        {
            if (cacheKey == null || !_cache.ContainsObject(cacheKey)) return;

            args.Result = _cache.GetObject(cacheKey);

            args.AbortPipeline();
        }
    }
}
