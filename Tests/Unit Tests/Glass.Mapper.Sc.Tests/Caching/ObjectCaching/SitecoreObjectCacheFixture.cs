using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Pipelines.ObjectConstruction;
using Glass.Mapper.Sc.Caching;
using Glass.Mapper.Sc.Configuration;
using NSubstitute;
using NUnit.Framework;
using Sitecore.Data;
using Glass.Mapper.Caching.Exceptions;

namespace Glass.Mapper.Sc.Tests.Caching.ObjectCaching
{
    [TestFixture]
    public class SitecoreObjectCacheFixture
    {
        private const string Database = "SomeDatabase";
        
        [SetUp]
        public void SetUp()
        {
        }

    }

    public class StubClass
    {

    }
}
