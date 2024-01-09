using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat_Tim2.Klase;


namespace Projekat_Tim2.Klase
{
    public class ProveraFormataUlaznogFajla
    {
        private string imeFajla;
        private string[] deloviImena;

        public ProveraFormataUlaznogFajla() { }

        public bool ProveraFormataPPFajla(string putanjaDoFajla)
        {
            imeFajla = System.IO.Path.GetFileNameWithoutExtension(putanjaDoFajla);
            deloviImena = imeFajla.Split('_');

            if (deloviImena.Length == 4)
            {
                if (deloviImena[0].Equals("prog", StringComparison.OrdinalIgnoreCase))
                {
                    if (ProveraFormataDatuma(deloviImena[1], deloviImena[2], deloviImena[3]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool ProveraFormataOPFajla(string putanjaDoFajla)
        {
            imeFajla = System.IO.Path.GetFileNameWithoutExtension(putanjaDoFajla);
            deloviImena = imeFajla.Split('_');

            if (deloviImena.Length == 4)
            {
                if (deloviImena[0].Equals("ostv", StringComparison.OrdinalIgnoreCase))
                {
                    if (ProveraFormataDatuma(deloviImena[1], deloviImena[2], deloviImena[3]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool ProveraFormataDatuma(string godina, string mesec, string dan)
        {
            int y, m, d;

            if (int.TryParse(godina, out y) && int.TryParse(mesec, out m) && int.TryParse(dan, out d))
            {
                if ((y >= 1900 && y <= 2100) && (m >= 1 && m <= 12) && (d <= DateTime.DaysInMonth(y, m)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
