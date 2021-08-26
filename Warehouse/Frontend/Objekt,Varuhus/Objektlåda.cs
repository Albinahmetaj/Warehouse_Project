using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Boxes
{
    //En abstakt klass som ärver interfacet I3DStorage. 
    //Den här abstrakta klassen skall ärvas av alla andra objekt klassen (exempel blob, sfär, kub,kuboid) och placeras sedan i klassen Hyllplatser.
    public abstract class Objektlåda : I3DStorage
    {
        //Privata och Inre fält som anser vara nödvändiga för projektet
        private int id;
        private string beskrivning;
        private int vikt;
        internal double volym;
        internal double area;
        private bool ärÖmtålig;
        internal double maxDimension;
        private int försäkringsVärde;

        //Konstruktor
        internal Objektlåda(int iD, string beskrivning, int vikt, bool ärÖmtålig)
        {
            this.id = iD;
            this.beskrivning = beskrivning;
            this.vikt = vikt;
            this.ärÖmtålig = ärÖmtålig;
        }
        //Properties
        public int ID { get { return id; } }
        public string Beskrivning { get { return beskrivning; } }
        public int Vikt { get { return vikt; } }
        //Använder Math.Round metoden för att erhålla volym och area med två decimaler.
        public double Volym { get { return Math.Round(volym, 2); } }
        public double Area { get { return Math.Round(area, 2); } }
        public bool ÄrÖmtålig { get { return ärÖmtålig; } }
        public double MaxDimension { get { return maxDimension; } }
        public int FörsäkringsVärde { get { return försäkringsVärde; } set { försäkringsVärde = value; } }

        //Abstrakta metoder som implementeras/infogas i objektklasserna (blob,sfär,kub,kuboid)
        internal abstract double RäknautVolym();
        internal abstract double RäknautArea();
        internal abstract double MaxDim();
        internal abstract string Databas();
        public abstract override string ToString();
        public abstract string MinimalInfo();
    }
}
