using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Glass.Mapper.Caching.Configuration;
using Glass.Mapper.Caching.ObjectCaching;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching.Configuration
{
    [TestFixture]
    public class ContextFixture
    {
        private ContextFixtureIDependencyResolver _contextFixtureIDependencyResolver;

        [SetUp]
        public void SetUp()
        {
            _contextFixtureIDependencyResolver = new ContextFixtureIDependencyResolver();
        }

        [Test]
        public void CanConfigureCache()
        {           
            //create a context
            var context = Context.Create(_contextFixtureIDependencyResolver);
            context.ConfigureCache();

            Assert.IsNotNull(context.ObjectCacheConfiguration);
        }

        [Test]
        public void CanConfigureCacheManuley()
        {
            //create a context
            var context = Context.Create(_contextFixtureIDependencyResolver);
            context.ConfigureCache(new ContextFixtureAbstractObjectCacheConfiguration());

            Assert.IsNotNull(context.ObjectCacheConfiguration);
        }
    }
    public class ContextFixtureIAbstractObjectCache:IAbstractObjectCache
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

    public class ContextFixtureAbstractObjectCacheConfiguration:AbstractObjectCacheConfiguration
    {
        
    }

    public class ContextFixtureIDependencyResolver : IDependencyResolver
    {
        private IWindsorContainer container;

        public ContextFixtureIDependencyResolver()
        {
            container = new WindsorContainer();
            container.Register(
                Component.For<IAbstractObjectCache>().ImplementedBy<ContextFixtureIAbstractObjectCache>().LifestyleTransient(),
                Component.For<AbstractObjectCacheConfiguration>().ImplementedBy<ContextFixtureAbstractObjectCacheConfiguration>().LifestyleTransient());
        }

        /// <summary>
        /// Resolves the specified args.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">The args.</param>
        /// <returns>``0.</returns>
        public T Resolve<T>(IDictionary<string, object> args = null)
        {
            if (args == null)
                return container.Resolve<T>();


            return container.Resolve<T>((IDictionary)args);
        }

        /// <summary>
        /// Resolves all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable{``0}.</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }
    }
}
