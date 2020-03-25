using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using Ladeskab.Moduls;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace Ladeskab_Test.Unit
{
    class TestStationControl
    {
        private IDisplay _display;
        private IDoor _door;
        private ILog _logfile;
        private IRFidReader _rFidReader;
        private IChargeControl _chargeControl;
        private IUsbCharger _usbCharger;
        private StationControl _stationControl;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _logfile = Substitute.For<ILog>();
            _rFidReader = Substitute.For<IRFidReader>();
            _chargeControl = Substitute.For<IChargeControl>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _stationControl = new StationControl(_door, _chargeControl, _rFidReader, _display, _logfile, _usbCharger);
        }

        [Test]
        public void StationControlIsAvailable()
        {
            Assert.That(_stationControl._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void StationControlWorksWithDoorEvents()
        {
            _door.Received().ChangedValueEvent += Arg.Any<EventHandler<ChangedEventArgs>>();
        }

        [Test]
        public void StationControlWorksWithRFidEvents()
        {
            _rFidReader.Received().RFIDReadEvent += Arg.Any<EventHandler<RFIDReadEventArgs>>();
        }

        [TestCase(true, "Dør åben, tilslut telefon", StationControl.LadeskabState.DoorOpen)]
        [TestCase(false, "Indlæs RFID", StationControl.LadeskabState.Available)]
        public void StationControlHandleDoorstateDoorOpen(bool open, string output, StationControl.LadeskabState state)
        {
            _door.ChangedValueEvent += Raise.EventWith<ChangedEventArgs>(new ChangedEventArgs() {DoorState = true});
            _door.ChangedValueEvent += Raise.EventWith<ChangedEventArgs>(new ChangedEventArgs() {DoorState = open});
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
            Assert.That(_stationControl._state, Is.EqualTo(state));
        }

        [TestCase(false, "Indlæs RFID", StationControl.LadeskabState.Available)]
        [TestCase(true, "Dør åben, tilslut telefon", StationControl.LadeskabState.DoorOpen)]
        public void StationControlHandleDoorstateAvailable(bool open, string output, StationControl.LadeskabState state)
        {
            _door.ChangedValueEvent += Raise.EventWith<ChangedEventArgs>(new ChangedEventArgs() {DoorState = open});
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
            Assert.That(_stationControl._state, Is.EqualTo(state));
        }

        /* disse test virker ikke i nu, men det er tæt på. Skal arbejdes videre med*/

        [TestCase(50, "Ladeskab optaget", true, StationControl.LadeskabState.Locked)]
        public void StationControlRFIdAvailableConnected(int id, string output, bool islocked, StationControl.LadeskabState state)
        {
            _chargeControl.IsConnected().Returns(true);
            _rFidReader.RFIDReadEvent += Raise.EventWith<RFIDReadEventArgs>(new RFIDReadEventArgs() { ID = id });
            _door.Received().LockDoor();
            _logfile.Received().Log(islocked, id);
            _chargeControl.Received().StartCharge();
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
            Assert.That(_stationControl._state, Is.EqualTo(state));
        }

        [TestCase(50, "Tag din telefon ud af skabet og luk døren", false, StationControl.LadeskabState.Available)]
        public void StationControlRFIdLocked(int id, string output, bool islocked, StationControl.LadeskabState state)
        {
            _chargeControl.IsConnected().Returns(true);
            _rFidReader.RFIDReadEvent += Raise.EventWith<RFIDReadEventArgs>(new RFIDReadEventArgs() { ID = id });


            _rFidReader.RFIDReadEvent += Raise.EventWith<RFIDReadEventArgs>(new RFIDReadEventArgs() { ID = id });
            _chargeControl.Received().StopCharge();
            _door.Received().UnlockDoor();
            _logfile.Received().Log(islocked, id);
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
            Assert.That(_stationControl._state, Is.EqualTo(state));
        }
    }

}
