using System;

namespace Ladeskab.Moduls
{

    public class ChangedEventArgs : EventArgs
    {
        public bool DoorState { get; set; }
    }
    public interface IDoor
    {

        event EventHandler<ChangedEventArgs> ChangedValueEvent;

        //Tells if door is locked or unlocked
        //char getState();
        
        //locks the door
        void LockDoor();
        
        //unlock the door
        void UnlockDoor();

    }
}