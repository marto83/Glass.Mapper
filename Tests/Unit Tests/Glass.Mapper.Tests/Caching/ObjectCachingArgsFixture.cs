using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines.ObjectConstruction;
using NSubstitute;
using NUnit.Framework;
using Glass.Mapper.Caching;

namespace Glass.Mapper.Tests.Caching
{
    [TestFixture]
    public class ObjectCachingArgsFixture
    {
        private Context _context;
        private AbstractTypeCreationContext _abstractTypeCreationContext;
        private Type _type;
        private IAbstractService _service;
        private AbstractTypeConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _type = typeof(StubClass);
            _service = Substitute.For<IAbstractService>();

            _context = Context.Create(Substitute.For<IDependencyResolver>());

            _abstractTypeCreationContext = Substitute.For<AbstractTypeCreationContext>();
            _abstractTypeCreationContext.RequestedType = typeof(StubClass);
            _abstractTypeCreationContext.IsLazy = true;

            _configuration = Substitute.For<AbstractTypeConfiguration>();
            _configuration.Type = _type;
            _configuration.ConstructorMethods = Utilities.CreateConstructorDelegates(_type);
        }

        [Test]
        public void ObjectCachingArgsIsInstanceOfObjectConstructionArgs()
        {
            //Assert
            Assert.IsInstanceOf<ObjectConstructionArgs>(new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service));
        }
        [Test]
        public void CanSetCacheKeyFromCrounstrocter()
        {
            //Assign
            var cacheKey = Substitute.For<CacheKey<int>>();
            //Act
            var args = new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service, cacheKey);
            //Assert
            Assert.AreSame(args.CacheKey, cacheKey);
        }
    }

    public class StubClass
    {

    }
}
