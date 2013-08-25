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
        [Test]
        public void Add_Object_To_Cache_With_Null_Cache_Key_Null_Cache_Key_Exception_Thrown()
        {
            //assign
            var objectCache = Substitute.For<AbstractObjectCache>();

            //assert
            Assert.Throws<NullCacheKeyException>(() => objectCache.Add(null, new object()));
        }

        [Test]
        public void Add_Object_To_Cache_With_Cache_Key_Object_Cache_Contains_Cache_Key()
        {
            //assign 
            var objectCache = Substitute.For<AbstractObjectCache>();
            var groupIdentifier = "GroupIdentifier";
            var uniqueIdentifier = "UniqueIdentifier";
            var cacheKey = Substitute.For<AbstractCacheKey>(uniqueIdentifier, groupIdentifier);
            var objectForCaching = new object();
            
            //act
            objectCache.Add(cacheKey, objectForCaching);

            //assert
            Assert.IsTrue(objectCache.ContainCacheKey(cacheKey));
        }

        [Test]
        public void Object_Not_Added_To_Cache_Object_Cache_Does_Not_Contain_Cache_Key()
        {
            //assign 
            var objectCache = Substitute.For<AbstractObjectCache>();
            var cacheKey = Substitute.For<AbstractCacheKey>();

            //assert
            Assert.IsFalse(objectCache.ContainCacheKey(cacheKey));
        }

        [Test]
        public void Add_Object_To_Cache_With_Cache_Key_Including_Group_Key_Cache_Contain_Group()
        {
            //assign 
            var objectCache = Substitute.For<AbstractObjectCache>();
            var groupIdentifier = "GroupIdentifier";
            var uniqueIdentifier = "UniqueIdentifier";
            var cacheKey = Substitute.For<AbstractCacheKey>(uniqueIdentifier, groupIdentifier);
            var objectForCaching = new object();
            
            //act
            objectCache.Add(cacheKey, objectForCaching);

            //assert
            Assert.IsTrue(objectCache.ContainGroupKey(groupIdentifier));
        }

        [Test]
        public void Object_Not_Added_To_Cache_With_Cache_Key_Including_Group_Cache_Does_Not_Contain_Group()
        {
            //assign 
            var objectCache = Substitute.For<AbstractObjectCache>();
            var groupIdentifier = "GroupIdentifier";

            //assert
            Assert.IsFalse(objectCache.ContainGroupKey(groupIdentifier));
        }


        [Test]
        public void Object_Not_Added_To_Cache_Get_Grouped_Cached_Keys_Returns_Empty_List()
        {
            //assign 
            var objectCache = Substitute.For<AbstractObjectCache>();
            var groupIdentifier = "GroupIdentifier";

            //assert
            Assert.IsEmpty(objectCache.GetGroupedCacheKeys(groupIdentifier));
        }


        [Test]
        public void Add_Object_To_Cache_With_Cache_Key_Including_Group_Key_Group_Contains_Cache_Key()
        {
            //assign 
            var objectCache = Substitute.For<AbstractObjectCache>();
            var groupIdentifier = "GroupIdentifier";
            var uniqueIdentifier = "UniqueIdentifier";
            var cacheKey = Substitute.For<AbstractCacheKey>(uniqueIdentifier, groupIdentifier);
            var objectForCaching = new object();

            //act
            objectCache.Add(cacheKey, objectForCaching);
            var result = objectCache.GetGroupedCacheKeys(groupIdentifier);

            //assert
            Assert.IsTrue(result.Any(key => key.UniqueIdentifier == uniqueIdentifier));
        }
    }
}
