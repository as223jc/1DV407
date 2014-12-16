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
        public void showMenu() {
            Console.Clear();
            Console.WriteLine("Båtklubben\n0. Avsluta\n\nRedigera\n1. Lägg till medlem\n2. Ta bort medlem\n3. Lägg till båt\n4. Ta bort båt\n5. Redigera medlem\n6. Redigera båt\nVisa\n7. Visa kompakt medlemslista\n8. Visa fullständig medlemslista\nAnge val (0-7): ");
        }
        
        public int getInput() {
             int val;                
                if (int.TryParse(Console.ReadLine(), out val)) {
                    if (val < 0 || val > 7) {
                        Console.WriteLine("Ange ett heltal mellan 0-7");
                    } else {
                        return val;
                    }
                } else {
                    Console.WriteLine("Ange bara siffror");                
                }
                ContinueOnKeyPressed();
                return -1;
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
            Console.WriteLine("\n" + namn + " är nu tillagd som medlem i klubben.\n");
            ContinueOnKeyPressed();
            return nyMedlem;
        }

        //Ta bort en medlem
        public Medlem taBortMedlem(List<Medlem> medlemmar) {
            int choice = 0;
            int res;

            if (medlemmar.Count <= 0) {
                Console.WriteLine("Det finns inga medlemmar att ta bort.");
            } else {
                kompaktLista(medlemmar, true);
                Console.Write("\nAnge vilken medlem du vill ta bort: ");

                string indata = Console.ReadLine();
                int.TryParse(indata, out res);                
                choice = verifyInput(res, 1, medlemmar.Count);

                if (choice != 0) {
                    Console.WriteLine("\n{0} har tagits bort från medlemslistan.\n", medlemmar[choice - 1].Namn);
                } else {
                    return null;
                }
            }
            ContinueOnKeyPressed();
            return medlemmar[choice-1];
        }

        //Ändra en medlem
        public void redMedlemsInfo(List<Medlem> medlemmar) {
            int choice = 0;
            int res;
            string[] medlem = new string[3];
            string err = "";

            if (medlemmar.Count <= 0) {
                Console.WriteLine("Det finns inga medlemmar att redigera.");
            } else {
                kompaktLista(medlemmar, true);
                Console.Write("\nAnge vilken medlem du vill redigera: ");

                string indata = Console.ReadLine();
                int.TryParse(indata, out res);
                choice = verifyInput(res, 1, medlemmar.Count);

                if (choice != 0) {
                    Console.Write("Förnamn: ");
                    medlem[0] = Console.ReadLine();
                    
                    if (!verifInputBokstaver(medlem[0]))
                        err += "Fel! Du måste ange bokstäver\n";
                    else {
                        Console.Write("Personnummer i formatet(yymmddxxxx): ");
                        medlem[1] = Console.ReadLine();                        
                        if (!verifInputPn(medlem[1]))
                            err += "Fel! Du måste ange siffror\n";
                    }
                    if (err == "") {
                        Console.WriteLine("Medlemsinformationen är nu uppdaterad.");
                        medlemmar[choice - 1].Namn = medlem[0];
                        medlemmar[choice - 1].Personnummer = long.Parse(medlem[1]);
                    } else
                        Console.WriteLine(err);
                } else {
                    //return null;
                }
            }
            ContinueOnKeyPressed();
            //return medlem;
        }

        //Skapa ny båt
        public string[] nyBatInfo(List<Medlem> medlemmar) {
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

            kompaktLista(medlemmar, true);
            Console.Write("Ange numret på ägaren: ");
            string medlemsIndex = Console.ReadLine();

            if (!Regex.IsMatch(medlemsIndex, @"^[1-" + (medlemmar.Count+1) + "]+$") || string.IsNullOrEmpty(batLangd)) {
                Console.WriteLine("Vänligen ange bara heltal som är listat. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return null;
            }

            string medlemsNummer = medlemmar[int.Parse(medlemsIndex) - 1].Medlemsnummer.ToString();

            nyBat[0] = batTyp;
            nyBat[1] = batLangd;
            nyBat[2] = medlemsNummer;
            Console.WriteLine("\nBåten är nu tillagd i båtlistan i klubben.\n");
            ContinueOnKeyPressed();
            return nyBat;
        }       

        //Ta bort en båt
        public Bat taBortBat(List<Medlem> ml) {
            int ind = ListaBatar(ml);
            Bat removeBoat = null;
            if (ind != 0) {
                Console.Write("\nVilken båt du vill ta bort 1-{0}: ", ind);

                int res;
                string indata = Console.ReadLine();
                int.TryParse(indata, out res);
                int choice = verifyInput(res, 1, ind);
                int index = 0;

                if (choice != 0) {
                    foreach (Medlem m in ml) {
                        foreach (Bat bat in m.Batar) {
                            if ((choice - 1) == index) {
                                Console.Write(bat.ToString());
                                Console.Write(" har tagits bort från båtlistan.\n");
                                removeBoat = bat;
                            }
                            index++;
                        }
                    }
                }               
            }
            ContinueOnKeyPressed();
            return removeBoat;
        }
        //Ändra en båt, hämta båt
        public Bat redBat(List<Medlem> ml, int antalBatar) {
            int index = 0;

            Console.Write("Ange numret på båten du vill ändra: ");
            string choice = Console.ReadLine();

            if (!Regex.IsMatch(choice.ToString(), @"^[1-" + (antalBatar + 1) + "]+$") || string.IsNullOrEmpty(choice)) {
                Console.WriteLine("Vänligen ange bara heltal som är listat. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return null;
            }

            foreach (Medlem m in ml) {
                foreach (Bat bat in m.Batar) {
                    if ((int.Parse(choice) - 1) == index) {
                        return bat;
                    }
                    index++;
                }
            }
            return null;
        }

        //Ändra en båt, be om ny information
        public string[] redBatInfo(int antal) {
            string[] nyBat = new string[2];
            
            Console.Write("Ange ny båttyp (1-5). 1. Segelbåt, 2. Motorseglare, 3. Motorbåt, 4. Kajak/Kanot, 5. Övrigt: ");
            string batTyp = Console.ReadLine();

            if (!Regex.IsMatch(batTyp, @"^[1-5]+$") || string.IsNullOrEmpty(batTyp)) {
                Console.WriteLine("Var vänlig ange ett nummer mellan 1-5. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return null;
            }

            Console.Write("Ny längd på båten i meter (t.ex 1,5): ");
            string batLangd = Console.ReadLine();
            if (!Regex.IsMatch(batLangd, @"^[0-9,]+$") || string.IsNullOrEmpty(batLangd)) {
                Console.WriteLine("Vänligen ange ett tal i meter med en decimal. Tryck valfri knapp för att återgå till till huvudmenyn.");
                ContinueOnKeyPressed();
                return null;
            }

            nyBat[0] = batTyp;
            nyBat[1] = batLangd;
            Console.WriteLine("\nBåtinformationen är nu ändrad.\n");
            ContinueOnKeyPressed();
            return nyBat;
        }

        //Verifiera namn
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
        //Verifiera personnummer
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

        

        //Visa medlemslistan, kompakt
        public void kompaktLista(List<Medlem> medlemmar, bool numrerad = false) {
            if (medlemmar.Count == 0) {
                Console.Write("Det finns inga medlemmar att visa");
                ContinueOnKeyPressed();
            } else {
                int num = 1;
                foreach (Medlem m in medlemmar) {
                    if (!numrerad)
                        Console.WriteLine("Namn: " + m.Namn + ". Medlemsnummer: " + m.Medlemsnummer + " Antal båtar: " + m.AntalBatar + ".");
                    else
                        Console.WriteLine(num + ". Namn: " + m.Namn + ". Medlemsnummer: " + m.Medlemsnummer + " Antal båtar: " + m.AntalBatar + ".");
                    num++;
                }
            }
        }
        //Visa medlemslistan, full
        public void fullLista(List<Medlem> medlemmar, bool numrerad = false) {
            if (medlemmar.Count == 0) {
                Console.Write("Det finns inga medlemmar att visa");
            } else {
                int num = 1;
                foreach (Medlem m in medlemmar) {
                    if (!numrerad)
                        Console.WriteLine("Namn: " + m.Namn + " Personnummer: " + m.Personnummer + ". Medlemsnummer: " + m.Medlemsnummer + " Båtlista: ");
                    else
                        Console.WriteLine(num + ". Namn: " + m.Namn + " Personnummer: " + m.Personnummer + ". Medlemsnummer: " + m.Medlemsnummer + " Båtlista: ");
                    if (m.AntalBatar > 0)
                        foreach (Bat bat in m.Batar)
                            Console.WriteLine("  "+bat.ToString());
                    else
                        Console.WriteLine("  Inga båtar");
                    num++;
                }
            }
        }

        //För att pausa och vänta låta användaren läsa
        public void ContinueOnKeyPressed() {
            Console.WriteLine("Tryck valfri tangent för att fortsätta..");
            Console.ReadKey();
        }

        //Lista alla båtar
        public int ListaBatar(List<Medlem> ml) {    
            
            int index = 0;
            foreach (Medlem m in ml) {
                foreach (Bat bat in m.Batar) {
                    index++;
                    Console.WriteLine(index + ". " + bat.ToString() + " Ägare: " + m.Namn);                    
                }
            }
            if (index == 0) {
                Console.WriteLine("Det finns inga båtar att visa");
                return 0;
            }
            return index;
        }

       

        //public string[] redMedlemsInfo(List<Medlem> medlemmar) {
        //    string[] medlem = new string[3];
        //    string err = "";
        //    if (medlemmar.Count <= 0) {
        //        Console.WriteLine("Det finns inga medlemmar att redigera.");
        //    } else {
        //        kompaktLista(medlemmar);
        //        Console.Write("\nAnge vilken medlem du vill redigera: ");
        //        medlem[2] = Console.ReadLine();
        //        int choice = verifyInput(int.Parse(medlem[2]), 0, medlemmar.Count);
        //        if (choice != 0) {
        //            Console.Write("Förnamn: ");
        //            medlem[0] = Console.ReadLine();
        //            if (!verifInputBokstaver(medlem[0]))
        //                err += "Fel! Du måste ange bokstäver\n";
        //            else {
        //                Console.Write("Personnummer i formatet(yymmddxxxx): ");
        //                medlem[1] = Console.ReadLine();
        //                if (!verifInputPn(medlem[1]))
        //                    err += "Fel! Du måste ange siffror\n";
        //            }
        //            if (err == "")
        //                Console.WriteLine("Medlemsinformationen är nu uppdaterad.");
        //            else
        //                Console.WriteLine(err);
        //        }
        //    }
        //    ContinueOnKeyPressed();
        //    return medlem;
        //}

        public int verifyInput(int choice, int min, int max) {
            if (choice > max || choice < min) { 
                Console.WriteLine("Du måste ange ett nummer mellan {0} och {1}", min, max);
                ContinueOnKeyPressed();
                return 0;
            }
            return choice;
        }       
    }
}
