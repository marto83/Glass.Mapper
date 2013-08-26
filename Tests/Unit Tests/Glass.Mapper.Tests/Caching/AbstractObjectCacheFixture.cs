using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching
{
    [TestFixture]
    public class AbstractObjectCacheFixture
    {
        private AbstractObjectCache _objectCache;
        private AbstractCacheKey _cacheKey;
        private const string GroupIdentifier = "GroupIdentifier";
        private const string UniqueIdentifier = "UniqueIdentifier";
        
        [SetUp]
        public void SetUp()
        {
            _objectCache = Substitute.For<AbstractObjectCache>();
            _cacheKey = Substitute.For<AbstractCacheKey>(UniqueIdentifier, GroupIdentifier);
        }

        [Test]
        public void Add_Object_To_Cache_With_Null_Cache_Key_Null_Cache_Key_Exception_Thrown()
        {
           //assert
            Assert.Throws<NullCacheKeyException>(() => _objectCache.Add(null, new object()));
        }

        [Test]
        public void Add_Object_To_Cache_With_Cache_Key_Object_Cache_Contains_Cache_Key()
        {
            //act
            _objectCache.Add(_cacheKey, new object());

            //assert
            Assert.IsTrue(_objectCache.ContainCacheKey(_cacheKey));
        }

        [Test]
        public void Object_Not_Added_To_Cache_Object_Cache_Does_Not_Contain_Cache_Key()
        {
            //assert
            Assert.IsFalse(_objectCache.ContainCacheKey(_cacheKey));
        }

        [Test]
        public void Add_Object_To_Cache_With_Cache_Key_Including_Group_Key_Cache_Contain_Group()
        {
            //act
            _objectCache.Add(_cacheKey, new object());

            //assert
            Assert.IsTrue(_objectCache.IsGroupKeyContained(GroupIdentifier));
        }

        [Test]
        public void Object_Not_Added_To_Cache_With_Cache_Key_Including_Group_Cache_Does_Not_Contain_Group()
        {
            //assert
            Assert.IsFalse(_objectCache.IsGroupKeyContained(GroupIdentifier));
        }

        [Test]
        public void Object_Not_Added_To_Cache_Get_Grouped_Cached_Keys_Returns_Empty_List()
        {
            //assert
            Assert.IsEmpty(_objectCache.GetGroupedCacheKeys(GroupIdentifier));
        }

        [Test]
        public void Add_Object_To_Cache_With_Cache_Key_Including_Group_Key_Group_Contains_Cache_Key()
        {
            //act
            _objectCache.Add(_cacheKey, new object());
            var result = _objectCache.GetGroupedCacheKeys(GroupIdentifier);

            //assert
            Assert.IsTrue(result.Any(key => key.UniqueIdentifier == UniqueIdentifier));
        }

        [Test]
        public void Add_Object_To_Cache_With_Cache_Key_No_Group_Key_Default_Group_Key_Used()
        {
            //assign
            var cacheKey = Substitute.For<AbstractCacheKey>(UniqueIdentifier);
            _objectCache.Add(cacheKey, new object());

            //act
            var result = _objectCache.GetGroupedCacheKeys(AbstractCacheKey.DefaultGroupIdentifier);

            //assert
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void Add_Two_Objects_To_Cache_With_Same_Group_Key_GetGroupedCacheKeys_Returns_Both_Cache_Keys()
        {
            //assign
            var uniqueIdentifierOne = "UniqueIdentifierOne";
            var uniqueIdentifierTwo = "UniqueIdentifierTwo";
            var cacheKeyOne = Substitute.For<AbstractCacheKey>(uniqueIdentifierOne, GroupIdentifier);
            var cacheKeyTwo = Substitute.For<AbstractCacheKey>(uniqueIdentifierTwo, GroupIdentifier);

            _objectCache.Add(cacheKeyOne, new object());
            _objectCache.Add(cacheKeyTwo, new object());

            //act
            var result = _objectCache.GetGroupedCacheKeys(GroupIdentifier);

            //assert
            Assert.IsTrue(result.Any(key => key.UniqueIdentifier == uniqueIdentifierOne));
            Assert.IsTrue(result.Any(key => key.UniqueIdentifier == uniqueIdentifierTwo));
        }

        [Test]
        public void Add_Objects_To_Cache_And_Read_From_Cache_Cache_Thread_Safe()
        {
            //act
            var key = Substitute.For<AbstractCacheKey>(UniqueIdentifier, GroupIdentifier);
            _objectCache.Add(key, new object());

            Parallel.For(0, 1000, i =>
            {
                //assign
                var cacheKey = Substitute.For<AbstractCacheKey>(UniqueIdentifier + i, GroupIdentifier);

                //act
                _objectCache.Add(cacheKey, new object());

                //assert
                Assert.DoesNotThrow(() => _objectCache.ContainCacheKey(key));
            });
        }

        [Test]
        public void Add_Object_To_Cache_And_Read_From_Group_Thread_Safe()
        {
            Parallel.For(0, 1000, i =>
            {
                //assign
                var cacheKey = Substitute.For<AbstractCacheKey>(UniqueIdentifier + i, GroupIdentifier + i);

                //act
                _objectCache.Add(cacheKey, new object());

                //assert
                Assert.DoesNotThrow(() => _objectCache.IsGroupKeyContained(GroupIdentifier + i));
            });
        }

        [Test]
        public void Add_Object_To_Cache_Object_Has_Been_Cached()
        {
            //assign
            var stubObjectCache = new StubObjectCache();
            var objectForCaching = new object();

            //act
            stubObjectCache.Add(_cacheKey, objectForCaching);

            //assert
            Assert.Contains(objectForCaching, stubObjectCache.CacheDictionary.Values);
        }

        [Test]
        public void Add_Object_To_Cache_With_Same_Key()
        {
            //assign
            var stubObjectCache = new StubObjectCache();
            var objectForCachingOne = new object();
            var objectForCachingTwo = new object();

            //act
            stubObjectCache.Add(_cacheKey, objectForCachingOne);
            stubObjectCache.Add(_cacheKey, objectForCachingTwo);

            //assert
            Assert.Contains(objectForCachingOne, stubObjectCache.CacheDictionary.Values);
            Assert.IsFalse(stubObjectCache.CacheDictionary.Values.Any(x => x == objectForCachingTwo));
        }

        public class StubObjectCache:AbstractObjectCache
        {
            public volatile Dictionary<string, object> CacheDictionary = new Dictionary<string,object>();

            protected override void AddToCache(string cacheKey, object objectForCaching)
            {
               CacheDictionary.Add(cacheKey, objectForCaching);
            }

            protected override bool IsKeyUsedInCache(string cacheKey)
            {
                return CacheDictionary.ContainsKey(cacheKey);
            }
        }
    }
}
