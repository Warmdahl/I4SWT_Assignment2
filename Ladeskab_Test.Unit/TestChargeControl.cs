using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Moduls;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace Ladeskab_Test.Unit
{
    class TestChargeControl
    {
        private IDisplay _display;
        private IUsbCharger _charger;
        private ChargeControl _uut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _charger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_display, _charger);
        }

        //Tester at charger kaster fejl når strømmen er under 0.
        [Test]
        public void HandleCurrentChangedUnderZeroThrowsError()
        {
            Assert.That(() =>
                    _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() { Current = -1.0 }),
                Throws.TypeOf<Exception>());
        }

        //Tester at display får "Finished Charging!" når strøm er mellem 0 og 5.
        [TestCase(0.01)]
        [TestCase(2.5)]
        [TestCase(5.0)]
        public void HandleCurrentChangedBetweenZeroAndFive(double current)
        {
            _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() {Current = current});
            _display.Received().DisplayChargingMessage(Arg.Is<string>("Finished Charging!"));
        }

        
        //Tester at display får beskeden "Charging!" når strømmen er mellem 5 og 500.
        [TestCase(5.01)]
        [TestCase(125)]
        [TestCase(375)]
        [TestCase(500)]
        public void HandleCurrentChangedBetweenFiveAndFiveHundred(double current)
        {
            _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() {Current = current});
            _display.Received().DisplayChargingMessage(Arg.Is<string>("Charging!"));
        }

        
        //Tester at display får beskeden "An error has occured, charging has been stopped" når strømmen er over 500
        [TestCase(500.01)]
        [TestCase(945)]
        [TestCase(1500)]
        public void HandleCurrentChangedBetweenOverFiveHundred(double current)
        {
            _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() {Current = current});
            _display.Received().DisplayChargingMessage(Arg.Is<string>("An Error has occurred, charging has been stopped"));
            _charger.Received().StopCharge();
        }

        
        //Tester at chargecontrol ved at chargeren er tislsuttet og frakoblet.
        [TestCase(true)]
        [TestCase(false)]
        public void isconnected(bool con)
        {
            _charger.Connected.Returns(con);
            Assert.That(_uut.IsConnected, Is.EqualTo(con));
        }

        //Tester om laderen beynder at lade.
        [Test]
        public void ChargeStart()
        {
            _uut.StartCharge();
            _charger.Received().StartCharge();
        }

        //Tester om laderen stopper med at lade.
        [Test]
        public void ChargeStop()
        {
            _uut.StopCharge();
            _charger.Received().StopCharge();
        }
    }
}
