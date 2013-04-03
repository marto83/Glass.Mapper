using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Configuration;
using Glass.Mapper.Sc.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Sc.Tests.Configuration
{
    [TestFixture]
    public class SitecoreGlassConfigurationFactoryFixture
    {
        [Test]
        public void CanBuildGlassConfiguration()
        {
            var galssConfigFactory = Substitute.For<IGlassConfigurationFactory<SitecoreGlassConfiguration>>();
            galssConfigFactory.CreateGlassConfiguration().Returns(new SitecoreGlassConfiguration());
            var glassConfig = galssConfigFactory.CreateGlassConfiguration();

            Assert.IsNotNull(glassConfig);
        }

        [Test]
        public void HasCacheName()
        {
            var galssConfigFactory = Substitute.For<IGlassConfigurationFactory<SitecoreGlassConfiguration>>();
            galssConfigFactory.CreateGlassConfiguration().Returns(new SitecoreGlassConfiguration());
            var glassConfig = galssConfigFactory.CreateGlassConfiguration();

            Assert.AreEqual("Glass.Mapper.Caching.ObjectCaching", glassConfig.CacheName);
        }

        [Test]
        public void HasCacheSize()
        {
            var galssConfigFactory = Substitute.For<IGlassConfigurationFactory<SitecoreGlassConfiguration>>();
            galssConfigFactory.CreateGlassConfiguration().Returns(new SitecoreGlassConfiguration());
            var glassConfig = galssConfigFactory.CreateGlassConfiguration();

            Assert.AreEqual(100, glassConfig.CacheSize);

        }
    }
}
