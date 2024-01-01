using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Projekat_Tim2.Klase
{
    internal class EvidencijaGP
    {
        private string putanjaDoUlaza;
        private List<string>sifre = new List<string>();
        private string putanjaDoEv;
        private List<string> procitaneOblasti = new List<string>();

        public EvidencijaGP(string p = null) 
        {
            putanjaDoUlaza = p;
            
        }
        public void Evidentiraj()
        {
            sifre = this.GetSSS();
            PutanjeDoSkladista putanja = new PutanjeDoSkladista();
            putanjaDoEv = putanja.GetSkladisteEv();

            procitaneOblasti = this.GetProcitaneOblasti();

            XDocument doc = XDocument.Load(putanjaDoEv);

            foreach(string s in procitaneOblasti)
            {
                if (!sifre.Contains(s))
                {
                    XElement root = new XElement("OBLAST");
                    root.Add(new XElement("IME_OBLASTI", s));
                    root.Add(new XElement("SIFRA_OBLASTI", s));
                    doc.Element("GEOGRAFSKA_PODRUCJA").Add(root);
                    doc.Save(putanjaDoEv);
                }
            }
        }

        public void EvidentirajIspis()
        {
            sifre = this.GetSSS();
            Console.Write("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("Ispis oblasti: ");
            foreach (string s in sifre)
            {
                Console.WriteLine(s);
            }
        }

        public void IzmeniImeOblasti()
        {

        }

        public List<string> GetProcitaneOblasti()
        {
            List<string> po = new List<string>();

            string tempObl;

            XmlDocument ulaz = new XmlDocument();
            ulaz.Load(putanjaDoUlaza);
            XmlNodeList stavke = ulaz.SelectNodes("/PROGNOZIRANI_LOAD/STAVKA");
            foreach(XmlNode stavka in stavke)
            {
                tempObl = stavka.SelectSingleNode("OBLAST").InnerText;
                if (!po.Contains(tempObl))
                {
                    po.Add(tempObl);
                }
            }

            return po;
        }

        public List<string> GetSS()
        {
            PutanjeDoSkladista putanja = new PutanjeDoSkladista();
            string putanjaOPS = putanja.GetSkladisteOP();
            string putanjaPPS = putanja.GetSkladistePP();
            string tempObl;

            List<string> ret = new List<string>();

            XmlDocument skladistePP = new XmlDocument();
            skladistePP.Load(putanjaPPS);
            XmlNodeList stavkePP = skladistePP.SelectNodes("/PROGNOZIRANI_LOAD/STAVKA");
            foreach (XmlNode stavka in stavkePP)
            {
                tempObl = stavka.SelectSingleNode("OBLAST").InnerText;
                if (!(ret.Contains(tempObl)))
                {
                    ret.Add(tempObl);
                }
            }

            XmlDocument skladisteOP = new XmlDocument();
            skladisteOP.Load(putanjaOPS);
            XmlNodeList stavkeOP = skladisteOP.SelectNodes("/PROGNOZIRANI_LOAD/STAVKA");
            foreach (XmlNode stavka in stavkeOP)
            {
                tempObl = stavka.SelectSingleNode("OBLAST").InnerText;
                if (!(ret.Contains(tempObl)))
                {
                    ret.Add(tempObl);
                }
            }

            return ret;
        }

        public List<string> GetSSS()
        {
            PutanjeDoSkladista putanja = new PutanjeDoSkladista();
            string putanjaEV = putanja.GetSkladisteEv();
            string tempObl;

            List<string> ret = new List<string>();


            XmlDocument skladisteEV = new XmlDocument();
            skladisteEV.Load(putanjaEV);
            XmlNodeList stavkeEV = skladisteEV.SelectNodes("/GEOGRAFSKA_PODRUCJA/OBLAST");
            foreach (XmlNode oblast in stavkeEV)
            {
                tempObl = oblast.SelectSingleNode("IME_OBLASTI").InnerText;
                if (!(ret.Contains(tempObl)))
                {
                    ret.Add(tempObl);
                }
            }

            return ret;
        }
    }

    
}
