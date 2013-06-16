using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines;
using Glass.Mapper.Pipelines.ObjectConstruction;
using Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingSaver;
using NSubstitute;
using NUnit.Framework;
using Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingResolver;

namespace Glass.Mapper.Tests.Pipelines.ObjectConstruction.Tasks.ObjectCachingResolver
{
    [TestFixture]
    public class ObjectCachingResolverTaskFixture
    {
        private IObjectCache _cache;
        private ICacheKeyFactory _keyFactory;
        private ObjectCachingResolverTask _task;

        [SetUp]
        public void Setup()
        {
            //Assign
            _cache = Substitute.For<IObjectCache>();
            _keyFactory = Substitute.For<ICacheKeyFactory>();

            _task = new ObjectCachingResolverTask(_keyFactory);
            _task.ObjectCache = _cache;
        }
    }

    #region Stubs

    public class StubClass
    {

    }

    public interface IStubInterface
    {

    }

    #endregion
}
