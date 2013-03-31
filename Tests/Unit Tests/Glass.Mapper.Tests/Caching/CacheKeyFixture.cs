using System.Globalization;
using Glass.Mapper.Caching;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Caching
{
    [TestFixture]
    public class CacheKeyFixture
    {
        private const int Id = 42;
        private const int RevisionId = 6;
        private const string Database = "SomeDatabase";

        private CacheKey<int> _key;

        [SetUp]
        public void Setup()
        {
            _key = Substitute.For<CacheKey<int>>(42, RevisionId, Database);
        }

        [Test]
        public void CanCreateCacheKey()
        {
            //Assert
            Assert.AreEqual(_key.Database, Database);
            Assert.AreEqual(_key.Id, Id);
            Assert.AreEqual(_key.GetId(), Id);
            Assert.AreEqual(_key.RevisionId, RevisionId);
            Assert.AreEqual(_key.KeyType, typeof(int));
        }

        [Test]
        public void CanGetCacheKeyString()
        {
            //Assign
            var keyStrng = "{0},{1},{2},{3}".Formatted(Id, RevisionId, Database, typeof(int));

            //Assert
            Assert.AreEqual(keyStrng, _key.ToString());
        }

        [Test]
        public void AreKeysEqual()
        {
            var key = Substitute.For<CacheKey<int>>(Id, RevisionId, Database);
            Assert.IsTrue(_key.Equals(key));
// ReSharper disable EqualExpressionComparison
            Assert.IsTrue(_key.Equals(_key));
// ReSharper restore EqualExpressionComparison
        }

        [Test]
        public void AreKeyNotEqual()
        {
            //Assign
            var key = Substitute.For<CacheKey<string>>(
                Id.ToString(CultureInfo.InvariantCulture),
                RevisionId.ToString(CultureInfo.InvariantCulture), 
                Database);

            //Assert
// ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_key.Equals(key));
// ReSharper restore SuspiciousTypeConversion.Global
            Assert.IsFalse(_key.Equals(null));
        }

    }
    public class StubCacheKey : CacheKey<int>
    {
        public StubCacheKey(int id, int revisionId, string database)
            : base(id, revisionId, database)
        {
        }
    }
}
