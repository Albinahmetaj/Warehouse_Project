using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Boxes
{
    //Blob klassen ärver utav den abstrakta klassen objektlåda som ärver interfacet I3DStorage. 
    public class Blob : Objektlåda
    {
        //Vi räknar ut Blobens area, volym etc med hjälp av dess sida. Nedanför har jag ett privat fält med sida som namn.
        private double sida;
        
        //Konstruktor
        internal Blob(int id, string beskrivning, int vikt, double sida) : base(id, beskrivning, vikt, true)
        {
            this.sida = sida;
            this.area = RäknautArea();
            this.volym = RäknautVolym();
            this.maxDimension = MaxDim();
        }
        //Property
        public double Sida { get { return sida; } }

        // Metod som räknar ut blobens arean
        internal override double RäknautArea()
        {

            return Sida * Sida;
        }
        // Räknar ut den högst angivna dimensionen för blob
        internal override double MaxDim()
        {
            return (double)Sida;
        }
        // Metod som räknar ut blobens volymen
        internal override double RäknautVolym()
        {
            return Sida * Sida * Sida;
        }
       
        internal override string Databas()
        {
            return string.Format("BlobNUM\nIDNUM{0}\nBeskrivningsNUM{1}\nVikt{2}\nSida{3}\n", ID, Beskrivning, Vikt, Sida);
        }
        //Returnerar allt som en sträng som sedan slängs upp i consolen som display.
        public override string ToString()
        {
            return string.Format("Typ: Blob\nID: {0}\nBeskrivning: {1}\nVikt: {2} kg\nÖmtålig: JA {3}\nArea: {4} m²\nVolym: {5} m³\nMax Dimension: {6} cm\nSida: {7} cm\n", ID, Beskrivning, Vikt, ÄrÖmtålig, Area/10000, Volym/1000000, MaxDimension, Sida);
        }
       //Den här metoden returnerar en minimal info i översikten så att  man lätt kan hitta platsen med hjälp av ID
        public override string MinimalInfo()
        {
            string shortDescription = "Type: Blob, ID: " + ID;
            return shortDescription;
        }
    }
}
