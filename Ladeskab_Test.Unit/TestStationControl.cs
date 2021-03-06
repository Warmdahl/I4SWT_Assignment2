﻿using System;
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

        //Test af om ladeskab går som available fra start af.
        [Test]
        public void StationControlIsAvailable()
        {
            Assert.That(_stationControl._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        //Tester om station control kan modtage events fra door.
        [Test]
        public void StationControlWorksWithDoorEvents()
        {
            _door.Received().ChangedValueEvent += Arg.Any<EventHandler<ChangedEventArgs>>();
        }

        //Tester om station control kan modtage events fra RFId
        [Test]
        public void StationControlWorksWithRFidEvents()
        {
            _rFidReader.Received().RFIDReadEvent += Arg.Any<EventHandler<RFIDReadEventArgs>>();
        }

        //Tester om Station Control håndter den switch case for handledoorstate ladeskab.DoorOpen korrekt
        [TestCase(true, "Dør åben, tilslut telefon", StationControl.LadeskabState.DoorOpen)]
        [TestCase(false, "Indlæs RFID", StationControl.LadeskabState.Available)]
        public void StationControlHandleDoorstateDoorOpen(bool open, string output, StationControl.LadeskabState state)
        {
            _door.ChangedValueEvent += Raise.EventWith<ChangedEventArgs>(new ChangedEventArgs() {DoorState = true});
            _door.ChangedValueEvent += Raise.EventWith<ChangedEventArgs>(new ChangedEventArgs() {DoorState = open});
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
            Assert.That(_stationControl._state, Is.EqualTo(state));
        }

        //Tester om Station Control håndtere den switch case for handledoorstate ladeskab.Available korrekt
        [TestCase(false, "Indlæs RFID", StationControl.LadeskabState.Available)]
        [TestCase(true, "Dør åben, tilslut telefon", StationControl.LadeskabState.DoorOpen)]
        public void StationControlHandleDoorstateAvailable(bool open, string output, StationControl.LadeskabState state)
        {
            _door.ChangedValueEvent += Raise.EventWith<ChangedEventArgs>(new ChangedEventArgs() {DoorState = open});
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
            Assert.That(_stationControl._state, Is.EqualTo(state));
        }

        //Tester om Station Control håndtere den switch case for RFidDeteced ladeskab.availble og er connected korrekt
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

        //Tester om Station Control håndtere den switch case for RFidDeteced ladeskab.availble og ikke er connected korrekt
        [TestCase(50,"Din telefon er ikke ordentlig tilsluttet. Prøv igen.")]
        public void StationControlRFIdAvailableConnectedFail(int id, string output)
        {
            _chargeControl.IsConnected().Returns(false);
            _rFidReader.RFIDReadEvent += Raise.EventWith<RFIDReadEventArgs>(new RFIDReadEventArgs() { ID = id });
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
        }

        //Tester om Station Control håndtere den switch case for RFidDeteced ladeskab.locked og er connected korrekt
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

        //Tester om Station Control håndtere den switch case for RFidDeteced ladeskab.availble og ikke er connected korrekt
        [TestCase(50,100, "Forkert RFID tag")]
        public void StationControlRfIdLockedFail(int id,int wrongid,string output)
        {
            _chargeControl.IsConnected().Returns(true);
            _rFidReader.RFIDReadEvent += Raise.EventWith<RFIDReadEventArgs>(new RFIDReadEventArgs() { ID = id });


            _rFidReader.RFIDReadEvent += Raise.EventWith<RFIDReadEventArgs>(new RFIDReadEventArgs() { ID = wrongid });
            _display.Received().DisplayUserInstructions(Arg.Is<string>(output));
        }
    }

}
