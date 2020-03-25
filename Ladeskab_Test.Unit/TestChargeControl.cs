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

        [Test]
        public void HandleCurrentChangedUnderZeroThrowsError()
        {
            Assert.That(() =>
                    _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() { Current = -1.0 }),
                Throws.TypeOf<Exception>());
        }

        [TestCase(0.01)]
        [TestCase(2.5)]
        [TestCase(5.0)]
        public void HandleCurrentChangedBetweenZeroAndFive(double current)
        {
            _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() {Current = current});
            _display.Received().DisplayChargingMessage(Arg.Is<string>("Finished Charging!"));
        }

        [TestCase(5.01)]
        [TestCase(125)]
        [TestCase(375)]
        [TestCase(500)]
        public void HandleCurrentChangedBetweenFiveAndFiveHundred(double current)
        {
            _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() {Current = current});
            _display.Received().DisplayChargingMessage(Arg.Is<string>("Charging!"));
        }

        [TestCase(500.01)]
        [TestCase(945)]
        [TestCase(1500)]
        public void HandleCurrentChangedBetweenOverFiveHundred(double current)
        {
            _charger.CurrentValueEvent += Raise.EventWith<CurrentEventArgs>(new CurrentEventArgs() {Current = current});
            _display.Received().DisplayChargingMessage(Arg.Is<string>("An Error has occurred, charging has been stopped"));
            _charger.Received().StopCharge();
        }
    }
}
