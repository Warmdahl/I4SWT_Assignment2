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

            // Simulering
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


            //bool finish = false;
            //do
            //{
            //    string input;
            //    System.Console.WriteLine("Indtast E, O, C, R: ");
            //    input = Console.ReadLine();
            //    if (string.IsNullOrEmpty(input)) continue;

            //    switch (input[0])
            //    {
            //        case 'E':
            //            finish = true;
            //            break;

            //        case 'O':
            //            door.OnDoorOpen();
            //            break;

            //        case 'C':
            //            door.OnDoorClose();
            //            break;

            //        case 'R':
            //            System.Console.WriteLine("Indtast RFID id: ");
            //            string idString = System.Console.ReadLine();

            //            int id = Convert.ToInt32(idString);
            //            rfidReader.OnRfidRead(id);
            //            break;

            //        default:
            //            break;
            //    }

            //} while (!finish);
        }
    }
}