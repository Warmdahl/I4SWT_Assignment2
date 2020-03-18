using System;

namespace Ladeskab.Moduls
{

    public class ClosedEventArgs : EventArgs
    {
        private bool Closed { get; set; }
    }
    public interface IDoor
    {

        event EventHandler<ClosedEventArgs> ClosedValueEvent;
        
        //Tells if door is locked or unlocked
        char getState();
        
        //locks the door
        void LockDoor();
        
        //unlock the door
        void UnlockDoor();

    }
}