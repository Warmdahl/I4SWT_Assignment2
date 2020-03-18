using System;
using System.IO;
using Ladeskab.Moduls;
using NUnit.Framework;

namespace Ladeskab_Test.Unit
{
    class TestDisplay
    {
        private Ladeskab.Moduls.Display _D;

        [SetUp]
        public void SetUp()
        {
            _D = new Display();
        }

        // Test af User Instruction
        [TestCase("Dette er en instruktion!")]
        public void TestUserInstruction(string testString)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _D.DisplayUserInstructions(testString);
                string expected = string.Format("Bruger instruktion: {0}{1}", testString, Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        // Test af Message
        [TestCase("Dette er en besked!")]
        public void TestChargingMessage(string testString)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _D.DisplayChargingMessage(testString);
                string expected = string.Format("Opladnings besked: {0}{1}", testString, Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
