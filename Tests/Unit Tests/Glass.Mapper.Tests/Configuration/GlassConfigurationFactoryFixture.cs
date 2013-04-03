using Glass.Mapper.Configuration;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Tests.Configuration
{
    [TestFixture]
    public class GlassConfigurationFactoryFixture
    {
        [Test]
        public void CanBuildGlassConfiguration()
        {
            var galssConfigFactory = Substitute.For<IGlassConfigurationFactory<GlassConfiguration>>();
            galssConfigFactory.CreateGlassConfiguration().Returns(new GlassConfiguration());
            var glassConfig = galssConfigFactory.CreateGlassConfiguration();

            Assert.AreEqual("Glass.Mapper.Caching.ObjectCaching", glassConfig.CacheName);
        }
    }
}
