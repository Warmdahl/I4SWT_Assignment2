using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab.Moduls;
using UsbSimulator;

namespace Ladeskab
{
    class Program
    {
        static void Main(string[] args)
        {   
            // Assemble your system here from all the classes
            Display _display = new Display();
            LogFile _logFile = new LogFile();
            UsbChargerSimulator _usbChargerSimulator = new UsbChargerSimulator();
            ChargeControl _chargeControl = new ChargeControl(_display,_usbChargerSimulator);
            RFidReader _rFreader = new RFidReader();
            Door _door = new Door();
            StationControl _stationControl = new StationControl(_door, _chargeControl,_rFreader,_display,_logFile,_usbChargerSimulator);

            // Test af program på console window
            _door.SimulateDoorOpen();
            _usbChargerSimulator.SimulateConnected(true);
            _door.SimulateDoorClose();
            _door.SimulateDoorOpen();
            _rFreader.SimulateReadRFID(50);
            _door.SimulateDoorClose();
            _rFreader.SimulateReadRFID(50);
            _rFreader.SimulateReadRFID(25);
            _rFreader.SimulateReadRFID(125);
            _rFreader.SimulateReadRFID(50);
            Thread.Sleep(2000);
            _usbChargerSimulator.SimulateConnected(false);
            _door.SimulateDoorClose();
        }
    }
}