using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Glass.Mapper.Configuration;

namespace Glass.Mapper.Sc.Configuration
{
    /// <summary>
    /// GlassConfigurationFactory for Sitecore
    /// </summary>
    public class SitecoreGlassConfigurationFactory : IGlassConfigurationFactory<SitecoreGlassConfiguration>
    {
        private const string GlassConfigurationSectionNode = "GlassConfigurationSection/ObjectCaching";
        public SitecoreGlassConfiguration CreateGlassConfiguration()
        {
            XmlNode config = Sitecore.Configuration.Factory.GetConfigNode(GlassConfigurationSectionNode);
            return Sitecore.Configuration.Factory.CreateObject<SitecoreGlassConfiguration>(config);
        }
    }
}
