using System;
using System.IO;
using Ladeskab.Moduls;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab_Test.Unit
{
    class TestDisplay
    {
        //private Ladeskab.Moduls.Display _D;
        private IDisplay _D;

        [SetUp]
        public void SetUp()
        {
            //_D = new Display();
            _D = Substitute.For<IDisplay>();
        }

        // Test af User Instruction
        [TestCase("This is an instruction")]
        public void TestUserInstruction(string testString)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _D.DisplayUserInstructions(testString);
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
                _D.DisplayChargingMessage(testString);
                string expected = string.Format($"Charging Message: {testString}{Environment.NewLine}");
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestCase("This is an instruction")]
        public void TestInstruction(string testString)
        {
            _D.DisplayUserInstructions(testString);
            _D.Received().DisplayUserInstructions(testString);
        }
    }
}
