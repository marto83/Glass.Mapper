using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
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

        private CacheKey<int> _key;

        [SetUp]
        public void SetUp()
        {
            _args = Substitute.For<ObjectCachingArgs>();
            _cacheKeyResolver = Substitute.For<AbstractCacheKeyResolver<int>>();
            _key = Substitute.For<CacheKey<int>>(42, RevisionId, Database);
            _cacheKeyResolver.GetKey(_args).Returns(_key);
        }
        [Test]
        public void AbstractObjectCacheContainObject()
        {
            var abstractObjectCache = Substitute.For<AbstractObjectCache<int>>(_cacheKeyResolver);
            //abstractObjectCache.ContansObject(_args).Returns();
            Assert.IsTrue(abstractObjectCache.ContansObject(_args));
        }
    }
}
