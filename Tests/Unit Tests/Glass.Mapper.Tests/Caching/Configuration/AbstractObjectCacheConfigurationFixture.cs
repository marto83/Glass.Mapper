using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Glass.Mapper.Caching.Configuration;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching.Configuration
{
    [TestFixture]
    public class AbstractObjectCacheConfigurationFixture
    {
        private StubIDependencyResolver stubIDependencyResolver;

        [SetUp]
        public void SetUp()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Register(
                Component.For<AbstractObjectCacheConfiguration>()
                         .ImplementedBy<StubAbstractObjectCacheConfiguration>()
                         .LifestyleTransient());

            stubIDependencyResolver = new StubIDependencyResolver(container);
        }
    }

    public class StubAbstractObjectCacheConfiguration:AbstractObjectCacheConfiguration
    {
        
    }
    public class StubIDependencyResolver:IDependencyResolver
    {
        public IWindsorContainer Container { get; private set; }

        public StubIDependencyResolver(IWindsorContainer container)
        {
            Container = container;
        }

        public T Resolve<T>(IDictionary<string, object> args = null)
        {
            if (args == null)
                return Container.Resolve<T>();


            return Container.Resolve<T>((IDictionary)args);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }
    }
}
