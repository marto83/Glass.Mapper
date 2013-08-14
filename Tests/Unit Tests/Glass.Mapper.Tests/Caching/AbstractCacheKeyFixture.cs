using System;
using Glass.Mapper.Caching;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching
{
    [TestFixture]
    public class AbstractCacheKeyFixture
    {
        [Test]
        public void Can_Create_AbstractCacheKey()
        {
            var cacheKey = Substitute.For<AbstractCacheKey>();

            Assert.NotNull(cacheKey);
        }
        [Test]
        public void Create_Cache_Key_Passing_Null_Exception_Thrown()
        {
            Assert.Throws<NullReferenceException>(() => new StubCacheKey("", null));
        }

        [Test]
        public void Create_Cache_Key_Passing_Object_Type_Base_Key_Is_Object_Type()
        {
            //assign
            var testObject = new StubObject();

            //act
            var cacheKey = Substitute.For<AbstractCacheKey>(testObject.GetType());
            
            //assert
            Assert.AreEqual(testObject.GetType().FullName, cacheKey.BaseKey);
        }

        [Test]
        public void Create_Cache_Key_Passing_Object_Type_And_Extra_Identification()
        {
            //assign
            var testObject = new StubObject();
            var testIdentification = "Test";

            //act
            var cacheKey = new StubCacheKey(testIdentification, testObject.GetType());

            //assert
            Assert.AreEqual(testIdentification + testObject.GetType().FullName, cacheKey.GetKey());

        }

        private class StubCacheKey : AbstractCacheKey
        {
            private string Key { get; set; }
            public StubCacheKey(string key, Type objectType)
                : base(objectType)
            {
                this.Key = key;
            }

            public override string InternalGetKey()
            {
                return Key;
            }
        }

        private class StubObject
        {

        }
    }
}
