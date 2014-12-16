using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Medlemsklassen
namespace Klubb
{
    public class Medlem
    {
        private string namn;

        public string Namn {
            get { return namn; }
            set { namn = value; }
        }
        private string personnummer;

        public string Personnummer {
            get { return personnummer; }
            set { personnummer = value; }
        }
        private int medlemsnummer;

        public int Medlemsnummer {
            get { return medlemsnummer; }
            set { medlemsnummer = value; }
        }

        private int antalBatar;

        public int AntalBatar {
            get { return antalBatar; }
            set { antalBatar = value; }
        }

        public Medlem(string n, string pn, int mn, int ab = 0) {
            this.namn = n;
            this.personnummer = pn;
            this.medlemsnummer = mn;
            this.antalBatar = ab;
        }  
        public override string ToString()
        {
            return ("Medlemsnummer: " + medlemsnummer + " Namn: " + namn + " Antal båtar: " + antalBatar + " Personnummer: " + personnummer + ".");
        }
    }
    
}
