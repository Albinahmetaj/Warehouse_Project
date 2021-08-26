using Backend.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Backend.Warehouse
{
    //Denna klassen instancierar hela varuhuset med hjälp av en multidimensionell array av klassen Hyllplatser
    //Klassen är kapabel att skapa objekt av lådor och hyllplatser
    public class Varuhus : IEnumerable
    {
        private Hyllplatser[,] lager;
        private readonly int våningsIndex;
        private readonly int platsIndex;
        private int idNr = 1;
     
        //Konstruktor som instansierar med den multidimensionella arrayen utav parametrarna, våningsnummer och platsnummer.
        public Varuhus(int våningsNum, int platsNum)
        {
            this.lager = NyttLager(våningsNum, platsNum);
            this.våningsIndex = våningsNum;
            this.platsIndex = platsNum;
        }
        //Properties
        public int VåningsNum { get { return våningsIndex; } }
        public int PlatsNum { get { return platsIndex; } }

        
        //Två indexers som returnerar den angivna våningen och platsen i Hyllplatser
        public Hyllplatser this[int indexEtt, int indexTvå]
        {
            get
            {
                return lager[indexEtt, indexTvå];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return lager.GetEnumerator();
        }
        //Metod som skapar en ny tom multidimensionell array av Hyllplatser med två int parametrar som representerar hur många våningar och hur många platser.
        private Hyllplatser[,] NyttLager(int mängdavVåning, int mängdavPlats)
        {
            Hyllplatser[,] nyttLager = new Hyllplatser[mängdavVåning, mängdavPlats];
            //Initializing the storage with empty WarehouseLocations
            for (int i = 0; i < nyttLager.GetLength(0); i++)
            {
                for (int j = 0; j < nyttLager.GetLength(1); j++)
                {
                    nyttLager[i, j] = NyttDefaultLager();
                }
            }
            return nyttLager;
        }

        //Metod som skapar ett nytt Varuhus och visar defaultstorlek för lagerplats (höjd,bredd,djup, maxvikt)
        public Hyllplatser NyttDefaultLager()
        {
            return new Hyllplatser(200, 300, 200, 1000);
        }
        // Skapar ett nytt objekt av Blob med inmatningsparametrar och ett eget exklusivt ID nummer
        public Blob NyBlob(string beskrivning, int vikt, double sida)
        {
            return new Blob(idNr++, beskrivning, vikt, sida);
        }
        // Skapar ett nytt objekt av Kub med inmatningsparametrar och ett eget exklusivt ID nummer
        public Kub NyKub(string beskrivning, int vikt, bool ärÖmtålig, double sida)
        {
            return new Kub(idNr++, beskrivning, vikt, ärÖmtålig, sida);
        }
        // Skapar ett nytt objekt av Kuboid med inmatningsparametrar och ett eget exklusivt ID nummer
        public Kuboid NyKuboid(string beskrivning, int vikt, bool ömtålig, double höjd, double bredd, double djup)
        {
            return new Kuboid(idNr++, beskrivning, vikt, ömtålig, höjd, bredd, djup);
        }
        // Skapar ett nytt objekt av Sfär med inmatningsparametrar och ett eget exklusivt ID nummer
        public Sfär NySfär(string beskrivning, int vikt, bool ömtålig, double radie)
        {
            return new Sfär(idNr++, beskrivning, vikt, ömtålig, radie);
        }

        //Metod som automatiskt försöker lägga in objekt i första lediga plats
        public bool ObjektAutomatiskPlats(Objektlåda objekt, out int placeradVåning, out int placeradPlats)
        {
            for (int i = 1; i < lager.GetLength(0); i++)
            {
                for (int j = 1; j < lager.GetLength(1); j++)
                {
                    if (lager[i, j].LäggtillObjekt(objekt))
                    {
                        placeradVåning = i;
                        placeradPlats = j;
                       
                        return true;
                    }
                }
            }
            placeradVåning = -1;
            placeradPlats = -1;
            return false;
        }
        public void ReadFile()
        {

        }
        public void WriteFile()
        {

        }
        //Metod som låter användaren lägga in objekt manuellt med hjälp av inmatningsparametrarna våning och plats
        public bool ObjektManuellPlacering(Objektlåda objekt, int våning, int plats)
        {
            if (lager[våning, plats].LäggtillObjekt(objekt))
            {
                
                return true;
            }
            return false;
        }
       //Metod som hittar objekt via ID nummer, parametrarna visar Hyllplatsen av objektet
        public bool HittaObjekt(int id, out int våning, out int plats)
        {
            for (int i = 1; i < lager.GetLength(0); i++)
            {
                for (int j = 1; j < lager.GetLength(1); j++)
                {
                    if (lager[i, j].KontrolleraID(id))
                    {
                        våning = i;
                        plats = j;
                        return true;
                    }
                }
            }
            våning = -1;
            plats = -1;
            return false;
        }
        //ej klar
        public Objektlåda FåEnKopia(int id, int våning, int plats)
        {
            Objektlåda returneraObjekt = lager[våning, plats].TaemotIdKopia(id);
            return returneraObjekt;
        }
        //Metod som flyttar objektet via ID nummer till en ny plats om  det ej finns plats för objektet så får den sin föregående plats.
        public bool FlyttaObjekt(int id, int nyVåning, int nyPlats)
        {
            bool hittaLåda = HittaObjekt(id, out int våning, out int plats);
            if (hittaLåda)
            {
                Objektlåda flyttaObjekt = lager[våning, plats].HittaIDNum(id);
                if (lager[nyVåning, nyPlats].LäggtillObjekt(flyttaObjekt))
                {
                   
                    return true;
                }
               
                lager[våning, plats].LäggtillObjekt(flyttaObjekt);
            }
            return false;
        }
       //Tar bort objekt via det angivna ID numret
        public bool ElemineraObjekt(int id)
        {
            bool hittaLåda = HittaObjekt(id, out int våning, out int plats);
            if (hittaLåda)
            {
                lager[våning, plats].HittaIDNum(id);
               
                return true;
            }
            return false;
        }
        //ej klar
        public Hyllplatser FåKlonavLager(int våning, int plats)
        {
            return lager[våning, plats].TestKlona();
        }
       
    }
}
