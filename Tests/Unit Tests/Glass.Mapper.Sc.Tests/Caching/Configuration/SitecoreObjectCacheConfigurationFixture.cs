using System.Collections;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Glass.Mapper.Caching.Configuration;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Sc.Caching.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Sc.Tests.Caching.Configuration
{
    [TestFixture]
    public class SitecoreObjectCacheConfigurationFixture
    {
        private StubIDependencyResolver stubIDependencyResolver;

        [SetUp]
        public void SetUp()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Register(
                Component.For<AbstractObjectCacheConfiguration>()
                         .ImplementedBy<SitecoreObjectCacheConfiguration>()
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
            var objectCacheConfiguration = new SitecoreObjectCacheConfiguration();

            Assert.IsNotNull(objectCacheConfiguration.ObjectCache);
        }

        [Test]
        public void CanCreateAbstractObjectCacheConfigurationByName()
        {
            var objectCacheConfiguration = new SitecoreObjectCacheConfiguration("Default");

            Assert.IsNotNull(objectCacheConfiguration.ObjectCache);
        }

        [Test]
        public void CanCreateAbstractObjectCacheConfigurationByResolver()
        {
            var objectCacheConfiguration = new SitecoreObjectCacheConfiguration(stubIDependencyResolver);

            Assert.IsNotNull(objectCacheConfiguration.ObjectCache);
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
