using System;
using Glass.Mapper.Caching.Configuration;
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
        private IPipelineTask<ObjectConstructionArgs> _task;
        private Context _context;
        private AbstractTypeCreationContext _abstractTypeCreationContext;
        private Type _type;
        private IAbstractService _service;
        private AbstractTypeConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _task = new ObjectCachingSaverTask();

            //Assign
            _type = typeof(ObjectCachingResolver.StubClass);
            _service = Substitute.For<IAbstractService>();

            _context = Context.Create(Substitute.For<IDependencyResolver>());
            _context.ConfigureCache(Substitute.For<AbstractObjectCacheConfiguration>());
            _context.ObjectCacheConfiguration.ObjectCache = Substitute.For<IAbstractObjectCache>();

            _abstractTypeCreationContext = Substitute.For<AbstractTypeCreationContext>();
            _abstractTypeCreationContext.RequestedType = _type;

            _configuration = Substitute.For<AbstractTypeConfiguration>();
            _configuration.Type = _type;
        }

        [Test]
        public void CanTaskExecute()
        {
            var args = new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service);
            _task.Execute(args);
        }

        [Test]
        public void CanTastExecuteAndDoesNotAbortTask()
        {
            var args = new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service);

            _context.ObjectCacheConfiguration.ObjectCache.ContansObject(args).Returns(true);
            _task.Execute(args);

            Assert.IsFalse(args.IsAborted);
        }

        [Test]
        public void CanTastExecuteAndAddObjectToCache()
        {
            var stubClass = new StubClass();

            var args = new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service);
            args.Result = stubClass;
            _task.Execute(args);

            _context.ObjectCacheConfiguration.ObjectCache.Received().AddObject(args);
        }

        [Test]
        [ExpectedException(typeof(ObjectConstructionException))]
        public void CanNotPassObjectConstructionArgsToObjectCachingSaverTaskExecute()
        {
            var args = new ObjectConstructionArgs(_context, _abstractTypeCreationContext, _configuration, _service);
            _task.Execute(args);
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
