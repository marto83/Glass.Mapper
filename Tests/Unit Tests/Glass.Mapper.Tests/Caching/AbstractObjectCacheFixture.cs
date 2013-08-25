using System.Linq;
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
            Assert.IsTrue(_objectCache.ContainGroupKey(GroupIdentifier));
        }

        [Test]
        public void Object_Not_Added_To_Cache_With_Cache_Key_Including_Group_Cache_Does_Not_Contain_Group()
        {
            //assert
            Assert.IsFalse(_objectCache.ContainGroupKey(GroupIdentifier));
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


    }
}
