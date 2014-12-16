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
        private int medlemsnummer;
        private long personnummer;
        private List<Bat> _batar = new List<Bat>();

        public Medlem(string n, long pn, int mn) {
            this.namn = n;
            this.personnummer = pn;
            this.medlemsnummer = mn;           
        }

        public int AntalBatar {
             get { return _batar.Count; } 
        }
                
        public void Add(Bat bat) {
            _batar.Add(bat);
        }

        public void Remove(Bat bat) {
            _batar.Remove(bat);
        }

        public List<Bat> Batar {
            get { return _batar; }
            set { _batar = value; }
        }

        public string Namn {
            get { return namn; }
            set { namn = value; }
        }

        public long Personnummer {
            get { return personnummer; }
            set { personnummer = value; }
        }

        public int Medlemsnummer {
            get { return medlemsnummer; }
            set { medlemsnummer = value; }
        }
                
        public override string ToString()
        {
            return ("Medlemsnummer: " + medlemsnummer + " Namn: " + namn + " Antal båtar: " + AntalBatar + " Personnummer: " + personnummer + ".");
        }
        

    }
    
}
