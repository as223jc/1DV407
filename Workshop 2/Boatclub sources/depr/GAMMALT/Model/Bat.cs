using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klubb
{
    //Båtklassen 
    public class Bat
    {
        private string typ;
        
        public string Typ {
            get { return typ; }
            set { typ = value; }
        }
        private string langd;

        public string Langd {
            get { return langd; }
            set { langd = value; }
        }
        private int medlemsnummer;

        public int Medlemsnummer {
            get { return medlemsnummer; }
            set { Medlemsnummer = value; }
        }

        public Bat(string t, string l, int m) {
            this.typ = t;
            this.langd = l;
            this.medlemsnummer = m;
        }   
               
        public override string ToString()
        {
            return ("Typ av båt: " + typ + ". Längd: " + Langd + "m Medlemsnummer: " + Medlemsnummer);
        }
    }
    
}
