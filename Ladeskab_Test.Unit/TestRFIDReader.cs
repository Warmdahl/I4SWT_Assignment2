using Ladeskab.Moduls;
using NUnit.Framework;

namespace RFIDReader.Unit
{
    [TestFixture]
    class TestRFIDReader
    {
        private Ladeskab.Moduls.RFidReader _rFidReader;
        private RFIDReadEventArgs _rfidReadEventArgs;
        private int Valid_ID = 432;

        [SetUp]
        public void SetUp()
        {
            _rfidReadEventArgs = null;
            _rFidReader = new RFidReader();

            _rFidReader.RFIDReadEvent += (o, args) => { _rfidReadEventArgs = args; };
        }

        // Test 1
        [Test]
        public void RFID_Reader_EventFired()
        {
            _rFidReader.SimulateReadRFID(432);
            Assert.That(_rfidReadEventArgs.ID, Is.Not.Null);
        }

        // Test 2
        [Test]
        public void RFID_Reader_Succes()
        {
            _rFidReader.SimulateReadRFID(432);
            Assert.That(_rfidReadEventArgs.ID, Is.EqualTo(Valid_ID));
        }

        // Test 3
        [Test]
        public void RFID_Reader_Failed()
        {
            _rFidReader.SimulateReadRFID(21);
            Assert.That(_rfidReadEventArgs.ID, Is.Not.EqualTo(Valid_ID));
        }
    }
}
