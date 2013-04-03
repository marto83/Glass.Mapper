using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Glass.Mapper.Caching.Configuration;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Configuration;
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
        public void CanConfigureCacheViaProperty()
        {
            //create a context
            var context = Context.Create(_contextFixtureIDependencyResolver);

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

        [Test]
        public void Load_LoadContextWithTypeConfigs_CanGetTypeConfigsFromContext()
        {
            //Assign
            var loader1 = Substitute.For<IConfigurationLoader>();
            var config1 = Substitute.For<AbstractTypeConfiguration>();
            config1.Type = typeof(StubClass1);
            loader1.Load().Returns(new[] { config1 });

            var loader2 = Substitute.For<IConfigurationLoader>();
            var config2 = Substitute.For<AbstractTypeConfiguration>();
            config2.Type = typeof(StubClass2);
            loader2.Load().Returns(new[] { config2 });

            //Act
            var context = Context.Create(Substitute.For<IDependencyResolver>());
            context.Load(loader1, loader2);
            context.ConfigureCache(new ContextFixtureAbstractObjectCacheConfiguration());

            //Assert
            Assert.IsNotNull(Context.Default);
            Assert.AreEqual(Context.Contexts[Context.DefaultContextName], Context.Default);
            Assert.AreEqual(config1, Context.Default.TypeConfigurations[config1.Type]);
            Assert.AreEqual(config2, Context.Default.TypeConfigurations[config2.Type]);
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

    public class StubClass1
    {

    }

    public class StubClass2
    {

    }
}
