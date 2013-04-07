using System;

namespace Glass.Mapper.Umb.CastleWindsor
{
    [Obsolete("This class is obsolete; use the GlassConfiguration class instead")]
    public class Config
    {
        public Config()
        {
            UseWindsorContructor = false;
        }
        /// <summary>
        /// Indicates that classes should be build using the Windsor dependency resolver. Default is False
        /// </summary>
        public bool UseWindsorContructor { get; set; }

    }
}
