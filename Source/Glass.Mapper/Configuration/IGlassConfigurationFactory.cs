using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Mapper.Configuration
{
    /// <summary>
    /// Interface to give the ability for 
    /// </summary>
    public interface IGlassConfigurationFactory<T> where T: GlassConfiguration
    {
        /// <summary>
        /// Builds a GlassConfiguration object
        /// </summary>
        /// <returns>GlassConfiguration object</returns>
        T CreateGlassConfiguration();
    }
}
