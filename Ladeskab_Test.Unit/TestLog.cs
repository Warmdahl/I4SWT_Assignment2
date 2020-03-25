using System;
using System.IO;
using System.Linq;
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

        
        //Tester loggen når døren er låses.
        [TestCase(true, 12)]
        [TestCase(true, 9)]
        [TestCase(true, 100)]
        [TestCase(true, 0)]
        [TestCase(true, -1)]
        public void logfileTrue(bool locked, int id)
        {
            var now = DateTime.Now;
            var s = now + ", ";
            string st = "";
            using (StringWriter sw = new StringWriter())
            {

                _log.Log(locked, id);
                s = s + "locked by: " + id + ", door is locked";
                
                st = File.ReadLines("navn.txt").Last();
                

                Assert.AreEqual(s, st);
            }
        } 
        
        //Tester loggen når døren låses op.
        [TestCase(false, 12)]
        [TestCase(false, 9)]
        [TestCase(false, 100)]
        [TestCase(false, 0)]
        [TestCase(false, -1)]
        public void logfileFalse(bool locked, int id)
        {
            var now = DateTime.Now;
            var s = now + ", ";
            string st;
            using (StringWriter sw = new StringWriter())
            {

                _log.Log(locked, id);
                s = s + "unlocked by: " + id + ", door is unlocked";
                
                st = File.ReadLines("navn.txt").Last();
                

                Assert.AreEqual(s, st);
            }
        }
    }
}