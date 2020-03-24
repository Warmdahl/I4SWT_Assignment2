using System;
using System.IO;
using Ladeskab.Moduls;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Ladeskab_Test.Unit
{
    public class TestLog
    {
        private ILog _log;
        
        [SetUp]
        public void SetUp()
        {
            _log = Substitute.For<ILog>();
        }

        [TestCase(true, 12)]
        public void logfile(bool locked, int id)
        {
            _log.Log(locked, id);
            _log.Received().Log(true, 12);
        }
        
        

    }
}