using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klubb
{
    class Program
    {
        static Klubb klubb = new Klubb();
        static KlubbController piratKlubben = new KlubbController(klubb);
        static void Main(string[] args)
        {
            //Genom en instans av kontrollerobjektet kör vi igång programmer och fångar även som programmet ev. skulle kasta
            try
            {
                piratKlubben.RunProgram();

                Console.WriteLine("Closing program.. Press any key to exit");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }

    }
}
