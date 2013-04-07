using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks
{
    public abstract class ObjectCachingTask : IObjectConstructionTask, IPipelineTask<ObjectCachingArgs>
    {
        public abstract void Execute(ObjectCachingArgs args);

        public void Execute(ObjectConstructionArgs args)
        {
            if (args is ObjectCachingArgs)
            {
                Execute(args as ObjectCachingArgs);
            }
            else
            {
                throw new ObjectConstructionException(
                    "Expected args to be of type ObjectCachingArgs when using an object caching task");
            }
        }
    }
}
