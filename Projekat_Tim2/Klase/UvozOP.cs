using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat_Tim2.Klase
{
    internal class UvozOP
    {
        private string putanjaUOP;
        private string putanjaXML;

        private string putanjaDoUlaznogFajla;
        private string putanjaDoSkladista;

        public UvozOP()
        {
            Console.WriteLine("Unesite putanju do ulaznog fajla: \n");
            putanjaDoUlaznogFajla = Console.ReadLine();
            putanjaUOP = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, putanjaDoUlaznogFajla);

            string dir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            putanjaDoSkladista = @"Skladista\skladisteOP.xml";
            putanjaXML = Path.Combine(dir, putanjaDoSkladista);
            Path.GetFullPath(putanjaXML);

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

            XmlDocument skladiste = new XmlDocument();
            skladiste.Load(putanjaXML);

            foreach (XmlNode modifiedNode in izvor.DocumentElement.ChildNodes)
            {
                XmlNode importedNode = skladiste.ImportNode(modifiedNode, true);
                skladiste.DocumentElement.AppendChild(importedNode);
            }

            skladiste.Save(putanjaXML);

            Console.WriteLine("Uvoz podataka uspesan.");
        }
        
    }
}
