using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_Tim2.Klase
{
    internal class UnosZaIspis
    {
        public int Godina {  get; set; }
        public int Mesec { get; set; }
        public int Dan { get; set; }

        public string UnetaOblast { get; set; }

        private string dateString;

        private int godinaInt;
        private int mesecInt;
        private int danInt;

        private string godinaStr;
        private string mesecStr;
        private string danStr;

        private string unetaOblast;

        private int g, m, d;

        public UnosZaIspis()
        {
            
        }

        public UnosZaIspis UnesiInformacije()
        {
            string[] deloviDatuma = null;
            UnosZaIspis instance = new UnosZaIspis();
            ProveraFormataUlaznogFajla provera = new ProveraFormataUlaznogFajla();
            Ispis ispis = new Ispis();

            Console.WriteLine("Unesite datum: ");
            dateString = Console.ReadLine();
            deloviDatuma = dateString.Split('.');

            godinaStr = deloviDatuma[2];
            mesecStr = deloviDatuma[1];
            danStr = deloviDatuma[0];

            godinaInt = Convert.ToInt32(godinaStr);
            mesecInt = Convert.ToInt32(mesecStr);
            danInt = Convert.ToInt32(danStr);
            
            while (provera.ProveraFormataDatuma(godinaStr, mesecStr, danStr) != true)
            {
                Console.WriteLine("Neispravan format datuma, unesite novi datum:");
                dateString = Console.ReadLine();
                deloviDatuma = dateString.Split('.');
                godinaStr = deloviDatuma[2];
                mesecStr = deloviDatuma[1];
                danStr = deloviDatuma[0];
            }    

            godinaInt = Convert.ToInt32(deloviDatuma[2]);
            mesecInt = Convert.ToInt32(deloviDatuma[1]);
            danInt = Convert.ToInt32(deloviDatuma[0]);


            Console.WriteLine("Unesite geografsku oblast: ");
            unetaOblast = Console.ReadLine();
            unetaOblast = unetaOblast.ToUpper();

            instance.Dan = danInt;
            instance.Mesec = mesecInt;
            instance.Godina = godinaInt;
            instance.UnetaOblast = unetaOblast;
            
            
            return instance; 
        }
    }
}
