using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_Tim2.Klase
{
    public class PutanjeDoSkladista
    {
        public PutanjeDoSkladista() { }
        public string GetSkladistePP()
        {
            string dirPP = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string putanjaDoSkladista1 = @"Skladista\skladistePP.xml";
            string putanjaXMLPP = Path.Combine(dirPP, putanjaDoSkladista1);
            return Path.GetFullPath(putanjaXMLPP);
        }

        public string GetSkladisteOP()
        {
            string dirOP = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string putanjaDoSkladista2 = @"Skladista\skladisteOP.xml";
            string putanjaXMLOP = Path.Combine(dirOP, putanjaDoSkladista2);
            return Path.GetFullPath(putanjaXMLOP);
        }
        public string GetTabelaRO()
        {
            string dirTRO = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string putanjaDoTabeleRO = @"Skladista\TabelaRO.csv";
            string putanjaCSVTRO = Path.Combine(dirTRO, putanjaDoTabeleRO);
            return Path.GetFullPath(putanjaCSVTRO);
        }

        public string GetSkladisteEv()
        {
            string dirEV = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string putanjaDoSkladistaEv = @"Skladista\evidencijaGP.xml";
            string putanjaXMLEV = Path.Combine(dirEV, putanjaDoSkladistaEv);
            return putanjaXMLEV;
        }

        public string GetSkladisteFajlova()
        {
            string dirF = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string putanjaDoSkladistaF = @"Skladista\skladisteFajlova.xml";
            string putanjaXMLF = Path.Combine(dirF, putanjaDoSkladistaF);
            return putanjaXMLF;
        }
        public string GetAuditTabela()
        {
            string dirAT = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string putanjaDoAuditTabele = @"Skladista\AuditTabela.csv";
            string putanjaXMLAT = Path.Combine(dirAT, putanjaDoAuditTabele);
            return putanjaXMLAT;
        }
    }
}
