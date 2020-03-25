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
        private Ladeskab.Moduls.LogFile _log;
        
        [SetUp]
        public void SetUp()
        {
            _log = new LogFile();
        }

        [TestCase(true, 12)]
        public void logfile(bool locked, int id)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _log.Log(locked, id);
                string expected = string.Format($"User Instruction: {testString}{Environment.NewLine}");
                Assert.AreEqual(expected, sw.ToString());
            }
            
        }
        
        

    }
}