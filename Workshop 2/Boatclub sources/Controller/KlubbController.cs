using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klubb
{
    class KlubbController {

        private Klubbinfo klubbVy = new Klubbinfo();
        private KlubbModell klubbMod = new KlubbModell();          
       
        //Huvudloopen som körs sålänge användaren skriver en nolla
        public bool RunProgram(){
            klubbVy.showMenu();
            switch (klubbVy.getInput()) {
                case 0: //Avsluta
                    return false;
                case 1://Skapa ny medlem
                    klubbMod.nyMedlem(klubbVy.nyMedlemsInfo());
                    break;
                case 2://Ta bort medlem
                    klubbMod.tabortMedlem(klubbVy.taBortMedlem(klubbMod.MedlemsLista));
                    break;
                case 3://Skapa ny båt
                    klubbMod.nyBat(klubbVy.nyBatInfo(klubbMod.MedlemsLista));
                    break;
                case 4://Ta bort båt    
                    klubbMod.tabortBat(klubbVy.taBortBat(klubbMod.MedlemsLista));
                    break;
                case 5://Redigera en medlem
                    //klubbMod.redigeraMedlem(klubbVy.redMedlemsInfo(klubbMod.MedlemsLista));
                    klubbVy.redMedlemsInfo(klubbMod.MedlemsLista);
                    break;
                case 6://Redigera en båt
                    andraBat();
                    break;
                case 7://Visa en kompakt medlemslista
                    klubbVy.kompaktLista(klubbMod.MedlemsLista);
                    klubbVy.ContinueOnKeyPressed();
                    break;
                case 8://Visa en full medlemslista
                    klubbVy.fullLista(klubbMod.MedlemsLista);
                    klubbVy.ContinueOnKeyPressed();
                    break;                
                default://Om annat har angetts loopar vi helt enkelt om
                    break;
            }
            return true;
        }

        public void andraBat() {
            int antalBatar = klubbVy.ListaBatar(klubbMod.MedlemsLista);
            if (antalBatar <= 0)
                return;   
            Bat bat = klubbVy.redBat(klubbMod.MedlemsLista, antalBatar);
            if (bat == null)
                return;   
            string[] batInfo = klubbVy.redBatInfo(antalBatar);
            if(batInfo == null)
                return;   
            klubbMod.uppdateraBat(bat, batInfo);            
        }

                  
    }
}
