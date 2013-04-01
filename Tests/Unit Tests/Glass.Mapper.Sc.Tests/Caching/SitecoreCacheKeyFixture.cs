using Glass.Mapper.Sc.Caching;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;

namespace Glass.Mapper.Sc.Tests.Caching
{
    [TestFixture]
    public class SitecoreCacheKeyFixture
    {
        private ID _id = null;
        private ID _revisionId = null;
        private const string Database = "SomeDatabase";
        private SitecoreCacheKey _key;

        [SetUp]
        public void Setup()
        {
            _id = new ID(new Guid());
            _revisionId = new ID(new Guid());
            _key = new SitecoreCacheKey(_id, _revisionId, Database);
        }

        [Test]
        public void CanCreateSitecoreCacheKey()
        {
            //Assert
            Assert.AreEqual(_key.Database, Database);
            Assert.AreEqual(_key.Id, _id);
            Assert.AreEqual(_key.GetId(), _id);
            Assert.AreEqual(_key.RevisionId, _revisionId);
            Assert.AreEqual(_key.KeyType, typeof(ID));
        }

        [Test]
        public void CanGetSitecoreCacheKeyString()
        {
            //Assign
            var keyStrng = "{0},{1},{2},{3}".Formatted(_id, _revisionId, Database, typeof(ID));

            //Assert
            Assert.AreEqual(keyStrng, _key.ToString());
        }
    }
}
