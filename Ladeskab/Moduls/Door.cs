using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Moduls
{
    public class Door : IDoor
    {

        public bool Islocked { get; set; }

        public event EventHandler<ChangedEventArgs> ChangedValueEvent;

        public Door()
        {
            Islocked = false;
        }

        public void LockDoor()
        {
            Islocked = true;
            Console.WriteLine("Door is Locked");
        }

        public void UnlockDoor()
        {
            Islocked = false;
            System.Console.WriteLine("Door is Unlocked");
        }

        public void SimulateDoorOpen()
        {
            OnDoorChanged(new ChangedEventArgs() {DoorState = true});
        }

        public void SimulateDoorClose()
        {
            OnDoorChanged(new ChangedEventArgs() {DoorState = false});
        }

        protected virtual void OnDoorChanged(ChangedEventArgs e)
        {
            ChangedValueEvent?.Invoke(this, e); //sending an instance of the data to all connected observers
        }
    }
}