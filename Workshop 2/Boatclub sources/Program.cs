﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klubb
{
    class Program
    {
        static void Main(string[] args)
        {
            //Genom en instans av kontrollerobjektet kör vi igång programmer och fångar även som programmet ev. skulle kasta
            KlubbController piratKlubben = new KlubbController();
            try{ while(piratKlubben.RunProgram()); }
            catch (Exception ex) {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
