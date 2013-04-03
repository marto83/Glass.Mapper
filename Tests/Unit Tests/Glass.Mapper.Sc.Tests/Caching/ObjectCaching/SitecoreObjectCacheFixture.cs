using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Pipelines.ObjectConstruction;
using Glass.Mapper.Sc.Caching;
using Glass.Mapper.Sc.Configuration;
using NSubstitute;
using NUnit.Framework;
using Sitecore.Data;
using Glass.Mapper.Caching.Exceptions;

namespace Glass.Mapper.Sc.Tests.Caching.ObjectCaching
{
    [TestFixture]
    public class SitecoreObjectCacheFixture
    {
        private AbstractCacheKeyResolver<ID> _cacheKeyResolver;
        private const string Database = "SomeDatabase";
        
        [SetUp]
        public void SetUp()
        {
            _cacheKeyResolver = Substitute.For<AbstractCacheKeyResolver<ID>>();
        }

        [Test]
        public void AbstractObjectCacheContainObject()
        {
            var id = new ID(new Guid("e6c031ee-6ebd-46a3-a0cb-f79798f4fdd7"));
            var revisionId = new ID(new Guid("e6c031ee-6ebd-46a3-a0cb-f79798f4fdd7"));
            var args = Substitute.For<ObjectCachingArgs>();
            var stubClass = new StubClass();
            var key = Substitute.For<CacheKey<ID>>(id, revisionId, Database);

            args.Result = stubClass;
            args.CacheKey = key;

            _cacheKeyResolver.GetKey(args).Returns(key);


            //Assign
            var abstractObjectCache = new SitecoreObjectCache(_cacheKeyResolver, new SitecoreGlassConfiguration());

            //Act
            abstractObjectCache.AddObject(args);

            //Assert
            Assert.IsTrue(abstractObjectCache.ContansObject(args));
        }

        [Test]
        public void AbstractObjectCacheAddObject()
        {
            //Assign

            var id = new ID(new Guid("32e12d0b-db0c-4201-bcaf-302ec53c7309"));
            var revisionId = new ID(new Guid("32e12d0b-db0c-4201-bcaf-302ec53c7309"));
            var args = Substitute.For<ObjectCachingArgs>();
            var stubClass = new StubClass();
            var key = Substitute.For<CacheKey<ID>>(id, revisionId, Database);

            args.Result = stubClass;
            args.CacheKey = key;

            _cacheKeyResolver.GetKey(args).Returns(key);

            var abstractObjectCache = new SitecoreObjectCache(_cacheKeyResolver, new SitecoreGlassConfiguration());
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
            var abstractObjectCache = new SitecoreObjectCache(_cacheKeyResolver, new SitecoreGlassConfiguration());

            var id = new ID(new Guid("d1f6f2b8-93dd-477d-8089-c3639e713fab"));
            var revisionId = new ID(new Guid("d1f6f2b8-93dd-477d-8089-c3639e713fab"));
            var args = Substitute.For<ObjectCachingArgs>();
            var stubClass = new StubClass();
            var key = Substitute.For<CacheKey<ID>>(id, revisionId, Database);

            _cacheKeyResolver.GetKey(args).Returns(key);

            args.Result = stubClass;
            args.CacheKey = key;

            //Act
            abstractObjectCache.AddObject(args);
            abstractObjectCache.AddObject(args);
        }
    }

    public class StubClass
    {

    }
}
