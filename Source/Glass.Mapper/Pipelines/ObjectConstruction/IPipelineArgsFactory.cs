using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Configuration;

namespace Glass.Mapper.Pipelines.ObjectConstruction
{
    public interface IPipelineArgsFactory
    {
        AbstractPipelineArgs CreatePipelineArgs(Context context,
            AbstractTypeCreationContext abstractTypeCreationContext,
            AbstractTypeConfiguration configuration,
            IAbstractService service);
    }
}
