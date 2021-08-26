using Backend.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Backend.Warehouse
{
    public class Hyllplatser : IEnumerable
    { 
        //Ny lista av objektlåda, samt nödvändiga fält för Hyllplatserna
        private List<Objektlåda> objektlåda = new List<Objektlåda>();
        private double höjd;
        private double bredd;
        private double djup;
        private double tillåtenVolym;
        private int tillåtenVikt;
        private double aktuellVolym;
        private int aktuellVikt;
        private bool innehållerÖmtåligtObjekt = false;
        
       //Konstruktor
        internal Hyllplatser(double höjd, double bredd, double djup, int maxVikt)
        {
            this.höjd = höjd;
            this.bredd = bredd;
            this.djup = djup;
            this.tillåtenVikt = maxVikt;
            this.tillåtenVolym = höjd * bredd * djup;
        }
        //Properties
        internal double Höjd { get { return höjd; } }
        internal double Bredd { get { return bredd; } }
        internal double Djup { get { return djup; } }
        internal double TillåtenVolym { get { return tillåtenVolym; } }
        internal int TillåtenVikt { get { return tillåtenVikt; } }
        public List<Objektlåda> NyaObjekt { get { return objektlåda; } }

        //Returnerar en Enumerator som ittererar genom listan objektlåda som hittas i momentet
        public IEnumerator GetEnumerator()
        {
            return objektlåda.GetEnumerator();
        }
        //Returnerar en Enumerator som ittererar genom listan objektlåda som hittas i momentet
        IEnumerator IEnumerable.GetEnumerator()
        {
            return objektlåda.GetEnumerator();
        }
        //Kontrollerar om objekt kan läggas till i den aktuella Hyllplatsen
        //Returnerar true om den kan, annars false.
        private bool KontrolleraTillagdaObjekt(Objektlåda lådObjekt)
        {
            if (innehållerÖmtåligtObjekt)
            { return false; }
             if (aktuellVolym != 0 && lådObjekt.ÄrÖmtålig)
            {return false;}
            if (aktuellVolym + lådObjekt.Volym > tillåtenVolym)
            {return false;}
            if (aktuellVikt + lådObjekt.Vikt > tillåtenVikt)
            {return false;}
            return true;
        }
        
        //Använder metoden KontrolleraTillagdaObjekt() som sedan kontrollerar om det aktuella objektet kan läggas till.
        //om true, så läggs den in i Hyllplatser,  om inte returnerar den false
        internal bool LäggtillObjekt(Objektlåda lådObjekt)
        {
            if (KontrolleraTillagdaObjekt(lådObjekt))
            {
                objektlåda.Add(lådObjekt);
                aktuellVolym += lådObjekt.Volym;
                aktuellVikt += lådObjekt.Vikt;
                if (lådObjekt.ÄrÖmtålig)
                {
                    innehållerÖmtåligtObjekt = true;
                }
                return true;
            }
            return false;
        }
        
        //Metod som kontrollerar angivet ID nummer är närvarande/finns i Hyllplatser, om ID't finns returnerar den true annars returnerar den false
        internal bool KontrolleraID(int kollaIDNUM)
        {
            for (int i = 0; i < objektlåda.Count; i++)
            {
                if (objektlåda[i].ID == kollaIDNUM)
                {
                    return true;
                }
            }
            return false;
        }
       /* Metod som itterar igenom listan med hjälp av count. Försöker hitta ID nummret som användaren matar in, returnerar objektet
       och tar bort det från dåvarande plats. Om användaren matar in fel eller objekt ej hittas så returnerar metoden null  */
        internal Objektlåda HittaIDNum(int kollaIDNUM)
        {
            for (int i = 0; i < objektlåda.Count; i++)
            {
                if (objektlåda[i].ID == kollaIDNUM)
                {
                    if (objektlåda[i].ÄrÖmtålig)
                    {
                        innehållerÖmtåligtObjekt = false;
                    }
                    aktuellVolym -= objektlåda[i].Volym;
                    aktuellVikt -= objektlåda[i].Vikt;
                    Objektlåda returneraBox = objektlåda[i];
                    objektlåda.RemoveAt(i);
                    return returneraBox;
                }
            }
            return null;
        }
        //Hittar och returnerar en kopia av det angivna objektet. Returnerar ett objekt som kopieras från id
        internal Objektlåda TaemotIdKopia(int id) //*
        {
            for (int i = 0; i < objektlåda.Count; i++)
            {
                if (objektlåda[i].ID == id)
                {
                    Objektlåda returneraBox = objektlåda[i];
                    return returneraBox;
                }
            }
            return null;
        }
        //Returnerar en lista av objekt/lådor som är närvarande/finns i Hyllplatser
        public List<Objektlåda> ReturnaBoxLista()
        {
            return objektlåda;
        }
        //test ej klar
        internal Hyllplatser TestKlona()
        {
            Hyllplatser klonadModell = new Hyllplatser(höjd, bredd, djup, tillåtenVikt);
            List<Objektlåda> objektTillKlon = ReturnaBoxLista();
            foreach (Objektlåda box in objektTillKlon)
            {
                klonadModell.LäggtillObjekt(box);
            }
            return klonadModell;
        }
       
        
    }
}
