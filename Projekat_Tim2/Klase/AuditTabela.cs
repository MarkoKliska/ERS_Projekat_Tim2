using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat_Tim2.Klase
{
    internal class AuditTabela
    {
        public int sat;
        public int minut;
        public int sekunda;
        public string imeFajla;
        public string lokacija;
        public int brojRedovaFajla;

        XmlDocument ucitaniFajl = new XmlDocument();
        XmlNodeList stavke;

        public AuditTabela() { }

        public AuditTabela(string putanjaDoFajla)
        {
            imeFajla = Path.GetFileName(putanjaDoFajla);
            lokacija = putanjaDoFajla;
            DateTime vreme = DateTime.Now;
            sat = vreme.Hour;
            minut = vreme.Minute;
            sekunda = vreme.Second;

            ucitaniFajl.Load(putanjaDoFajla);
            stavke = ucitaniFajl.SelectNodes("//STAVKA");

            brojRedovaFajla = stavke.Count;
        }

        public void UpisUAuditTabelu(string putanjaDoFajla)
        {
            AuditTabela audTb = new AuditTabela(putanjaDoFajla);
            List<AuditTabela> auditLista = new List<AuditTabela>();
            auditLista.Add(audTb);

            SacuvajCSV(auditLista);
        }

        public void SacuvajCSV(List<AuditTabela> auditLista)
        {
            PutanjeDoSkladista putanja = new PutanjeDoSkladista();
            string putanjaDoAuditTabele = putanja.GetAuditTabela();

            try
            {
                using (StreamWriter writer = new StreamWriter(putanjaDoAuditTabele, false, Encoding.UTF8))
                {
                    writer.WriteLine("VREME,IME_FAJLA,LOKACIJA,BROJ_REDOVA_FAJLA");
                }
                foreach (AuditTabela entry in auditLista)
                {
                    string logEntry = $"{entry.sat}:{entry.minut}:{entry.sekunda},{entry.imeFajla},{entry.lokacija},{entry.brojRedovaFajla}";

                    File.AppendAllText(putanjaDoAuditTabele, logEntry + Environment.NewLine);
                }

                Console.WriteLine("Podaci o nevalidnom fajlu uspešno sačuvani.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greška prilikom čuvanja podataka o nevalidnosti: {e.Message}");
            }
        }

        static void LogError(string errorMessage)
        {
            Console.WriteLine($"Error: {errorMessage}");
        }

    }
}
