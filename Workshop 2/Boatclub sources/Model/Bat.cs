﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klubb
{
    //Båtklassen 
    public class Bat
    {
        private string typ;
        private string langd;

        public Bat(string t, string l) {
            this.typ = t;
            this.langd = l;
        }   
        
        public string Typ {
            get { return typ; }
            set { typ = value; }
        }

        public string Langd {
            get { return langd; }
            set { langd = value; }
        }
                                      
        public override string ToString()
        {
            return ("Typ av båt: " + (BoatType)int.Parse(Typ) + ". Längd: " + Langd + "m");
        }
    }
    
}
