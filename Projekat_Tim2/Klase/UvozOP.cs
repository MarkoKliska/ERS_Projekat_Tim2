using Projekat_Tim2.Interfejsi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat_Tim2.Klase
{
    public class UvozOP : IUvozOP
    {
        private string putanjaUOP;
        private string putanjaXML;

        private string putanjaDoUlaznogFajla;
        //private string putanjaDoSkladista;

        private bool dozvolaZaUvoz;
        private bool validnostFajla;

        public UvozOP()
        {
            
        }

        public bool DozvolaZaUvoz()
        {
            return dozvolaZaUvoz;
        }
        public void UveziXML_OP()
        {
            Console.WriteLine("~~~~~~~~ Unesite putanju do ulaznog fajla: ~~~~~~~\n");
            putanjaDoUlaznogFajla = Console.ReadLine();
            putanjaDoUlaznogFajla = putanjaDoUlaznogFajla.Trim('\"');
            try
            {
                putanjaUOP = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, putanjaDoUlaznogFajla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nevalidna putanja, pokušajte ponovo.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            ProveraFormataUlaznogFajla pfup = new ProveraFormataUlaznogFajla();
            ProveraValidnostiFajla pvf = new ProveraValidnostiFajla();
            AuditTabela audTb = new AuditTabela();
            PutanjeDoSkladista putanjaDoSkl = new PutanjeDoSkladista();

            try
            {
                if (pfup.ProveraFormataOPFajla(putanjaDoUlaznogFajla))
                {
                    dozvolaZaUvoz = true;
                }
                else
                {
                    Console.WriteLine("\nFormat fajla je neispravan.");
                    dozvolaZaUvoz = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Došlo je do greške: \n", ex.ToString());
            }

            if (dozvolaZaUvoz)
            {
                if (ProveraPostojanjaFajlaOP(putanjaUOP) == true)
                {
                    Console.WriteLine("\nDati fajl je već učitan u bazu podataka.\n");
                    return;
                }
                else
                {
                    putanjaXML = putanjaDoSkl.GetSkladisteOP();

                    XmlDocument izvor = new XmlDocument();
                    izvor.Load(putanjaUOP);

                    InformacijeOU info = new InformacijeOU(putanjaUOP);

                    string vremeUvoza = info.sat.ToString() + ":" + info.minut.ToString() + ":" + info.sekunda.ToString();

                    foreach (XmlNode stavka in izvor.SelectNodes("//STAVKA"))
                    {
                        XmlElement ime_fajla = izvor.CreateElement("IME_FAJLA");
                        ime_fajla.InnerText = info.imeFajla;
                        stavka.AppendChild(ime_fajla);

                        XmlElement vreme_uvoza_fajla = izvor.CreateElement("VREME_UVOZA_FAJLA");
                        vreme_uvoza_fajla.InnerText = vremeUvoza;
                        stavka.AppendChild(vreme_uvoza_fajla);

                        XmlElement lokacija_fajla = izvor.CreateElement("LOKACIJA_FAJLA");
                        lokacija_fajla.InnerText = info.lokacija;
                        stavka.AppendChild(lokacija_fajla);
                    }

                    try
                    {
                        if (pvf.ProveraValidnosti(izvor))
                        {
                            validnostFajla = true;
                        }
                        else
                        {
                            Console.WriteLine("Fajl nije validan.");
                            validnostFajla = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Došlo je do greške: \n", ex.Message);
                    }

                    if (validnostFajla)
                    {
                        XmlDocument skladiste = new XmlDocument();
                        skladiste.Load(putanjaXML);

                        foreach (XmlNode modifiedNode in izvor.DocumentElement.ChildNodes)
                        {
                            XmlNode importedNode = skladiste.ImportNode(modifiedNode, true);
                            skladiste.DocumentElement.AppendChild(importedNode);
                        }

                        skladiste.Save(putanjaXML);

                        Console.WriteLine("\nUvoz podataka uspešan.\n");
                        this.AzuriranjePostojanjaFajlaOP(putanjaUOP);
                    }
                    else
                    {
                        audTb.UpisUAuditTabelu(putanjaDoUlaznogFajla);
                        Console.WriteLine("Uvoz podataka neuspešan, pokušajte ponovo.\n");
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Uvoz podataka neuspešan, pokušajte ponovo.\n");
            }

            EvidencijaGP evidencija = new EvidencijaGP(putanjaUOP);
            evidencija.Evidentiraj();
        }
        public bool ProveraPostojanjaFajlaOP(string putanjaDoFajla)
        {
            PutanjeDoSkladista putanja = new PutanjeDoSkladista();
            string putanjaSF = putanja.GetSkladisteFajlova();
            string imeFajla = System.IO.Path.GetFileNameWithoutExtension(putanjaDoFajla);

            XmlDocument skladisteF = new XmlDocument();
            skladisteF.Load(putanjaSF);
            XmlNodeList fajlovi = skladisteF.SelectNodes("//FAJL");

            bool postojiFajl = false;

            foreach (XmlNode fajl in fajlovi)
            {
                if (fajl.InnerText == imeFajla)
                {
                    postojiFajl = true;
                    return postojiFajl;
                }
            }
            return postojiFajl;
        }

        public void AzuriranjePostojanjaFajlaOP(string putanjaDoFajla)
        {
            PutanjeDoSkladista putanja = new PutanjeDoSkladista();
            string putanjaSF = putanja.GetSkladisteFajlova();
            string imeFajla = System.IO.Path.GetFileNameWithoutExtension(putanjaDoFajla);

            XmlDocument skladisteF = new XmlDocument();
            skladisteF.Load(putanjaSF);
            XmlNodeList fajlovi = skladisteF.SelectNodes("//FAJL");

            XmlNode xmlNode = skladisteF.CreateElement("FAJL");
            xmlNode.InnerText = imeFajla;
            skladisteF.DocumentElement.AppendChild(xmlNode);
            skladisteF.Save(putanjaSF);
        }
    }
}
