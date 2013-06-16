using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Caching.ObjectCaching;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines;
using Glass.Mapper.Pipelines.ObjectConstruction;
using NSubstitute;
using NUnit.Framework;
using Glass.Mapper.Pipelines.ObjectConstruction.Tasks.ObjectCachingResolver;

namespace Glass.Mapper.Tests.Pipelines.ObjectConstruction.Tasks.ObjectCachingResolver
{
    [TestFixture]
    public class ObjectCachingResolverTaskFixture
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
            _task = new ObjectCachingResolverTask();

            //Assign
            _type = typeof(StubClass);
            _service = Substitute.For<IAbstractService>();

            _context = Context.Create(Substitute.For<IDependencyResolver>());
            _context.ConfigureCache(Substitute.For<AbstractObjectCacheConfiguration>());
            _context.ObjectCacheConfiguration.ObjectCache = Substitute.For<IAbstractObjectCache>();

            _abstractTypeCreationContext = Substitute.For<AbstractTypeCreationContext>();
            _abstractTypeCreationContext.RequestedType = _type;

            _configuration = Substitute.For<AbstractTypeConfiguration>();
            _configuration.Type = _type;
        }

        #region Method - Execute

        [Test]
        public void CanTaskExecute()
        {
            var args = new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service);
            _task.Execute(args);
        }

        [Test]
        public void CanTastExecuteAndAbortTask()
        {
            var args = new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service);

            _context.ObjectCacheConfiguration.ObjectCache.ContansObject(args).Returns(true);
            _task.Execute(args);

            Assert.IsTrue(args.IsAborted);
        }

        [Test]
        public void CanTastExecuteAndReturnsObjectFromCache()
        {
            var stubClass = new StubClass();

            var args = new ObjectCachingArgs(_context, _abstractTypeCreationContext, _configuration, _service);

            _context.ObjectCacheConfiguration.ObjectCache.GetObject(args).Returns(stubClass);
            _context.ObjectCacheConfiguration.ObjectCache.ContansObject(args).Returns(true);

            _task.Execute(args);

            Assert.AreEqual(stubClass, args.Result);
        }

        [Test]
        [ExpectedException(typeof(ObjectConstructionException))]
        public void CanNotPassObjectConstructionArgsToObjectCachingSaverTaskExecute()
        {
            var args = new ObjectConstructionArgs(_context, _abstractTypeCreationContext, _configuration, _service);
            _task.Execute(args);
        }

        #endregion
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
