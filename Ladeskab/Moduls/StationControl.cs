using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Moduls;
using NSubstitute;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };
        
        private IChargeControl _chargercontrol;
        private IUsbCharger _usbCharger;
        private IRFidReader _rfidreader;
        private IDisplay _display;
        private ILog _log;
        private IDoor _door;
        
        public LadeskabState _state { get; private set; }
        public int _oldId { get; private set; }
        

        // Her mangler constructor
        public StationControl(IDoor door, IChargeControl chagercontrol, IRFidReader rfidReader, IDisplay display, ILog log, IUsbCharger usbcharger)
        {
            _door = door;
            _chargercontrol = chagercontrol;
            _rfidreader = rfidReader;
            _display = display;
            _log = log;
            _state = LadeskabState.Available;
            _usbCharger = usbcharger;
            _door.ChangedValueEvent += handleDoorState;
            _rfidreader.RFIDReadEvent += RfidDetected;
        }


        private void handleDoorState(object o, ChangedEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:    //state=DoorOpen
                    if (!e.DoorState)
                    {
                        _state = LadeskabState.Available;
                        _display.DisplayUserInstructions("Indlæs RFID");
                    }
                    else
                    {
                        _display.DisplayUserInstructions("Dør åben, tilslut telefon");
                    }
                    break;
                case LadeskabState.Available:    //state=Available
                    if (e.DoorState)
                    {
                        _state = LadeskabState.DoorOpen;
                        _display.DisplayUserInstructions("Dør åben, tilslut telefon");
                    }
                    else
                    {
                        _display.DisplayUserInstructions("Indlæs RFID");
                    }
                    break;
                //case LadeskabState.Locked:    //state=locked
                //    _display.DisplayChargingMessage("Charging...");
                //    break;
                
            }
        }
        
        
        
        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object o, RFIDReadEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargercontrol.IsConnected())
                    {
                        _door.LockDoor();
                        _chargercontrol.StartCharge();
                        _oldId = e.ID;
                        _log.Log(true, _oldId);
                        _display.DisplayUserInstructions("Ladeskab optaget");
                        _state = LadeskabState.Locked;
                        
                    }
                    else
                    {
                        _display.DisplayUserInstructions("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.ID == _oldId)
                    {
                        _chargercontrol.StopCharge();
                        _door.UnlockDoor();
                        _log.Log(false, _oldId);

                        _display.DisplayUserInstructions("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayUserInstructions("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
