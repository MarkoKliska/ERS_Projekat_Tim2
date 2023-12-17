using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat_Tim2.Klase
{
    public class ProveraValidnostiFajla
    {
        public ProveraValidnostiFajla() { }

        public bool ProveraValidnosti(XmlDocument fajl)
        {
            try
            {
                XmlNodeList geografskeOblasti = fajl.SelectNodes("//OBLAST");

                foreach (XmlNode geografskaOblast in geografskeOblasti)
                {
                    string sifraGO = geografskaOblast.InnerText;
                    XmlNodeList brSati = fajl.SelectNodes($"//OBLAST[.='{sifraGO}']/../SAT");
                    int ocekivaniBrSati = 24;

                    if (brSati.Count != ocekivaniBrSati)
                    {
                        Console.Write("\nNevalidan broj sati za geografsku oblast ");
                        Console.Write(sifraGO);
                        Console.Write(" u datom fajlu.\n");
                        return false;
                    }
                }

                return true;
            }
            catch (XmlException e)
            {
                LogError($"\nNevalidan format za dati fajl: {e.Message}");
                return false;
            }
        }

        static void LogError(string errorMessage)
        {
            Console.WriteLine($"\nError: {errorMessage}");
        }
    }
}
