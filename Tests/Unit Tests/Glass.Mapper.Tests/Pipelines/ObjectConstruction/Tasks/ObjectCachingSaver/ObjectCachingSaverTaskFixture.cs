using System;
using Glass.Mapper.Caching;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines;
using Glass.Mapper.Pipelines.ObjectConstruction;
using Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingSaver;
using NSubstitute;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Pipelines.ObjectConstruction.Tasks.ObjectCachingSaver
{
    [TestFixture]
    public class ObjectCachingSaverTaskFixture
    {
        private IObjectCache _cache;
        private ICacheKeyFactory _keyFactory;
        private ObjectCachingSaverTask _task;

        [SetUp]
        public void Setup()
        {
            //Assign
            _cache = Substitute.For<IObjectCache>();
            _keyFactory = Substitute.For<ICacheKeyFactory>();
            
            _task = new ObjectCachingSaverTask(_keyFactory);
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
