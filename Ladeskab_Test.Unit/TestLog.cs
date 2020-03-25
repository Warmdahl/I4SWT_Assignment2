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

        /*[TestCase(true, 12)]
        public void logfileTrue(bool locked, int id)
        {
            using (StringWriter sw = new StringWriter())
            {

                _log.Log(locked, id);
                string s;
                string st = "locked by: " + id + ", door is locked";
                using (StreamReader SR = File.OpenText("navn.txt"))
                {
                    s = SR.ReadLine();
                }

                Assert.AreEqual(st, s);
            }
        } */
        [TestCase(false, 10)]
            public void logfileFalse(bool locked, int id)
            {
                using (StringWriter sw = new StringWriter())
                {

                    _log.Log(locked, id);
                    string s;
                    string st = "unlocked by: " + id + ", door is unlocked";
                    using (StreamReader SR = File.OpenText("navn.txt"))
                    {
                        s = SR.ReadLine();
                    }

                    Assert.AreEqual(st, s);
                }
            }
            
        
        

    }
}