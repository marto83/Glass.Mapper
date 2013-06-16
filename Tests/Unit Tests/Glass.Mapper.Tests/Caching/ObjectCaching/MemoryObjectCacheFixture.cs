using Glass.Mapper.Caching;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching.ObjectCaching
{
    [TestFixture]
    public class MemoryObjectCacheFixture
    {
        private const string Database = "SomeDatabase";
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void AddObject_ObjectAddedToCache_ContainsObjectReturnsTrue()
        {
            //Assign
            var abstractObjectCache = new MemoryObjectCache();
            string cacheKeyValue = "9CDC0A52-5410-452F-8764-03E396A11E90";
            var obj = new Stub();
            var key = Substitute.For<ICacheKey>();
            key.GetKey().Returns(cacheKeyValue);

            //Act
            abstractObjectCache.AddObject(key, obj);

            //Assert
            Assert.IsTrue(abstractObjectCache.ContainsObject(key));
        }

        [Test]
        public void GetObject_ReturnsObjectInCache_GetObjectReturnsObject()
        {
            //Assign
            var abstractObjectCache = new MemoryObjectCache();
            string cacheKeyValue = "9CDC0A52-5410-452F-8764-03E396A11E90";
            var obj = new Stub();
            var key = Substitute.For<ICacheKey>();
            key.GetKey().Returns(cacheKeyValue);

            abstractObjectCache.AddObject(key, obj);

            //Act
            var result = abstractObjectCache.GetObject(key);

            //Assert
            Assert.AreEqual(obj, result);
        }

       
        #region Stubs

        public class Stub{}

        #endregion
    }
}
