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

            //lav et string
            var now = DateTime.Now;
            var s = now + ", ";
            
            switch (islocked)
            {
                case true:
                    //laver streng når døren er låst
                    s = s + "locked by: " + id + ", door is locked";
                    break;
                case false:
                    //laver streng når døren er låst op
                    s = s + "unlocked by: " + id + ", door is unlocked";
                    break;
            }
            
            //skriv til fil
            using (StreamWriter stw = File.AppendText(filename))
            {
                stw.WriteLine(s);
            }
        }
    }
}