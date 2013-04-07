using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glass.Mapper.Configuration;
using Glass.Mapper.Pipelines;
using Glass.Mapper.Pipelines.ObjectConstruction;
using NUnit.Framework;

namespace Glass.Mapper.Tests.Pipelines
{
    [TestFixture]
    public class PipelineArgsFactoryFixture
    {
        private Context _context;
        private AbstractTypeCreationContext _abstractTypeCreationContext;
        private Type _type;
        private IAbstractService _service;
        private AbstractTypeConfiguration _configuration;

        [Test]
        public void CanCreateObjectCachingArgsFromPipelineArgsFactoryFactory()
        {
            IPipelineArgsFactory factory = new PipelineArgsFactory();
            AbstractPipelineArgs args = factory.CreatePipelineArgs(_context, _abstractTypeCreationContext, _configuration, _service);

            Assert.IsInstanceOf<ObjectConstructionArgs>(args);
        }
    }
}
