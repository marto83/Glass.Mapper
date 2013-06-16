using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Sc.CastleWindsor;
using Glass.Mapper.Sc.Configuration.Attributes;
using NUnit.Framework;

namespace Glass.Mapper.Sc.Integration.Caching
{
    [TestFixture]
    public class SitecoreCachingFixture
    {
        [Test]
        public void GetCurrentItem_NonGeneric()
        {
            //Assign
            var db = Sitecore.Configuration.Factory.GetDatabase("master");
            var context = Context.Create(Utilities.CreateStandardResolver());
            context.Load(new SitecoreAttributeConfigurationLoader("Glass.Mapper.Sc.Integration"));

            var path = "/sitecore/content/Tests/SitecoreContext/GetCurrentItem/Target";
            var scContext = new SitecoreContext();

            Sitecore.Context.Item = db.GetItem(path);

            //Act
            var result = scContext.GetCurrentItem(typeof(SitecoreContextFixture.StubClass)) as SitecoreContextFixture.StubClass;

            //Assert
            Assert.AreEqual(Sitecore.Context.Item.ID, result.Id);

        }
    }
}
