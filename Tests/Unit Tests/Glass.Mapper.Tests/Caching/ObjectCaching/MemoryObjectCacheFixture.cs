using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.Exceptions;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines.ObjectConstruction;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching.ObjectCaching
{
    [TestFixture]
    public class MemoryObjectCacheFixture
    {
        private AbstractCacheKeyResolver<int> _cacheKeyResolver;
        private const string Database = "SomeDatabase";
        private GlassConfiguration _glassConfiguration;
        [SetUp]
        public void SetUp()
        {
            _cacheKeyResolver = Substitute.For<AbstractCacheKeyResolver<int>>();
            _glassConfiguration = new GlassConfiguration();
        }

        [Test]
        public void AbstractObjectCacheContainObject()
        {
            var id =  1;
            var revisionId = 1;
            var args = Substitute.For<ObjectCachingArgs>();
            var stubClass = new StubClass();
            var key = Substitute.For<CacheKey<int>>(id, revisionId, Database);

            args.Result = stubClass;
            args.CacheKey = key;

            _cacheKeyResolver.GetKey(args).Returns(key);


            //Assign
            var abstractObjectCache = new MemoryObjectCache(_cacheKeyResolver, _glassConfiguration);

            //Act
            abstractObjectCache.AddObject(args);

            //Assert
            Assert.IsTrue(abstractObjectCache.ContansObject(args));
        }

        [Test]
        public void AbstractObjectCacheAddObject()
        {
            //Assign

            var id = 2;
            var revisionId = 2;
            var args = Substitute.For<ObjectCachingArgs>();
            var stubClass = new StubClass();
            var key = Substitute.For<CacheKey<int>>(id, revisionId, Database);

            args.Result = stubClass;
            args.CacheKey = key;

            _cacheKeyResolver.GetKey(args).Returns(key);

            var abstractObjectCache = new MemoryObjectCache(_cacheKeyResolver, _glassConfiguration);
            abstractObjectCache.AddObject(args);
            _cacheKeyResolver.GetKey(args).Returns(key);
            args.CacheKey = key;

            //Act
            abstractObjectCache.GetObject(args).Returns(key);

            //Assert
            Assert.AreSame(stubClass, abstractObjectCache.GetObject(args));
        }

        [Test]
        [ExpectedException(typeof(DuplicatedKeyObjectCacheException))]
        public void ExpectWhenTryToAddObjectAgain()
        {
            //Assign
            var abstractObjectCache = new MemoryObjectCache(_cacheKeyResolver, _glassConfiguration);

            var id = 3;
            var revisionId = 3;
            var args = Substitute.For<ObjectCachingArgs>();
            var stubClass = new StubClass();
            var key = Substitute.For<CacheKey<int>>(id, revisionId, Database);

            _cacheKeyResolver.GetKey(args).Returns(key);

            args.Result = stubClass;
            args.CacheKey = key;

            //Act
            abstractObjectCache.AddObject(args);
            abstractObjectCache.AddObject(args);
        }
    }
}
