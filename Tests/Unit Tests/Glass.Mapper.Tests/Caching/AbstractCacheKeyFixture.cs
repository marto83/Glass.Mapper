using Glass.Mapper.Caching;
using Glass.Mapper.Caching.Exceptions;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching
{
    [TestFixture]
    public class AbstractCacheKeyFixture
    {
        [Test]
        public void Create_Cache_Key_With_Null_Unique_Identifier_Null_Unique_Identifier_Exception_Thrown()
        {
            //assert
            Assert.Throws<NullUniqueIdentifierException>(() => new StubAbstractCacheKey(null));
        }

        [Test]
        public void Create_Cache_Key_With_Empty_Unique_Identifier_Empty_Unique_Identifier_Exception_Thrown()
        {
            //assert
            Assert.Throws<EmptyUniqueIdentifierException>(() => new StubAbstractCacheKey(string.Empty));
        }

        [Test]
        public void Create_Cache_Key_With_String_Unique_Identifier_String_Is_Unique_Identifier()
        {
            //assign
            var uniqueIdentifier = "TestUniqueIdentifier";
            var cacheKey = new StubAbstractCacheKey(uniqueIdentifier);

            //assert
            Assert.AreEqual(uniqueIdentifier, cacheKey.UniqueIdentifier);
        }

        [Test]
        public void Create_Cache_Key_With_Different_String_Unique_Identifier_String_Is_Unique_Identifier()
        {
            //assign
            var uniqueIdentifier = "Different_TestUniqueIdentifier";
            var cacheKey = new StubAbstractCacheKey(uniqueIdentifier);

            //assert
            Assert.AreEqual(uniqueIdentifier, cacheKey.UniqueIdentifier);
        }

        [Test]
        public void Create_Cache_Key_With_Null_Group_Identifier_Null_Group_Identifier_Exception_Thrown()
        {
            //assign
            var uniqueIdentifier = "TestUniqueIdentifier";
            Assert.Throws<NullGroupIdentifierException>(() => new StubAbstractCacheKey(uniqueIdentifier, null));
        }

        [Test]
        public void Create_Cache_Key_With_Empty_Group_Identifier_Default_Identifier_Used()
        {
            //assign
            var uniqueIdentifier = "TestUniqueIdentifier";
            var groupIdentifier = string.Empty;
            var cacheKey = new StubAbstractCacheKey(uniqueIdentifier, groupIdentifier);

            //assert
            Assert.AreEqual(AbstractCacheKey.DefaultGroupIdentifier, cacheKey.GroupIdentifier);
        }

        [Test]
        public void Create_Cache_Key_With_String_Group_Identifier_String_Is_Group_Identifier()
        {
            //assign
            var uniqueIdentifier = "TestUniqueIdentifier";
            var groupIdentifier = "TestGroupIdentifier";
            var cacheKey = new StubAbstractCacheKey(uniqueIdentifier, groupIdentifier);

            //assert
            Assert.AreEqual(groupIdentifier, cacheKey.GroupIdentifier);
        }

        [Test]
        public void Two_Cache_Keys_With_Same_Unique_Identifier_Are_Equal()
        {
            //assign
            var uniqueIdentifier = "TestUniqueIdentifier";
            var cacheKeyOne = new StubAbstractCacheKey(uniqueIdentifier);
            var cacheKeyTwo = new StubAbstractCacheKey(uniqueIdentifier);

            //act
            var result = cacheKeyOne.Equals(cacheKeyTwo);

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Two_Cache_Keys_With_Different_Unique_Identifier_Are_Not_Equal()
        {
            //assign
            var cacheKeyOne = new StubAbstractCacheKey("TestUniqueIdentifierOne");
            var cacheKeyTwo = new StubAbstractCacheKey("TestUniqueIdentifierTwo");

            //act
            var result = cacheKeyOne.Equals(cacheKeyTwo);

            //assert
            Assert.IsFalse(result);
        }

        private class StubAbstractCacheKey: AbstractCacheKey 
        {
            public StubAbstractCacheKey(string uniqueIdentifier)
                : base(uniqueIdentifier, string.Empty)
            {}

             public StubAbstractCacheKey(string uniqueIdentifier, string groupIdentifier)
                : base(uniqueIdentifier, groupIdentifier)
            {}
        }
    }
}
