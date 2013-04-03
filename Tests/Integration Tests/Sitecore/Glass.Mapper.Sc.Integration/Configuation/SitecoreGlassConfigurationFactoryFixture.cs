using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Glass.Mapper.Configuration;
using Glass.Mapper.Sc.Configuration;
using NUnit.Framework;

namespace Glass.Mapper.Sc.Integration.Configuation
{
    [TestFixture]
    public class SitecoreGlassConfigurationFactoryFixture
    {
        [Test]
        public void CanBuildGlassConfiguration()
        {
            var galssConfigFactory = new SitecoreGlassConfigurationFactory();
            var glassConfig = galssConfigFactory.CreateGlassConfiguration();

            Assert.IsNotNull(glassConfig);
        }

        [Test]
        public void HasCacheName()
        {
            var galssConfigFactory = new SitecoreGlassConfigurationFactory();
            var glassConfig = galssConfigFactory.CreateGlassConfiguration();

            Assert.AreEqual("Glass.Mapper.Caching.ObjectCaching.ConfigFile", glassConfig.CacheName);
        }

        [Test]
        public void HasCacheSize()
        {
            var galssConfigFactory = new SitecoreGlassConfigurationFactory();
            var glassConfig = galssConfigFactory.CreateGlassConfiguration();

            Assert.AreEqual(1000, glassConfig.CacheSize);
        }
    }
}
