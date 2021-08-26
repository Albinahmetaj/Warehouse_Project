using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Boxes
{
    //Kuboid klassen ärver utav den abstrakta klassen objektlåda som ärver interfacet I3DStorage. 
    public class Kuboid : Objektlåda
    {
        //Vi räknar ut Kuboidens area, volym etc med hjälp av dess sida. Nedanför har jag privata fält höjd,bredd och djup.
        private double höjd;
        private double bredd;
        private double djup;
        //Konstruktor
        internal Kuboid(int id, string description, int weight, bool isFragile, double xSide, double ySide, double zSide) : base(id, description, weight, isFragile)
        {
            this.höjd = xSide;
            this.bredd = ySide;
            this.djup = zSide;
            this.area = RäknautArea();
            this.volym = RäknautVolym();
            this.maxDimension = MaxDim();
        }
        //Properties
        public double Höjd { get { return höjd; } }
        public double Bredd { get { return bredd; } }
        public double Djup { get { return djup; } }
        
        //Metod som räknar ut arean på kuboiden
        internal override double RäknautArea()
        {
            
            double area = höjd * bredd;
            return area;
        }
        // Räknar ut den högst angivna dimensionen för kuboiden och sorterar efter storlek
        internal override double MaxDim()
        {
            
            double[] högstFörstArr = new double[] { (double)höjd, (double)bredd, (double)djup };
            Array.Sort(högstFörstArr);
            return högstFörstArr[2];
        }
       //Räknar du objektets volym
        internal override double RäknautVolym()
        {
            double volymKalk = höjd * bredd * djup;
            return volymKalk;
        }
        
        internal override string Databas()
        {
            return string.Format("KuboidNUM\nIDNUM{0}\nBeskrivningsNUM{1}\nViktNUM{2}\nÄrÖmtålig{3}\nHöjdNUM{4}\nBreddNUM{5}\nDjupNUM{6}\n", ID,Beskrivning,Vikt,ÄrÖmtålig,höjd,bredd,djup);
        }
        //Returnerar allt som en sträng som sedan slängs upp i consolen som display
        public override string ToString()
        {
            return string.Format("Typ: Kubeoid\nID: {0}\nBeskrivning: {1}\nVikt: {2} kg\nÖmtålig: {3}\nArea: {4} m²\nVolym: {5} m³\nMax Dimension: {6} m\nHöjd: {7} cm\nBredd: {8} cm\nDjup: {9} cm\n", ID, Beskrivning, Vikt, ÄrÖmtålig ? "Ja" : "Nej", Area/10000, Volym/1000000, MaxDimension, höjd, bredd, djup);
        }
        //Den här metoden returnerar en minimal info i översikten så att  man lätt kan hitta platsen med hjälp av ID
        public override string MinimalInfo()
        {
            string shortDescription = "Type: Kuboid, ID: " + ID;
            return shortDescription;
        }
    }
}
