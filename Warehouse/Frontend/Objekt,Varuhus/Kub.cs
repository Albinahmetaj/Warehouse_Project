using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Boxes
{
    //Kub klassen ärver utav den abstrakta klassen objektlåda som ärver interfacet I3DStorage. 
    public class Kub : Objektlåda
    {
        //Vi räknar ut Kubens area, volym etc med hjälp av dess sida. Nedanför har jag ett privat fält med sida som namn.
        private double sida;
      
        //Konstruktor
        internal Kub(int id, string beskrivning, int vikt, bool ärÖmtålig, double sida) : base(id, beskrivning, vikt, ärÖmtålig)
        {
            this.sida = sida;
            this.area = RäknautArea();
            this.volym = RäknautVolym();
            this.maxDimension = MaxDim();
        }
        //Property
        public double Sida { get { return sida; } }
        //Metod som räknar kubens ut arean
        internal override double RäknautArea()
        {
            return Sida * Sida;
        }
       //Metod som räknar ut kubens volym
        internal override double RäknautVolym()
        {
            double volymKalk = Sida * Sida * Sida;
            return volymKalk;
        }
        // Räknar ut den högst angivna dimensionen för kub
        internal override double MaxDim()
        {
            return (double)Sida;
        }
       
        internal override string Databas()
        {
            return string.Format("KubNUM\nIDNUM{0}\nBeskrivningsNUM{1}\nViktNUM{2}\nÄrÖmtålig{3}\nSidaNUM{4}\n", ID, Beskrivning, Vikt, ÄrÖmtålig,Sida);
        }
        //Returnerar allt som en sträng som sedan slängs upp i consolen som display
        public override string ToString()
        {
            return string.Format("Typ: Kub\nID: {0}\nBeskrivning: {1}\nVikt: {2} kg\nÖmtålig: {3}\nArea: {4} m²\nVolym: {5} m³\nMax Dimension: {6} cm\nSida: {7} cm\n", ID, Beskrivning, Vikt, ÄrÖmtålig ? "Ja" : "Nej", Area/100000, Volym/1000000, MaxDimension,Sida);
        }
        //Den här metoden returnerar en minimal info i översikten så att  man lätt kan hitta platsen med hjälp av ID
        public override string MinimalInfo()
        {
            string shortDescription = "Type: Kub, ID: " + ID;
            return shortDescription;
        }
    }
}
