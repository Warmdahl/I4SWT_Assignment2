using System;
using System.IO;

namespace Ladeskab.Moduls
{
    public class LogFile : ILog
    {
        public void Log(bool islocked, int id)
        {
            ////lav fil
            var filename = "navn.txt";
            //var sw = File.AppendText(filename);
            
            //lav et string
            var now = DateTime.Now;
            var s = now + ", ";
            
            switch (islocked)
            {
                case true:
                    //laver streng når døren er låst
                    s = s + "locked by: " + id + ", door is locked";
                    Console.WriteLine("Does true work");
                    break;
                case false:
                    //laver streng når døren er låst op
                    s = s + "unlocked by: " + id + ", door is unlocked";
                    Console.WriteLine("Does false work");
                    break;
            }

            //skriv til fil
            //sw.WriteLine(s);

            //using (StreamWriter sw = new StreamWriter("test.txt"))
            //{
            //    if (islocked == true)
            //    {
            //        sw.AppendAllText(s);
            //        Console.WriteLine(s);
            //    }
            //    else
            //    {
            //        sw.WriteLine(s);
            //        Console.WriteLine(s);
            //    }
            //}
        }
    }
}