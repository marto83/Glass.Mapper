using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Caching;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching
{
    [TestFixture]
    public class AbstractObjectCacheFixture
    {
        [Test]
        public void Can_Create_Object_Cache()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>();

            //act

            //assert
            Assert.NotNull(objectCache);
        }

        [Test]
        public void Create_Object_Cache_Default_Constructor_Base_Key_Is_Default()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>();

            //act

            //assert
            Assert.AreEqual(AbstractObjectCache.DefaultAbstractObjectCacheBaseKey, objectCache.AbstractObjectCacheBaseKey); 
        }

        [Test]
        public void Create_Object_Cache_Pass_Null_To_Constructor_Base_Key_Is_Default()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>(null);

            //act

            //assert
            Assert.AreEqual(AbstractObjectCache.DefaultAbstractObjectCacheBaseKey, objectCache.AbstractObjectCacheBaseKey);
        }

        [Test]
        public void Create_Object_Cache_Pass_Empty_String_To_Constructor_Base_Key_Is_Default()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>(string.Empty);

            //act

            //assert
            Assert.AreEqual(AbstractObjectCache.DefaultAbstractObjectCacheBaseKey, objectCache.AbstractObjectCacheBaseKey);
        }

        [Test]
        public void Create_Object_Cache_Pass_String_To_Constructor_Base_Key_Is_String()
        {
            //assign
            var testBaseKey = "TestBaseKey";
            var objectCache = Substitute.For<AbstractObjectCache>(testBaseKey);

            //act

            //assert
            Assert.AreEqual(testBaseKey, objectCache.AbstractObjectCacheBaseKey);
        }

        [Test]
        public void Add_Null_To_Cache_Null_Object_Cache_Null_Reference_Exception_Thrown()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>();
            var cacheKey = Substitute.For<AbstractCacheKey>();

            //act
            Assert.Throws<NullReferenceException>(() => objectCache.Add(cacheKey, null));
        }

        
        [Test]
        public void Add_Object_To_Cache_Object_Has_Been_Added()
        {
            //assign
            var objectCache = new StubAbstractObjectCache();
            var cacheKey = BuildCacheKey("TestKey");
            var objectToBeCached = new StubObject();

            //act
            objectCache.Add(cacheKey, objectToBeCached);

            Assert.AreSame(objectToBeCached, objectCache.Cache["TestKey" + objectCache.AbstractObjectCacheBaseKey]);
        }

        [Test]
        public void Add_Two_Objects_To_Cache_With_Same_Key_Duplicated_Key_Exception_Thrown()
        {
            //assign
            var objectCache = new StubAbstractObjectCache();
            var cacheKey = BuildCacheKey("TestKey");

            var firstObjectToBeCached = new StubObject();
            var secondObjectToBeCached = new StubObject();

            //act
            objectCache.Add(cacheKey, firstObjectToBeCached);

            Assert.Throws<DuplicatedKeyException>(() => objectCache.Add(cacheKey, secondObjectToBeCached));
        }

        [Test]
        public void Add_Object_To_Group_Cache_Null_As_Group_Cache_Key_Null_Reference_Exception_Thrown()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>();
            var cacheKey = Substitute.For<AbstractCacheKey>();
            var objectToBeCached = new StubObject();

            objectCache.Add(cacheKey, objectToBeCached);

            //act
            Assert.Throws<NullReferenceException>(() => objectCache.AddToGroup(null, "TESTKEY", objectToBeCached));
        }

        [Test]
        public void Add_Object_To_Group_Cache_Empty_String_As_Group_Cache_Key_Null_Reference_Exception_Thrown()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>();
            var cacheKey = "TestKey";
            var objectToBeGrouped = new StubObject();

            //act
            Assert.Throws<NullReferenceException>(() => objectCache.AddToGroup(string.Empty, cacheKey, objectToBeGrouped));
        }

        [Test]
        public void Add_Object_To_Group_Cache_Null_As_Cache_Key_Null_Reference_Exception_Thrown()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>();
            var cacheKey = Substitute.For<AbstractCacheKey>();
            var objectToBeCached = new StubObject();

            objectCache.Add(cacheKey, objectToBeCached);

            //act
            Assert.Throws<NullReferenceException>(() => objectCache.AddToGroup("TestGroup", null, objectToBeCached));
        }


        private static AbstractCacheKey BuildCacheKey(string key)
        {
            var cacheKey = Substitute.For<AbstractCacheKey>();
            cacheKey.InternalGetKey().Returns(key);
            return cacheKey;
        }

        private class StubAbstractObjectCache : AbstractObjectCache
        {
            public readonly Dictionary<string, object> Cache;

            public StubAbstractObjectCache()
            {
                Cache = new Dictionary<string, object>();
            }

            protected override void InternalAdd(string key, object objectForCaching)
            {
                Cache.Add(key, objectForCaching);
            }

            protected override bool CheckKey(string key)
            {
                return Cache.Keys.Any(k => k == key);
            }
        }

        private class StubObject
        { }
    }
}
