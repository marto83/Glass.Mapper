using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;

namespace Glass.Mapper.Sc.Caching
{
    public class CacheClearEvent
    {
        public void ClearCache(object sender, EventArgs args)
        {
            foreach (var contextPair in Context.Contexts)
            {
                try
                {
                    Sitecore.Diagnostics.Log.Info("Glass Cache Clearing Context {0}".Formatted(contextPair.Key), this);
                    var context = contextPair.Value;

                    var cacheManager = context.DependencyResolver.Resolve<ICacheManager>();

                    if (cacheManager == null)
                    {
                        Sitecore.Diagnostics.Log.Info("Glass Cache Manager not set for {0}".Formatted(contextPair.Key),
                            this);
                    }
                    else
                    {
                        cacheManager.ClearCache();
                        Sitecore.Diagnostics.Log.Info("Glass Cache Manager cleared for {0}".Formatted(contextPair.Key),
                            this);
                    }
                }
                catch(Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Glass Cache Manager error for {0}.".Formatted(contextPair.Key), ex,
                            this);
                }

            }

        }
    }
}
