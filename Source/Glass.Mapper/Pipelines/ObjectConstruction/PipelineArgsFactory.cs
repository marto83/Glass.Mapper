using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Configuration;

namespace Glass.Mapper.Pipelines.ObjectConstruction
{
    public class PipelineArgsFactory : IPipelineArgsFactory
    {

        public AbstractPipelineArgs CreatePipelineArgs(Context context,
            AbstractTypeCreationContext abstractTypeCreationContext,
            AbstractTypeConfiguration configuration,
            IAbstractService service)
        {
            return new ObjectConstructionArgs(context, abstractTypeCreationContext, configuration, service);
        }
    }
}
