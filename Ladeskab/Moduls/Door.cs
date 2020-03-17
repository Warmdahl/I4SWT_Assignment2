namespace Ladeskab.Moduls
{
    public class Door : IDoor
    {

        private char _state;
        
        public Door()
        {
        }

        public void Lock()
        {
            System.Console.WriteLine("Door is locked");
            _state = 'l';
        }

        public void unLock()
        {
            System.Console.WriteLine("Door is unlocked");
            _state = 'u';
        }

        public char getState()
        {
            return _state;
        }
    }
}