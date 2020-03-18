using System;
using System.IO;

namespace Ladeskab.Moduls
{
    public class LogFile : ILog
    {
        public void Log(bool islocked, int id)
        {
            //lav fil
            var filename = "navn.txt";
            var sw = File.AppendText(filename);
            
            //lav et string
            var now = DateTime.Now;
            var s = now + ", ";
            
            switch (islocked)
            {
                case true:

                    s = s + "locked by" + id + ", door is locked:";
                    break;
                case false:

                    s = s + "unlocked by" + id + ", door is unlocked:";
                    break;
            }
            
            //skriv til fil
            sw.WriteLine(s);
        }
    }
}