using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Klubb
{
    class Klubbinfo
    {    
        //Visa menyvalet
        public int menyVal() {
            string str = "Båtklubben\n0. Avsluta\n\nRedigera\n1. Lägg till medlem\n2. Ta bort medlem\n3. Lägg till båt\n4. Ta bort båt\nVisa\n5. Visa kompakt medlemslista\n6. Visa fullständig medlemslista\n7. Redigera medlem\nAnge val (0-7): ";

            while(true) {
                int val;
                Console.Clear();
                Console.WriteLine(str);

                if (int.TryParse(Console.ReadLine(), out val)) {
                    if (val < 0 || val > 7) {
                        Console.WriteLine("Ange ett heltal mellan 0-7");
                        ContinueOnKeyPressed();
                    } else
                        return val;
                }
            }
        }
        
        //För att pausa och vänta låta användaren läsa
        public void ContinueOnKeyPressed() {
            Console.WriteLine("Tryck valfri tangent för att fortsätta..");
            Console.ReadKey();
        }
        //Skapa ny medlem
        public string[] nyMedlemsInfo() {
            string[] nyMedlem = new string[2];
            
            Console.Write("Förnamn på ny medlem: ");
            string namn = Console.ReadLine();
            if (!verifInputBokstaver(namn))
                return null;
                
            Console.Write("Personnummer på ny medlem i formatet(yymmddxxxx): ");
            string personnummer = Console.ReadLine();
            if (!verifInputPn(personnummer))
                return null;

            nyMedlem[0] = namn;
            nyMedlem[1] = personnummer;
            Console.WriteLine("\n"+namn + " är nu tillagd som medlem i klubben.\n");
            ContinueOnKeyPressed();
            return nyMedlem;        
        }

        public bool verifInputBokstaver(string s) {
            if (!Regex.IsMatch(s, @"^[a-zA-Z-]+$") || string.IsNullOrEmpty(s)) {
                Console.WriteLine("Siffror, tecken och mellanslag är otillåtet. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return false;
            } else if (s.Length < 2) {
                Console.WriteLine("Namnet måste vara på minst 3 bokstäver.");
                ContinueOnKeyPressed();
                return false;
            }
            return true;
        }

        public bool verifInputPn(string s) {
            if (!Regex.IsMatch(s, @"^[0-9]+$") || string.IsNullOrEmpty(s)) {
                Console.WriteLine("Får endast bestå av siffror. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return false;
            } else if (s.Length < 10) {
                Console.WriteLine("Ett personnummer består av 10 siffror, i formatet ÅÅMMDDXXXX.");
                ContinueOnKeyPressed();
                return false;
            }
            return true;
        }

        //Skapa ny båt
        public string[] nyBatInfo(Klubb klubb) {
            string[] nyBat = new string[3];
            Console.Write("Ange båttyp. 1. Segelbåt, 2. Motorseglare, 3. Motorbåt, 4. Kajak/Kanot, 5. Övrigt (1-5): ");
            string batTyp = Console.ReadLine();

            if (!Regex.IsMatch(batTyp, @"^[1-5]+$") || string.IsNullOrEmpty(batTyp)) {
                Console.WriteLine("Var vänlig ange ett nummer mellan 1-5. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return null;
            }

            Console.Write("Längd på båten i meter (t.ex 1,5): ");
            string batLangd = Console.ReadLine();
            if (!Regex.IsMatch(batLangd, @"^[0-9,]+$") || string.IsNullOrEmpty(batLangd)) {
                Console.WriteLine("Vänligen ange ett tal i meter med en decimal. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return null;                
            }
           
            ListaMedlemmar(klubb, "kompakt");
            Console.Write("Ange numret på ägaren: ");
            string medlemsIndex = Console.ReadLine();

            if (!Regex.IsMatch(medlemsIndex, @"^[1-" + (klubb.Medlemmar.Count) + "]+$") || string.IsNullOrEmpty(batLangd)) {
                Console.WriteLine("Vänligen ange bara heltal som är listat. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return null;
            } 

            nyBat[0] = batTyp;
            nyBat[1] = batLangd;
            nyBat[2] = medlemsIndex;
            Console.WriteLine("\nBåten är nu tillagd i båtlistan i klubben.\n");
            ContinueOnKeyPressed();
            return nyBat;
        }
        //Visa medlemslistan, kompakt eller full
        public void ListaMedlemmar(Klubb klubb, string Listtyp = null) {
            int counter = 1;
            if(Listtyp == "kompakt")
                foreach (Medlem medlem in klubb.Medlemmar) {
                    Console.WriteLine(counter + ". Namn: " + medlem.Namn + ". Medlemsnummer: " + medlem.Medlemsnummer + " Antal båtar: " + medlem.AntalBatar + ".");                   
                    counter++;
                }
            else if (Listtyp == "full")
                foreach (Medlem medlem in klubb.Medlemmar) {
                    Console.WriteLine(counter + ". Namn: " + medlem.Namn + " Personnummer: " + medlem.Personnummer + ". Medlemsnummer: " + medlem.Medlemsnummer + " båtar: ");
                    
                    //foreach (Bat bat in klubb.Batar) {
                    //    if (bat.Medlemsnummer == medlem.Medlemsnummer)
                    //        Console.WriteLine(bat.ToString());
                    //}           
                    ListaBatar(klubb, medlem.Medlemsnummer);
                    counter++;
                }
            else
                foreach (Medlem medlem in klubb.Medlemmar) {
                    Console.WriteLine(counter + ". " + medlem.ToString());
                    counter++;
                }
        }
        //Lista alla båtar
        public void ListaBatar(Klubb klubb, int medlemsNummer=0) {
            int counter = 1;
            string ret = "";
            foreach (Bat bat in klubb.Batar) {
                switch (int.Parse(bat.Typ)) {
                    case 1:
                        ret = counter + ". Typ: Segelbåt Längd: " + bat.Langd + "m Medlemsnummer: " + bat.Medlemsnummer;
                        break;
                    case 2:
                         ret = counter + ". Typ: Motorseglare Längd: " + bat.Langd + "m Medlemsnummer: " + bat.Medlemsnummer;
                        break;
                    case 3:
                         ret = counter + ". Typ: Motorbåt Längd: " + bat.Langd + "m Medlemsnummer: " + bat.Medlemsnummer;
                        break;
                    case 4:
                         ret = counter + ". Typ: Kanot Längd: " + bat.Langd + "m Medlemsnummer: " + bat.Medlemsnummer;
                        break;
                    case 5:
                        ret = counter + ". Typ: Övrigt Längd: " + bat.Langd + "m Medlemsnummer: " + bat.Medlemsnummer;
                        break;
                    default: break;
                }
                if (medlemsNummer == 0)
                    Console.WriteLine(ret);
                else if (bat.Medlemsnummer == medlemsNummer)
                    Console.WriteLine(ret);
                counter++;
            }
        }
        //Ta bort en båt
        public int taBortBat(Klubb klubb) {
            if (klubb.Batar.Count <= 0) {
                Console.WriteLine("Det finns inga båtar att ta bort.");
                ContinueOnKeyPressed();
                return 0;
            }
            int number = 1;
            //for (int i = 0; i < klubb.Batar.Count; i++) {
            //    Console.WriteLine("{0}. {1} med längd {2}, ägs av medlemsnummer: {3}", number, klubb.Batar[i].Typ, klubb.Batar[i].Langd, klubb.Batar[i].Medlemsnummer);
            //    number++;
            //}
            ListaBatar(klubb);
            Console.Write("\nAnge vilken båt du vill ta bort: ");

            int choice = int.Parse(Console.ReadLine());
            if (choice > klubb.Batar.Count || choice < 0) {
                throw new ArgumentOutOfRangeException();
            } else if (choice == 0) {
                return 0;
            } else {                
                Console.WriteLine("\n{0} har tagits bort från båtlistan.\n", klubb.Batar[choice - 1]);
                ContinueOnKeyPressed();
                return choice;
            }
        }
        //Ta bort en medlem
        public int taBortMedlem(Klubb klubb) {
            if (klubb.Medlemmar.Count <= 0) {
                Console.WriteLine("Det finns inga medlemmar att ta bort.");
                ContinueOnKeyPressed();
                return 0;
            }
            ListaMedlemmar(klubb, "kompakt");
            Console.Write("\nAnge vilken medlem du vill ta bort: ");
            string inData = Console.ReadLine();
            if (inData == "" || inData == null)
                return 0;
            int choice = int.Parse(inData);
            if (choice > klubb.Medlemmar.Count || choice < 0) {
                throw new ArgumentOutOfRangeException();
            } else if (choice == 0) {
                return 0;
            } else {
                Console.WriteLine("\n{0} har tagits bort från medlemslistan.\n", klubb.Medlemmar[choice - 1].Namn);
                //klubb.Medlemmar.RemoveAt(choice - 1);
                ContinueOnKeyPressed();
                return choice;
            }
        }

        public string[] redMedlemsInfo(Klubb klubb) {
            string[] medlem = new string[3];
            if (klubb.Medlemmar.Count <= 0) {
                Console.WriteLine("Det finns inga medlemmar att redigera.");
                ContinueOnKeyPressed();
            } else {
                ListaMedlemmar(klubb, "kompakt");
                Console.Write("\nAnge vilken medlem du vill redigera: ");
                medlem[2] = Console.ReadLine();
                int choice = int.Parse(medlem[2]);
                if (choice > klubb.Medlemmar.Count || choice < 0) {
                    throw new ArgumentOutOfRangeException();
                } else if (choice == 0) {
                    return medlem;
                } else {
                    Console.Write("Förnamn: ");
                    medlem[0] = Console.ReadLine();
                    if (!verifInputBokstaver(medlem[0]))
                        return medlem;

                    Console.Write("Personnummer i formatet(yymmddxxxx): ");
                    medlem[1] = Console.ReadLine();
                    if (!verifInputPn(medlem[1]))
                        return medlem;

                    Console.WriteLine("Medlemsinformationen är nu uppdaterad.");
                    ContinueOnKeyPressed();
                    return medlem;
                }
                
            }
            return medlem;
        }
    }
}
