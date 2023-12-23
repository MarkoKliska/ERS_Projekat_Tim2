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

        public UnosZaIspis()
        {
            
        }

        public UnosZaIspis UnesiInformacije()
        {
            UnosZaIspis instance = new UnosZaIspis();

            Console.WriteLine("Unesite datum: ");
            string dateString = Console.ReadLine();

            string[] deloviDatuma = null;

            deloviDatuma = dateString.Split('.');

            int godina = Convert.ToInt32(deloviDatuma[2]);
            int mesec = Convert.ToInt32(deloviDatuma[1]);
            int dan = Convert.ToInt32(deloviDatuma[0]);

            Console.WriteLine("Unesite geografsku oblast: ");
            string unetaOblast = Console.ReadLine();
            unetaOblast = unetaOblast.ToUpper();

            instance.Dan = dan;
            instance.Mesec = mesec;
            instance.Godina = godina;
            instance.UnetaOblast = unetaOblast;

            return instance;
        }

        

    }
}
