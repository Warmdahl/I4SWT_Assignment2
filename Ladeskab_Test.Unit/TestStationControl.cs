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
            _stationControl = new StationControl(_door,_chargeControl,_rFidReader,_display,_logfile,_usbCharger);
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
            _door.ChangedValueEvent += Raise.EventWith<ChangedEventArgs>(new ChangedEventArgs() { DoorState = open });
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
            Assert.That(_stationControl._state, Is.EqualTo(state));
        }
    }
}
