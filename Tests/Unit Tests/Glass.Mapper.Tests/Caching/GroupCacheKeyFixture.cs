using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching
{
    [TestFixture]
    public class GroupCacheKeyFixture
    {
        [Test]
        public void Create_GroupCacheKey_From_Null_Cache_Key_NullCacheKeyException_Thrown()
        {
            //assert
            Assert.Throws<NullCacheKeyException>(() => new GroupCacheKey(null));
        }

        [Test]
        public void Create_GroupCacheKey_From_Cache_Key_Group_Name_From_Cache_Key_Used()
        {
            //assign
            var groupIdentifier = "GroupIdentifier";
            var uniqueIdentifier = "UniqueIdentifier";
            var cacheKey = Substitute.For<AbstractCacheKey>(uniqueIdentifier, groupIdentifier);
            
            //act
            var result = new GroupCacheKey(cacheKey);

            //assert
            Assert.AreEqual(groupIdentifier, result.GroupIdentifier);
        }

        [Test]
        public void Create_GroupCacheKey_From_Cache_Key_With_Different_GroupIdentifier_Group_Name_From_Cache_Key_Used()
        {
            //assign
            var groupIdentifier = "GroupIdentifierDifferent";
            var uniqueIdentifier = "UniqueIdentifier";
            var cacheKey = Substitute.For<AbstractCacheKey>(uniqueIdentifier, groupIdentifier);

            //act
            var result = new GroupCacheKey(cacheKey);

            //assert
            Assert.AreEqual(groupIdentifier, result.GroupIdentifier);
        }
    }
}
