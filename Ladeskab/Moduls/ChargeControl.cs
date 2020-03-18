using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab.Moduls
{
    public class ChargeControl : IChargeControl
    {
        public bool IsCharging { get; private set; }
        private IDisplay _display;
        private IUsbCharger _charger;

        public ChargeControl(IDisplay display, IUsbCharger charger)
        {
            _display = display;
            _charger = charger;
            _charger.CurrentValueEvent += HandleCurrentValueChanged;
            IsCharging = false;
        }

        public bool IsConnected()
        {
            return _charger.Connected;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
            IsCharging = false;
        }

        private void HandleCurrentValueChanged(object o, CurrentEventArgs e)
        {
            double current = e.Current;

            if (current < 0)
            {
                
            }
            else if (current <= 5)
            {
                _display.DisplayChargingMessage("Finished Charging!");
            }
            else if (current <= 500)
            {
                if (IsCharging == false)
                {
                    _display.DisplayChargingMessage("Charging!");
                    IsCharging = true;
                }

            }
            else //current above 500
            {
                _charger.StopCharge();
                IsCharging = false;
                _display.DisplayChargingMessage("An Error has occurred, charging has been stopped");
            }
        }
    }
}
