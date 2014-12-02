using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Klubbobjektet, huvudobjekt som innehåller 2 listor. en lista med alla medlemmar och en med alla båtar
namespace Klubb
{
    class Klubb
    {
        private List<Medlem> _medlemmar;
        private List<Bat> _batar;
        
        public Klubb(){
            _medlemmar = new List<Medlem>();
            _batar = new List<Bat>();
        }
        
        public List<Medlem> Medlemmar{
            get{return _medlemmar;}
            set { _medlemmar = value; }
        }

        public List<Bat> Batar{
            get{return _batar;}
            set { _batar = value; }
        }

        public void Add(Medlem medlem){
            _medlemmar.Add(medlem);
        }

        public void Add(Bat bat){
            _batar.Add(bat);
        }

    }
}
