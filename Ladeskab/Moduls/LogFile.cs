using System;
using System.IO;

namespace Ladeskab.Moduls
{
    public class LogFile : ILog
    {
        public void Log(bool islocked, int id)
        {
            //lav fil
            string filename = "navn.txt";
            StreamWriter sw = File.AppendText(filename);
            
            //lav et streng
            DateTime now = DateTime.Now;
            string s = now+", ";
            
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