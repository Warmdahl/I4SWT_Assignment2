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
        public void LadeskabIsAvailable()
        {
            Assert.That(_stationControl._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }
    }
}
