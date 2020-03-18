using System;

namespace Ladeskab.Moduls
{
    public class Door : IDoor
    {

        private char _state;
        
        public Door()
        {
            _state = 'u';
        }

        public void LockDoor()
        {
            System.Console.WriteLine("Door is locked");
            _state = 'l';
        }

        public void UnlockDoor()
        {
            System.Console.WriteLine("Door is unlocked");
            _state = 'u';
        }

        public event EventHandler<ClosedEventArgs> ClosedValueEvent;

        public char getState()
        {
            return _state;
        }

        public event EventHandler DoorClosed;

        protected virtual void OnDoorClosed(EventArgs e)
        {
            //Something
        }
    }
}