using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_Tim2.Klase
{
    internal class InformacijeOU
    {
        public string imeFajla;
        public string lokacija;
        public int sat;
        public int minut;
        public int sekunda;

        public InformacijeOU(string filePath)
        {
            imeFajla = Path.GetFileName(filePath);
            lokacija = filePath;
            DateTime vreme = DateTime.Now;
            sat = vreme.Hour;
            minut = vreme.Minute;
            sekunda = vreme.Second;
        }
    }
}
