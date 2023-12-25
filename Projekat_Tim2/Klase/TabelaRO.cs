using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Projekat_Tim2.Klase
{
    class TabelaRO : Ispis
    {
        public void IzveziUCsv()
        {
            try
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

                    string[] datum;
                    datum = imeFajla.Split('_');
                    string[] dn = datum[3].Split('.');

                    if (Convert.ToInt32(datum[1]) == instance.Godina && Convert.ToInt32(datum[2]) == instance.Mesec && Convert.ToInt32(dn[0]) == instance.Dan && instance.UnetaOblast == oblast)
                    {
                        sat.Add(Convert.ToInt32(stavka.SelectSingleNode("SAT").InnerText));
                        prog_potr.Add(Convert.ToInt32(stavka.SelectSingleNode("LOAD").InnerText));

                    }
                }


                XmlDocument skladisteOP = new XmlDocument();
                skladisteOP.Load(putanjaXMLOP);
                XmlNodeList stavkeOP = skladisteOP.SelectNodes("/PROGNOZIRANI_LOAD/STAVKA");

                int i = 0;
                int prom_ost;
                double vr;

                PutanjeDoSkladista putanja = new PutanjeDoSkladista();

                string csvFilePath = putanja.GetTabelaRO(); // Postavite stvarnu putanju gde želite sačuvati CSV fajl

                using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
                {
                    writer.WriteLine("SAT,PROGNOZIRANA_PORTOSNJA,OSTVARENA_PORTOSNJA,RELATIVNO_PROCENTUALNO_ODSTUPANJE");
                    foreach (XmlNode stavka in stavkeOP)
                    {
                        string imeFajla = stavka.SelectSingleNode("IME_FAJLA").InnerText;
                        string oblast = stavka.SelectSingleNode("OBLAST").InnerText;

                        string[] datum;
                        datum = imeFajla.Split('_');
                        string[] dn = datum[3].Split('.');
                        if (Convert.ToInt32(datum[1]) == instance.Godina && Convert.ToInt32(datum[2]) == instance.Mesec && Convert.ToInt32(dn[0]) == instance.Dan && instance.UnetaOblast == oblast)
                        {
                            prom_ost = Convert.ToInt32(stavka.SelectSingleNode("LOAD").InnerText);
                            ostv_potr.Add(prom_ost);
                            vr = Convert.ToDouble(Convert.ToDouble(Math.Abs(prom_ost - prog_potr[i])) / prom_ost * 100);
                            vr = Math.Round(vr, 3);
                            rel_odst.Add(vr);
                            writer.WriteLine($"{sat[i]},{prog_potr[i]},{ostv_potr[i]},{rel_odst[i]}");
                            i++;
                        }
                    }
                }
                Console.WriteLine("Podaci su uspešno izvezeni u tabelu sa relativnim odstupanjima.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri cuvanju podataka u tabelu sa relativnim odstupanjima: {e.Message}");
            }
        }
    }
}
