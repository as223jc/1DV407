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
        //Instans av klubbinfo, view
        Klubbinfo klubbInfo = new Klubbinfo();
        private string path_ = "databas.txt";
        
        //Ladda in från fil (path)
        //läs in varje rad och skapa nya objekt att stoppa in i klubbobjektet
        public Klubb LaddaAlltDB(Klubb klubb)
        {            
            DBReadEnum status = DBReadEnum.None;
            using (StreamReader reader = new StreamReader(path_))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "[Medlemmar]")
                    {
                        status = DBReadEnum.Medlem;
                    }
                    else if (line == "[Batar]")
                    {
                        status = DBReadEnum.Bat;
                    }
                    else
                    {
                        if (status == DBReadEnum.Medlem) {
                            string[] medlem = line.Split(';');

                            if (medlem.Length != 4)
                                throw new ArgumentException("Fel vid inläsning av medlemmar.");

                            string Namn = medlem[0];
                            string pn = medlem[1];
                            int Medlemsnummer = int.Parse(medlem[2]);
                            int antalBatar = int.Parse(medlem[3]);
                           
                            Medlem nyMedlem = new Medlem(Namn, pn, Medlemsnummer, antalBatar);

                            klubb.Add(nyMedlem);
                        } else if (status == DBReadEnum.Bat) {
                            string[] bat = line.Split(';');

                            if (bat.Length != 3)
                                throw new ArgumentException("Fel vid inläsning av båtar.");

                            string Typ = bat[0];
                            string Langd = bat[1];
                            int MedlemsNummer = int.Parse(bat[2]);

                            Bat baten = new Bat(Typ, Langd, MedlemsNummer);
                            klubb.Add(baten);
                        }
                        else
                        {
                            Console.WriteLine("Fel vid inläsning");
                        }
                    }
                }                
                return klubb;
            }            
        }
        //Spara all data till FIL
        public void SparaAlltDB(Klubb klubb)
        {
            using (StreamWriter sw = new StreamWriter(path_))
            {
                sw.WriteLine("[Medlemmar]");
                foreach (Medlem medlem in klubb.Medlemmar)
                {
                    sw.WriteLine(medlem.Namn + ";" + medlem.Personnummer + ";" + medlem.Medlemsnummer + ";" + medlem.AntalBatar);
                }
                sw.WriteLine("[Batar]");
                foreach (Bat bat in klubb.Batar)
                {
                    sw.WriteLine(bat.Typ + ";" + bat.Langd + ";" + bat.Medlemsnummer);
                }
            }
            
        }
        //lista alla medlemmar, kompakt, full lista eller vanlig tostring(definieras i objektet)
        public void listaMedlemmar(Klubb klubb, string typ=null) {
            if(typ=="full")
                klubbInfo.ListaMedlemmar(klubb, "full");
            else if(typ=="kompakt")
                klubbInfo.ListaMedlemmar(klubb, "kompakt");
            else
            klubbInfo.ListaMedlemmar(klubb);
            klubbInfo.ContinueOnKeyPressed();
        }
                
        //Ta bårt båt
        public bool tabortBat(Klubb klubb) {
           int batIndex = klubbInfo.taBortBat(klubb);
           if (batIndex == 0)
               return false;

           foreach (Medlem m in klubb.Medlemmar) {
               if (m.Medlemsnummer == klubb.Batar[batIndex-1].Medlemsnummer)
                   m.AntalBatar--;
           }

           klubb.Batar.RemoveAt(batIndex -1);
           SparaAlltDB(klubb);

           return true;
        }

        //Lägga till båt
        public bool nyBat(Klubb klubb) {
            string[] nyBat = klubbInfo.nyBatInfo(klubb);
            if (nyBat == null)
                return false;

            int mNummer = klubb.Medlemmar[int.Parse(nyBat[2])-1].Medlemsnummer;

            foreach (Medlem m in klubb.Medlemmar) {               
                if (m.Medlemsnummer == mNummer)
                    m.AntalBatar++;
            }

            Bat bat = new Bat(nyBat[0], nyBat[1], mNummer);
            klubb.Batar.Add(bat);

            SparaAlltDB(klubb);

            return true;
        }

        //Ta bort medlem
        public bool tabortMedlem(Klubb klubb) {
            int mIndex = klubbInfo.taBortMedlem(klubb);
            if (mIndex == 0)
                return false;

            klubb.Medlemmar.RemoveAt(mIndex-1);

            SparaAlltDB(klubb);

            return true;
        }
        //Lägga till medlem
        public bool nyMedlem(Klubb klubb) {
            string[] nyMedlem = klubbInfo.nyMedlemsInfo();
            if (nyMedlem[0] == null || nyMedlem[0] == "" || nyMedlem[1] == null || nyMedlem[1] == "")
                return false;
            Console.Write(nyMedlem[0]);
            Random rnd = new Random();
            int medlemsNummer = rnd.Next(1, 64395);

            Medlem medlem = new Medlem(nyMedlem[0], nyMedlem[1], medlemsNummer);

            klubb.Medlemmar.Add(medlem);

            SparaAlltDB(klubb);

            return true;
        }

        //Redigera medlem
        public bool redigeraMedlem(Klubb klubb) {
            string[] redigeradMedlem = klubbInfo.redMedlemsInfo(klubb);
            if (redigeradMedlem[0] == null || redigeradMedlem[0] == "" || redigeradMedlem[1] == null || redigeradMedlem[1] == "")
                return false;

            klubb.Medlemmar[int.Parse(redigeradMedlem[2]) -1].Namn = redigeradMedlem[0];
            klubb.Medlemmar[int.Parse(redigeradMedlem[2]) -1].Personnummer = redigeradMedlem[1];           

            SparaAlltDB(klubb);           

            return true;
        }

    }
}
