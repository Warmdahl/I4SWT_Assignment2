﻿using System;
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

        [TestCase(true, 12)]
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
        [TestCase(false, 10)]
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