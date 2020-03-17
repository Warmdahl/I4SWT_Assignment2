using UsbSimulator;

namespace Ladeskab.Moduls
{
    public class USBCharger : IUsbCharger
    {
        public double CurrentValue { get; }

        public bool Connected { get; }

        public void StartCharge()
        {
            throw new System.NotImplementedException();
        }

        public void StopCharge()
        {
            throw new System.NotImplementedException();
        }
    }
}