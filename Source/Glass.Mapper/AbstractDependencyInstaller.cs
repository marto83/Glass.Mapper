using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Glass.Mapper.Configuration;

namespace Glass.Mapper
{
    public abstract class AbstractDependencyInstaller: IWindsorInstaller
    {
        public GlassConfiguration Config { get; private set; }
        
        protected AbstractDependencyInstaller(GlassConfiguration config)
        {
            Config = config;
        }

        public abstract void Install(IWindsorContainer container, IConfigurationStore store);
    }
}
