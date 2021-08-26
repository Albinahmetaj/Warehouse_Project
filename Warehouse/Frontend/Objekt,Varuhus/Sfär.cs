using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Boxes
{
    //Sfär klassen ärver utav den abstrakta klassen objektlåda som ärver interfacet I3DStorage. 
    public class Sfär : Objektlåda
    {
        //Vi räknar ut Sfärens area, volym etc med hjälp av dess sida. Nedanför har jag ett privat fält med sida som namn.
        private double radie;
        //Konstruktor
        internal Sfär(int id, string beskrivning, int vikt, bool ärÖmtålig, double radie) : base(id, beskrivning, vikt, ärÖmtålig)
        {
            this.radie = radie;
            this.area = RäknautArea();
            this.volym = RäknautVolym();
            this.maxDimension = MaxDim();
        }
        //Property
        public double Radie { get { return radie; } }
        //Metod som räknar ut objektets area, eftersom sfär räknas som en kub. Så tar man radien gånger radien och får en diameter. Sedan tar man diametern * 2 och får fram arean.
        internal override double RäknautArea()
        {

            double areaKalk = Radie * Radie;
            return areaKalk;
        }
        // Räknar ut den högst angivna dimensionen för sfär
        internal override double MaxDim()
        {
            return (double)Radie;
        }
        // Metod som räknar ut sfärens volym,  eftersom sfär räknas som en kub. Så tar man radien gånger radien och får en diameter. Sedan tar man diametern * 3 och får fram volym.
        internal override double RäknautVolym()
        {
            double volymKalk = Radie * Radie * Radie;
            return volymKalk;
        }
       
        internal override string Databas()
        {
            return string.Format("SfärNUM\nIDNUM{0}\nBeskrivningsNUM{1}\nVikt{2}\nÄrÖmtålig{3}\nRadie{4}\n", ID, Beskrivning, Vikt, ÄrÖmtålig, Radie);
        }
        //Returnerar allt som en sträng som sedan slängs upp i consolen som display
        public override string ToString()
        {
            return string.Format("Typ: Sfär\nID: {0}\nBeskrivning: {1}\nVikt: {2} kg\nÖmtålig: {3}\nArea: {4} m²\nVolym: {5} m³\nMax Dimension: {6} cm\nRadie: {7} cm\n", ID, Beskrivning, Vikt, ÄrÖmtålig ? "Ja" : "Nej", Area/100000, Volym/1000000, MaxDimension, Radie);
        }
        //Den här metoden returnerar en minimal info i översikten så att  man lätt kan hitta platsen med hjälp av ID
        public override string MinimalInfo()
        {
            string shortDescription = "Type: Sfär, ID: " + ID;
            return shortDescription;
        }
    }
}
