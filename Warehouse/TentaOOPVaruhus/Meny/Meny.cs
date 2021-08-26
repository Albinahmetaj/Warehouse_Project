using Backend.Boxes;
using Backend.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend
{
    class Meny
    {
        MenyFunktioner functions = new MenyFunktioner();
        //Meny funktion som låter användaren mata in vad hen vill göra
        public void Menyn()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Välkommen till Kronans Lager 144");
                    Console.WriteLine("---------------------------------------------------");
                    Console.Write("\n**Välj ett Alternativ**:\n");
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("[1]    Lägg till Objekt"); //Lägger till objekt får sedan upp den andra menyn om vilket objekt man vill skapa (sfär,blob,kub,kuboid)
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("[2]    Flytta Objekt"); // Flytta objekt med hjälp av ID nummer
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("[3]    Ta Bort Objekt"); // Ta bort objekt via ID nummer
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("[4]    Sök på objekt via ID nummer"); // Sök upp objekt via ID nummer och få information om det objektet
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("[5]    Sök på ett specikt objekt och få plats info"); // Sök efter plats via våning och platss (1-3, 1-100)
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("[6]    Översikt av alla platser"); // Översikt av hela varuhuset med alla platser och våningar, visar endast vilket ID och vad för typ av objekt
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("[9]    Exit");
                    Console.WriteLine("---------------------------------------------------");
                    ConsoleKey valdTangent = Console.ReadKey().Key;
                    switch (valdTangent)
                    {
                        case ConsoleKey.D1:
                         functions.LäggTillObjekt(VilkenBoxTyp());
                            break;
                        case ConsoleKey.D2:
                        functions.FlyttaObjekt();
                            break;
                        case ConsoleKey.D3:
                        functions.TabortObjekt();
                            break;
                        case ConsoleKey.D4:
                        functions.SökObjektMedID();
                            break;
                        case ConsoleKey.D5:
                        functions.SökSpecifikPlats();
                            break;
                        case ConsoleKey.D6:
                        functions.Översikt();
                            break;
                       case ConsoleKey.D9:
                        case ConsoleKey.Escape:
                            functions.Avsluta();
                            break;
                            

                        default:
                            break;}}
                catch (Exception ex){
                    Console.WriteLine(ex.GetType().ToString());
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
        }
       //En annan meny som låter användaren välja vilken typ av objekt hen vill skapa
        private MenyFunktioner.ObjektTyp VilkenBoxTyp()
        {
            MenyFunktioner.ObjektTyp objektTyp = MenyFunktioner.ObjektTyp.ObjektTOM;
            while (objektTyp == MenyFunktioner.ObjektTyp.ObjektTOM)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Lägg till objekt");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Vad är det för typ av objekt?");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("[1]    Blob");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("[2]    Kub");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("[3]    Kuboid");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("[4]    Sfär");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("[5]    Tillbaka till menyn");
                Console.WriteLine("---------------------------------------------------");
                ConsoleKey chosenBoxType = Console.ReadKey().Key;
                switch (chosenBoxType)
                { case ConsoleKey.D1:
                  objektTyp = MenyFunktioner.ObjektTyp.Blob;
                        break;
                  case ConsoleKey.D2:
                  objektTyp = MenyFunktioner.ObjektTyp.Kub;
                        break;
                  case ConsoleKey.D3:
                  objektTyp = MenyFunktioner.ObjektTyp.Kuboid;
                        break;
                  case ConsoleKey.D4:
                  objektTyp = MenyFunktioner.ObjektTyp.Sfär;
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        Menyn();
                        break;
                    default:
                        break;
                }
            }
            return objektTyp;
        }
    }
    class MenyFunktioner
    {
        //Enum som representerar varje objekttyp, som sedan inplementeras i läggtillobjekt metoden och i menyerna ovanför
        internal enum ObjektTyp
        {
            Blob,
            Kub,
            Kuboid,
            Sfär,
            ObjektTOM
        }
        //Ny lista utav varuhus med antal platser och våningar
        //Klassen hanterar meny funktionerna med hjälp  av interface och metoder.
        private Varuhus nyttVaruhus = new Varuhus(101, 4);
        //Skapar och lägger till objekt i Varuhuset.
        internal void LäggTillObjekt(ObjektTyp objektTyp)
        {
            Console.Clear();
            Console.WriteLine("Var vänlig och lägg till ett nytt objekt:");
            string beskrivning = MataInBeskrivning(objektTyp);
            int vikt = HämtaVikt();
            bool ömtålig = objektTyp == ObjektTyp.Blob ? true : ReturneraÖmtåligtObjekt();
            Objektlåda nyttObjekt;
            if (objektTyp == ObjektTyp.Blob)
            {//om Paul/den som rättar vill att man ska lägga till en höjd bredd och djup som exemplaret kuboiden så kan jag göra det. (ändra isf samma värde som kuboid och propertyn på blob,sfär,kub)
                double sida = ReturneraMått();
                nyttObjekt = nyttVaruhus.NyBlob(beskrivning, vikt, sida);
            }
            else if (objektTyp == ObjektTyp.Kub)
            {double sida = ReturneraMått();
                nyttObjekt = nyttVaruhus.NyKub(beskrivning, vikt, ömtålig, sida);
            }
            else if (objektTyp == ObjektTyp.Kuboid)
            {double höjd = ReturneraMått("höjd");
                double bredd = ReturneraMått("bredd");
                double djup = ReturneraMått("djup");
                nyttObjekt = nyttVaruhus.NyKuboid(beskrivning, vikt, ömtålig, höjd, bredd, djup);
            }
            else
            {double radius = ReturneraMått("radie");
                nyttObjekt = nyttVaruhus.NySfär(beskrivning, vikt, ömtålig, radius);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Objektet har skapats! med ID nummer: {0}.", nyttObjekt.ID);
            Console.ForegroundColor = ConsoleColor.White;
            ImplementeraObjektVaruhus(nyttObjekt);

        }
       //Metod som flyttar objekt till en ny våning samt plats med hjälp av int parametrarna
        internal void FlyttaObjekt()
        {
            Console.Clear();
            Console.WriteLine("Flytta objekt:");
            int FlyttaID = HämtaID();
            bool objektFinns = nyttVaruhus.HittaObjekt(FlyttaID, out int föregåendeVåning, out int föregåendePlats);
            if (objektFinns)
            {int nyVåning = HämtaIndex("plats", nyttVaruhus.VåningsNum - 1);
                int nyPlats = HämtaIndex("våning", nyttVaruhus.PlatsNum - 1);
                bool objektFlyttades = nyttVaruhus.FlyttaObjekt(FlyttaID, nyVåning, nyPlats);
                if (objektFlyttades)
                {Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Objektet med ID nummer: {0} har flyttats från plats: {1}, våning {2}", FlyttaID, föregåendeVåning, föregåendePlats);
                    Console.WriteLine("till plats: {0}, våning: {1}.", nyVåning, nyPlats);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {Console.WriteLine("Objektet med ID nummer: {0} kan inte flyttas till plats: {1}, våning: {2}.", FlyttaID, nyVåning, nyPlats);
                    Console.WriteLine("Objektet finns fortfarande kvar på plats: {0}, våning: {1}.", föregåendeVåning, föregåendePlats);
                }
            }
            else
            { Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("OBS ID hittades ej!: {0}.", FlyttaID);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Tryck enter för att återvända till menyn");
            }
            Console.ReadLine();

        }
        //Metod som tar bort objekt via ID nummer
        internal void TabortObjekt()
        {
            Console.Clear();
            Console.WriteLine("Tabort Objekt:");
            int objektID = HämtaID();
            bool ärBorta = nyttVaruhus.ElemineraObjekt(objektID);
            if (ärBorta)
            {Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Objekt med ID nummer {0} togs bort.", objektID);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tyvärr så finns inget objekt med detta ID Nummer {0}. Inget togs bort.", objektID);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ReadLine();
        }
       //Metod som låter användaren att söka objekt via ett ID nummer
        internal void SökObjektMedID()
        {
            Console.Clear();
            Console.WriteLine("Var vänlig och sök efter objekt med ID nummer:");
            int objektID = HämtaID();
            bool objektHittas = nyttVaruhus.HittaObjekt(objektID, out int våning, out int plats);
            if (objektHittas)
            {
                Objektlåda objekt = nyttVaruhus.FåEnKopia(objektID, våning, plats);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Lådan hittades på plats {1}, våning {2}.", objektID, våning, plats);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(objekt.ToString());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tyvärr så finns inget objekt med detta ID Nummer:{0}.", objektID);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ReadLine();
        }
       //Metod som visar info för om objekt som befinner sig i den specifika platsen, med hjälp av våning och plats inmatning
        internal void SökSpecifikPlats()
        {
            Console.Clear();
            Console.WriteLine("Sök efter specifik plats:");
            int våning = HämtaIndex("plats", nyttVaruhus.VåningsNum - 1);
            int plats = HämtaIndex("våning", nyttVaruhus.PlatsNum - 1);
            if (nyttVaruhus[våning, plats].NyaObjekt.Count < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Det finns tyvärr inga objekt på plats: {0} våning: {1}.", våning, plats);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write("*************************************************\n");
                Console.WriteLine("Objekt hittades på plats: {0} våning: {1}:", våning, plats);
                Console.Write("*************************************************\n");
                foreach (Objektlåda box in nyttVaruhus[våning, plats].NyaObjekt)
                {
                    Console.Write(box.ToString());
                    Console.Write("***********************************************\n");
                }
            }
            Console.ReadLine();
        }
       //Översikt av alla objekt i hela varuhuset, visar alla våningar och platser med beskrivning av vilket typ av objekt och vilket ID nummer den har.
        internal void Översikt()
        {
            Console.Clear();
            Console.WriteLine("Översikt på hela varuhuset.");
            Console.Write("OBS Listan kan bli väldigt lång: Vill du fortsätta? (J)a/(N)ej");
            ConsoleKey keyIndex = Console.ReadKey().Key;
            Console.Clear();
            if (keyIndex == ConsoleKey.J)
            {
                for (int våning = 1; våning < nyttVaruhus.VåningsNum; våning++)
                {
                    for (int plats = 1; plats < nyttVaruhus.PlatsNum; plats++)
                    {
                        Console.WriteLine("Objekt som hittades på plats: {0}, våning: {1}:", våning, plats);
                        Console.Write("***********************************************\n");
                        foreach (Objektlåda objekt in nyttVaruhus[våning, plats])
                        {
                            Console.WriteLine(objekt.MinimalInfo());
                        }
                        Console.Write("***********************************************\n");
                    }
                }
                Console.ReadLine();
            }
        }
       //metod som avslutar programmet
        internal void Avsluta()
        {
            Console.Clear();
            Console.Write("Är du säker på att du vill avsluta? (J)a/(N)ej ");
            ConsoleKey avslutaJaNej = Console.ReadKey().Key;
            Console.WriteLine();
            if (avslutaJaNej == ConsoleKey.J || avslutaJaNej == ConsoleKey.Escape || default == ConsoleKey.N)
            {
                {
                    Console.WriteLine("Tryck Enter för att avsluta.");
                }
                Console.ReadLine();

                System.Environment.Exit(0);
            }
        }
       // Metod som uppmanar användaren att ge objektet en beskrivning som får innehålla mellan 1-100 tecken
        private string MataInBeskrivning(ObjektTyp objektTyp)
        {
            Console.Write("Mata in beskrivning för objektet {0}: ", objektTyp);
            string beskrivning = Console.ReadLine();
            while (beskrivning.Length < 1 || beskrivning.Length > 100)
            {
                if (beskrivning.Length < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Får ej lämnas tom.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (beskrivning.Length > 100)
                {
                    Console.WriteLine("Inte mer än 100 ord.");
                }
                if (beskrivning.Contains("#") && beskrivning.Contains("%"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Inga speciella symboler tillåtet '#'.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("Var god och mata in en logisk beskrivning till objektet: {0}: ", objektTyp);
                beskrivning = Console.ReadLine();
            }
            return beskrivning;
        }
        //Metod som uppmanar användaren att ta emot objektets vikt i heltal
        private int HämtaVikt()
        {
            Console.Write("Var vänlig och fyll i vikt i kilogram, (OBS inga decimaler): ");
            string ViktString = Console.ReadLine();
            bool viktTryParse = Int32.TryParse(ViktString, out int vikt);
            while (!viktTryParse)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Endast siffror är tillåtet. Testa igen: ");
                Console.ForegroundColor = ConsoleColor.White;
                ViktString = Console.ReadLine();
                viktTryParse = Int32.TryParse(ViktString, out vikt);
            }
            return vikt;
        }
       //Metod som uppmanar användaren att ta emot objektets mått i decimaltal
        private double ReturneraMått(string someName = "sida")
        {
            Console.WriteLine("Kub,Sfär,Blob 1-200cm Kuboid: H:1-200cm B:1-300cm D:1-200cm");
            double sida;
            Console.Write("\n");
            Console.Write("Var god och skriv dess {0}, i centimeter: ", someName);
            string sidaSträng = Console.ReadLine();
            bool sidaTryParse = Double.TryParse(sidaSträng, out sida);

            while (!sidaTryParse)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Endast siffror tack! Var god och försök att skriva i {0} igen, i centimeter: ", someName);
                Console.ForegroundColor = ConsoleColor.White;
                sidaSträng = Console.ReadLine();
                sidaTryParse = Double.TryParse(sidaSträng, out sida);

            }
            return Math.Round(sida, 2);
        }
       //Metod som uppmanar användaren om objektet är ömtåligt eller inte
        private bool ReturneraÖmtåligtObjekt()
        {
            Console.Write("Är detta objekt ömtåligt? (J)a/(N)ej: ");
            ConsoleKey ömTåligt = Console.ReadKey().Key;
            Console.WriteLine();
            while (!(ömTåligt == ConsoleKey.J || ömTåligt == ConsoleKey.N))
            {
                Console.Write("Var vänlig och fyll endast i bokstaven J eller N. Är detta objekt ömtåligt? (J)a/(N)ej: ");
                ömTåligt = Console.ReadKey().Key;
                Console.WriteLine();
            }
            return ömTåligt == ConsoleKey.J ? true : false;
        }
        //Metod som ber användaren att ge plats i Varuhuset automatisk eller manuellt
        private void ImplementeraObjektVaruhus(Objektlåda objekt)
        {
            Console.Write("Vill du själv välja plats till objektet?" +
                "\n(om du väljer nej väljer systemet automatiskt åt dig) (J)a/(N)ej ");
            ConsoleKey väljDestination = Console.ReadKey().Key;
            Console.WriteLine();
            while (!(väljDestination == ConsoleKey.J || väljDestination == ConsoleKey.N))
            {
                Console.Write("Var vänlig och fyll endast i bokstaven J eller N. Vill du välja plats för objektet? (J)a/(N)ej: ");
                väljDestination = Console.ReadKey().Key;
                Console.WriteLine();
            }
            if (väljDestination == ConsoleKey.J)
            {
                int våning = HämtaIndex("plats", nyttVaruhus.VåningsNum - 1);
                int plats = HämtaIndex("våning", nyttVaruhus.PlatsNum - 1);
                bool lyckatsPlacera = nyttVaruhus.ObjektManuellPlacering(objekt, våning, plats);
                if (lyckatsPlacera)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Objektet blev placerad på plats {0}, våning {1}.", våning, plats);
                    Console.WriteLine("Objektets ID nummer: {0}.", objekt.ID);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Kan ej placera objekt HÄR, Försök igen.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                    ImplementeraObjektVaruhus(objekt);
                }
            }
            else
            {
                bool lyckadesInföra = nyttVaruhus.ObjektAutomatiskPlats(objekt, out int våning, out int plats);
                if (lyckadesInföra)
                {
                    Console.WriteLine("LYCKAD! Objektet blev placerad på plats {0}, våning {1}.", våning, plats);
                    Console.WriteLine("Objektets ID nummer är: {0}.", objekt.ID);
                    Console.ReadLine();
                }
                else
                {
                   
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Kan ej placeras här, finns ingen plats för objektet.");
                    Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
       
        private int HämtaIndex(string index, double maxNum)
        {
            Console.Write("Vilken {0}? (1-{1}) ", index, maxNum);
            string platsString = Console.ReadLine();
            bool lyckadesAnalysera = int.TryParse(platsString, out int plats);
            while (!lyckadesAnalysera || plats > maxNum)
            {
                if (!lyckadesAnalysera)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Endast siffror tack. Försök igen: ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Endast siffor mellan 1-{0},Försök igen: ", maxNum);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                platsString = Console.ReadLine();
                lyckadesAnalysera = int.TryParse(platsString, out plats);
            }
            return plats;
        }
        //Metod som frågar användaren om ID nummer, metoden är sedan instansierad i andra metoder
        private int HämtaID()
        {
            Console.Write("Mata in objektets ID (endast siffror tillåtet): ");
            string iDSträng = Console.ReadLine();
            bool lyckadesAnalysera = int.TryParse(iDSträng, out int id);
            while (!lyckadesAnalysera)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Endast siffor när du skriver in ID nummer, FÖRSÖK IGEN: ");
                Console.ForegroundColor = ConsoleColor.White;
                iDSträng = Console.ReadLine();
                lyckadesAnalysera = int.TryParse(iDSträng, out id);
            }
            return id;
        }

    }
}
