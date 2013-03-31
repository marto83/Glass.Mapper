using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.Exceptions;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Pipelines.ObjectConstruction;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching.ObjectCaching
{
    [TestFixture]
    public class AbstractObjectCacheFixture
    {
        private ObjectCachingArgs _args;
        private AbstractCacheKeyResolver<int> _cacheKeyResolver;
        private const int Id = 42;
        private const int RevisionId = 6;
        private const string Database = "SomeDatabase";
        private StubClass _stubObject;

        private CacheKey<int> _key;

        [SetUp]
        public void SetUp()
        {
            _args = Substitute.For<ObjectCachingArgs>();
            _stubObject = new StubClass();
            _args.Result = _stubObject;
            
            _cacheKeyResolver = Substitute.For<AbstractCacheKeyResolver<int>>();
            _key = Substitute.For<CacheKey<int>>(42, RevisionId, Database);

            _args.CacheKey = _key;
            _cacheKeyResolver.GetKey(_args).Returns(_key);
        }
        [Test]
        public void AbstractObjectCacheContainObject()
        {
            //Assign
            var abstractObjectCache = new StubAbstractObjectCache(_cacheKeyResolver);

            //Act
            abstractObjectCache.AddObject(_args);

            //Assert
            Assert.IsTrue(abstractObjectCache.ContansObject(_args));
        }

        [Test]
        public void AbstractObjectCacheAddObject()
        {
            //Assign
            var args = Substitute.For<ObjectCachingArgs>();
            var abstractObjectCache = new StubAbstractObjectCache(_cacheKeyResolver);
            abstractObjectCache.AddObject(_args);
            _cacheKeyResolver.GetKey(args).Returns(_key);
            args.CacheKey = _key;

            //Act
            abstractObjectCache.GetObject(args).Returns(_key);

            //Assert
            Assert.AreSame(_stubObject, abstractObjectCache.GetObject(args));
        }

        [Test]
        [ExpectedException(typeof(DuplicatedKeyObjectCacheException))]
        public void ExpectWhenTryToAddObjectAgain()
        {
            //Assign
            var abstractObjectCache = new StubAbstractObjectCache(_cacheKeyResolver);

            //Act
            abstractObjectCache.AddObject(_args);
            abstractObjectCache.AddObject(_args);
        }

    }

    public class StubAbstractObjectCache : AbstractObjectCache<int>
    {
        private volatile Hashtable _table = new Hashtable();

        public StubAbstractObjectCache(AbstractCacheKeyResolver<int> cacheKeyResolver)
            : base(cacheKeyResolver)
        {
        }
        protected override bool InternalContansObject(string objectKey)
        {
            return _table.ContainsKey(objectKey);
        }

        protected override void InternalAddObject(string objectKey, object objectForCaching)
        {
            _table.Add(objectKey, objectForCaching);
        }

        protected override object InternalGetObject(string objectKey)
        {
            return _table[objectKey];
        }
    }

    public class StubClass
    {

    }
}
