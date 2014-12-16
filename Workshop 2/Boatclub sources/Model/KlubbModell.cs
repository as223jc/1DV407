using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klubb
{
    class KlubbModell
    {        
        public List<Medlem> MedlemsLista = new List<Medlem>();
        private List<int> MedlemsNummer = new List<int>();
        private string path_ = "databas.txt";
        
        public KlubbModell() {
            LaddaAlltDB();
        }
        
        //Lägga till medlem
        public void nyMedlem(string[] m) {
            if (m == null)
                return;
            MedlemsLista.Add(new Medlem(m[0], long.Parse(m[1]), randUniqNumber()));
            SparaAlltDB();
        }

        //Ta bort medlem
        public bool tabortMedlem(Medlem m) {
            if (m == null)
                return false;
            MedlemsLista.Remove(m);
            SparaAlltDB();

            return true;
        }

        public bool nyBat(string[] nyBat) {
            if (nyBat == null)
                return false;
            foreach (Medlem m in MedlemsLista) {
                if (m.Medlemsnummer == int.Parse(nyBat[2]))
                    m.Batar.Add(new Bat(nyBat[0], nyBat[1]));               
            }
            
            SparaAlltDB();

            return true;
        }

        //Ta bort båt
        public bool tabortBat(Bat bat) {
            if (bat == null)
                return false;
            foreach (Medlem m in MedlemsLista)
                m.Remove(bat);
            SparaAlltDB();
            return true;
        }

        //Uppdatera båtinformation
        public bool uppdateraBat(Bat bat, string[] info) {
            if (bat == null || (info[0] == "" || info[1] == ""))
                return false;
            bat.Typ = info[0];  
            bat.Langd = info[1];
            SparaAlltDB();
            return true;
        }


        //Ladda in från fil (path)
        //läs in varje rad och skapa nya objekt att stoppa in i klubbobjektet
        public void LaddaAlltDB() {            
            DBReadEnum status = DBReadEnum.None;
            using (StreamReader reader = new StreamReader(path_)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "[Medlemmar]") {
                        status = DBReadEnum.Medlem;
                    }
                    else if (line == "[Batar]") {
                        status = DBReadEnum.Bat;
                    } else {
                        if (status == DBReadEnum.Medlem) {
                            string[] medlem = line.Split(';');

                            if (medlem.Length != 4)
                                throw new ArgumentException("Fel vid inläsning av medlemmar.");
                            
                            MedlemsLista.Add(new Medlem(medlem[0], long.Parse(medlem[1]), int.Parse(medlem[2])));
                        } else if (status == DBReadEnum.Bat) {
                            string[] bat = line.Split(';');

                            if (bat.Length != 3)
                                throw new ArgumentException("Fel vid inläsning av båtar.");

                            //Lägg till båt till medlem
                            foreach(Medlem m in MedlemsLista){
                                if(m.Medlemsnummer == int.Parse(bat[2])){
                                    m.Add(new Bat(bat[0], bat[1]));
                                }
                            }
                        }
                        else {
                            Console.WriteLine("Fel vid inläsning");
                        }
                    }
                }                
            }            
        }
        //Spara all data till FIL
        public void SparaAlltDB() {
            using (StreamWriter sw = new StreamWriter(path_)) {
                sw.WriteLine("[Medlemmar]");
                foreach (Medlem m in MedlemsLista) {
                    sw.WriteLine(m.Namn + ";" + m.Personnummer + ";" + m.Medlemsnummer + ";" + m.AntalBatar);
                }
                sw.WriteLine("[Batar]");
                foreach (Medlem m in MedlemsLista) {
                    foreach (Bat bat in m.Batar) {
                        sw.WriteLine(bat.Typ + ";" + bat.Langd + ";" + m.Medlemsnummer);
                    }
                }
            }            
        }

        //Skapa ett random nummer som inte finns redan, medlemsnummer
        public int randUniqNumber() {
            Random rnd = new Random();
            while (true) {
                int i = rnd.Next(1, 64395);
                if (!MedlemsNummer.Contains(i)) {
                    MedlemsNummer.Add(i);
                    return i;
                }
            }
        }

        
    }
}
