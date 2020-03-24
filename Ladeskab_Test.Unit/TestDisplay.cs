using System;
using System.IO;
using Ladeskab.Moduls;
using NUnit.Framework;

namespace Ladeskab_Test.Unit
{
    class TestDisplay
    {
        private Ladeskab.Moduls.Display _display;

        [SetUp]
        public void SetUp()
        {
            _display = new Display();
        }

        // Test af User Instruction
        [TestCase("This is a instruction")]
        public void TestUserInstruction(string testString)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _display.DisplayUserInstructions(testString);
                string expected = string.Format($"User Instruction: {testString}{Environment.NewLine}");
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        // Test af Message
        [TestCase("This is a message")]
        public void TestChargingMessage(string testString)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _display.DisplayChargingMessage(testString);
                string expected = string.Format($"Charging Message: {testString}{Environment.NewLine}");
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
