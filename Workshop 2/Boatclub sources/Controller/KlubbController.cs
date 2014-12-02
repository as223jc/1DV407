using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klubb
{
    class KlubbController {
        //Kontruktorn som tillsätter klubbobjektet med parametern och laddar från fil
        public KlubbController(Klubb klubb) {
            klubben = klubb;
            klubben = klubbModell.LaddaAlltDB(klubben);
        }

        private Klubb klubben;
        private KlubbModell klubbModell = new KlubbModell();
        private Klubbinfo klubbVy = new Klubbinfo();
        
        //Huvudloopen som körs sålänge användaren skriver en nolla
        public void RunProgram(){           
            while (menyAlternativ()) ;
        }

        public bool menyAlternativ() {
            switch (klubbVy.menyVal()) {
                case 0:
                    return false;                    
                case 1:
                    klubbModell.nyMedlem(this.klubben);
                    break;
                case 2:
                    klubbModell.tabortMedlem(this.klubben);
                    break;
                case 3:
                    klubbModell.nyBat(this.klubben);
                    break;
                case 4:
                    klubbModell.tabortBat(this.klubben);
                    break;
                case 5:
                    klubbModell.listaMedlemmar(this.klubben, "kompakt");
                    break;
                case 6:
                    klubbModell.listaMedlemmar(this.klubben, "full");
                    break;
                case 7:
                    klubbModell.redigeraMedlem(this.klubben);
                    break;      
                default:
                    break;
            }
            return true;

        }     
    }
}
