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
        [Test]
        public void CanConfigureCache()
        {
            //create the resolver
            var resolver = new ContextFixtureIDependencyResolver();
            
            //create a context
            var context = Context.Create(resolver);
            context.ConfigureCache();

            Assert.IsNotNull(context.ObjectCacheConfiguration);
        }
    }

    public class ContextFixtureIDependencyResolver : IDependencyResolver
    {
        private IWindsorContainer container;

        public ContextFixtureIDependencyResolver()
        {
            container = new WindsorContainer();
            container.Register(
                Component.For<IAbstractObjectCache>().ImplementedBy<IAbstractObjectCache>().LifestyleTransient(),
                Component.For<AbstractObjectCacheConfiguration>().ImplementedBy<AbstractObjectCacheConfiguration>().LifestyleTransient());
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
