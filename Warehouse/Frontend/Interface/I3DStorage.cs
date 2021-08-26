using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Boxes
{
    //Ett interface som ärvs utav klassen Objektlåda, som sen i sin tur ärvs utav resten av objekten
    public interface I3DStorage
    {
        int ID { get; }
        string Beskrivning { get; }
        int Vikt { get; }
        double Volym { get; }
        double Area { get; }
        bool ÄrÖmtålig { get; }
        double MaxDimension { get; }
        int FörsäkringsVärde { get; set; }
    }
}
