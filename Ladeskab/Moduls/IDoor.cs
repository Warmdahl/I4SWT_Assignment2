namespace Ladeskab.Moduls
{
    public interface IDoor
    {
        //Tells if door is locked or unlocked
        char getState();
        
        //locks the door
        void Lock();
        
        //unlock the door
        void unLock();

    }
}