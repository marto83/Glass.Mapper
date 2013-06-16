using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.ObjectCaching;
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
                         .LifestyleTransient(),
                Component.For<IAbstractObjectCache>()
                         .ImplementedBy<StubAbstractObjectCache>()
                         .LifestyleTransient()

                );

            stubIDependencyResolver = new StubIDependencyResolver(container);

            Context.Create(stubIDependencyResolver);
        }

        [Test]
        public void CanCreateAbstractObjectCacheConfiguration()
        {
            var objectCacheConfiguration = new StubAbstractObjectCacheConfiguration();

            Assert.IsNotNull(objectCacheConfiguration.ObjectCache);
        }

        [Test]
        public void CanCreateAbstractObjectCacheConfigurationByName()
        {
            var objectCacheConfiguration = new StubAbstractObjectCacheConfiguration("Default");

            Assert.IsNotNull(objectCacheConfiguration.ObjectCache);
        }

        [Test]
        public void CanCreateAbstractObjectCacheConfigurationByResolver()
        {
            var objectCacheConfiguration = new StubAbstractObjectCacheConfiguration(stubIDependencyResolver);

            Assert.IsNotNull(objectCacheConfiguration.ObjectCache);
        }
    }

    public class StubAbstractObjectCacheConfiguration : AbstractObjectCacheConfiguration
    {
        public StubAbstractObjectCacheConfiguration()
            : base()
        {
        }

        public StubAbstractObjectCacheConfiguration(string contextName)
            : base(contextName)
        {
        }

        public StubAbstractObjectCacheConfiguration(IDependencyResolver resolver)
            : base(resolver)
        {
        }
    }

    public class StubAbstractObjectCache : IAbstractObjectCache
    {

        public bool ContansObject(Mapper.Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            throw new NotImplementedException();
        }

        public object GetObject(Mapper.Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            throw new NotImplementedException();
        }

        public void AddObject(Mapper.Pipelines.ObjectConstruction.ObjectCachingArgs args)
        {
            throw new NotImplementedException();
        }
    }

    public class StubIDependencyResolver : IDependencyResolver
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


            return Container.Resolve<T>((IDictionary) args);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }
    }
}
