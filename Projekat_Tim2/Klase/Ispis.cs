using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat_Tim2.Klase
{
    internal class Ispis
    {
        protected List<int> sat = new List<int>();
        protected List<int> prog_potr = new List<int>();
        protected List<int> ostv_potr = new List<int>();
        protected List<double> rel_odst = new List<double>();

        public string[] datum;


        public Ispis() 
        {
            
        }

        public void IspisLogika()
        {
            UnosZaIspis instance = new UnosZaIspis().UnesiInformacije();
            PutanjeDoSkladista putanjaSkl = new PutanjeDoSkladista();
            string putanjaXMLPP = putanjaSkl.GetSkladistePP();
            string putanjaXMLOP = putanjaSkl.GetSkladisteOP();


            XmlDocument skladistePP = new XmlDocument();
            skladistePP.Load(putanjaXMLPP);
            XmlNodeList stavkePP = skladistePP.SelectNodes("/PROGNOZIRANI_LOAD/STAVKA");
            foreach (XmlNode stavka in stavkePP)
            {
                string imeFajla = stavka.SelectSingleNode("IME_FAJLA").InnerText;
                string oblast = stavka.SelectSingleNode("OBLAST").InnerText;

                datum = imeFajla.Split('_');
                string[] dn = datum[3].Split('.');

                if (instance != null)
                {
                    if (Convert.ToInt32(datum[1]) == instance.Godina && Convert.ToInt32(datum[2]) == instance.Mesec && Convert.ToInt32(dn[0]) == instance.Dan && instance.UnetaOblast == oblast)
                    {
                        sat.Add(Convert.ToInt32(stavka.SelectSingleNode("SAT").InnerText));
                        prog_potr.Add(Convert.ToInt32(stavka.SelectSingleNode("LOAD").InnerText));

                    }
                }
            }


            XmlDocument skladisteOP = new XmlDocument();
            skladisteOP.Load(putanjaXMLOP);
            XmlNodeList stavkeOP = skladisteOP.SelectNodes("/PROGNOZIRANI_LOAD/STAVKA");

            int i = 0;
            int prom_ost;
            double vr;

            if (instance != null)
            {
                Console.WriteLine("SAT     PROGNOZIRANA_POTROSNJA     OSTVARENA_POTROSNJA     RELATIVNO_PROCENTUALNO_ODSTUPANJE");
            }

            foreach (XmlNode stavka in stavkeOP)
            {
                string imeFajla = stavka.SelectSingleNode("IME_FAJLA").InnerText;
                string oblast = stavka.SelectSingleNode("OBLAST").InnerText;

                string[] datum;
                datum = imeFajla.Split('_');
                string[] dn = datum[3].Split('.');
                
                if (instance != null)
                {
                    if (Convert.ToInt32(datum[1]) == instance.Godina && Convert.ToInt32(datum[2]) == instance.Mesec && Convert.ToInt32(dn[0]) == instance.Dan && instance.UnetaOblast == oblast)
                    {
                        prom_ost = Convert.ToInt32(stavka.SelectSingleNode("LOAD").InnerText);
                        ostv_potr.Add(prom_ost);
                        vr = Convert.ToDouble(Convert.ToDouble(Math.Abs(prom_ost - prog_potr[i])) / prom_ost * 100);
                        vr = Math.Round(vr, 3);
                        rel_odst.Add(vr);
                        Console.WriteLine(sat[i] + "\t\t" + prog_potr[i] + "\t                  " + prom_ost + "\t\t\t         " + rel_odst[i]);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        i++;
                    }
                }
            }
        }
    }
}
