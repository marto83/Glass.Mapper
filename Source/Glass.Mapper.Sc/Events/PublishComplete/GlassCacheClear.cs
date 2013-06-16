using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;

namespace Glass.Mapper.Sc.Events.PublishComplete
{
    public class GlassCacheClear
    {
        public void ClearCache(object sender, EventArgs args)
        {
            foreach (var context in Context.Contexts)
            {
                try
                {
                    var cache = context.Value.DependencyResolver.Resolve<IObjectCache>();
                    cache.ClearCache();
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Failed to clear cache for context {0}".Formatted(context.Key), this);
                }
            }
        }
    }
}
