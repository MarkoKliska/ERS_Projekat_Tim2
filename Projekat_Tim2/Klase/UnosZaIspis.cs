using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekat_Tim2.Klase
{
    internal class UnosZaIspis
    {
        public int Godina {  get; set; }
        public int Mesec { get; set; }
        public int Dan { get; set; }

        public string UnetaOblast { get; set; }

        private string dateString;

        private int godinaInt;
        private int mesecInt;
        private int danInt;

        private string godinaStr;
        private string mesecStr;
        private string danStr;

        private string unetaOblast;

        private int g, m, d;

        private DateTime datum;
        private string trazeniDatum;

        private bool postojiOblastPP = false;
        private bool postojiOblastOP = false;
        public static bool dozvolaZaIspis = false;

        public UnosZaIspis()
        {
            
        }

        static bool ProveriFormatDatuma(string unos, out DateTime datum)
        {
            string formatDatuma = "dd.MM.yyyy.";

            if (DateTime.TryParseExact(unos, formatDatuma, null, System.Globalization.DateTimeStyles.None, out datum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UnosZaIspis UnesiInformacije()
        {
            string[] deloviDatuma = null;
            UnosZaIspis instance = new UnosZaIspis();
            ProveraFormataUlaznogFajla provera = new ProveraFormataUlaznogFajla();
            Ispis ispis = new Ispis();

            Console.WriteLine("Unesite datum: ");
            dateString = Console.ReadLine();
            
            
            while ((ProveriFormatDatuma(dateString, out DateTime datum)) != true)
            {
                Console.WriteLine("\nNeispravan format datuma ili ste uneli nepostojeći datum.\nDatum mora biti u formatu dd.mm.yyyy.");
                Console.WriteLine("Ponovo unesite datum:");
                dateString = Console.ReadLine();
            }

            deloviDatuma = dateString.Split('.');

            godinaStr = deloviDatuma[2];
            mesecStr = deloviDatuma[1];
            danStr = deloviDatuma[0];

            try
            {
                datum = DateTime.ParseExact(dateString, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                trazeniDatum = datum.ToString("yyyy_MM_dd");

                try
                {
                    PutanjeDoSkladista putanja = new PutanjeDoSkladista();
                    string putanjaSF = putanja.GetSkladisteFajlova();
                    XmlDocument skladisteF = new XmlDocument();
                    skladisteF.Load(putanjaSF);

                    string progIme = $"prog_{trazeniDatum}";
                    string ostvIme = $"ostv_{trazeniDatum}";

                    bool progPostoji = false;
                    bool ostvPostoji = false;

                    XmlNodeList fajlovi = skladisteF.SelectNodes("//FAJL");

                    foreach (XmlNode fajl in fajlovi)
                    {
                        string ime = fajl.InnerText;

                        string[] deloviImena = ime.Split('_');
                        if (deloviImena.Length == 4)
                        {
                            string godina = deloviImena[1];
                            string mesec = deloviImena[2];
                            string dan = deloviImena[3];

                            string konvertovaniDatum = $"{godina}_{mesec}_{dan}";

                            if (konvertovaniDatum == trazeniDatum)
                            {
                                if (deloviImena[0] == "prog")
                                    progPostoji = true;
                                else if (deloviImena[0] == "ostv")
                                    ostvPostoji = true;
                            }
                        }
                    }

                    if (progPostoji == true && ostvPostoji == true)
                    {
                        Console.WriteLine("Unesite šifru oblasti:");
                        unetaOblast = Console.ReadLine();
                        unetaOblast = unetaOblast.ToUpper();

                        PutanjeDoSkladista putanjaPP = new PutanjeDoSkladista();
                        PutanjeDoSkladista putanjaOP = new PutanjeDoSkladista();
                        string putanjaSPP = putanjaPP.GetSkladistePP();
                        string putanjaSOP = putanjaOP.GetSkladisteOP();
                        XmlDocument xmlPP = new XmlDocument();
                        XmlDocument xmlOP = new XmlDocument();
                        xmlPP.Load(putanjaSPP);
                        xmlOP.Load(putanjaSOP); 

                        XmlNodeList stavkePP = xmlPP.SelectNodes("//STAVKA");
                        XmlNodeList stavkeOP = xmlOP.SelectNodes("//STAVKA");


                        foreach (XmlNode stavkaPP in stavkePP)
                        {
                            XmlNode oblastPPNode = stavkaPP.SelectSingleNode("OBLAST");
                            if (unetaOblast == oblastPPNode.InnerText)
                            {
                                postojiOblastPP = true;
                                break;
                            }
                        }
                                
                        foreach (XmlNode stavkaOP in stavkeOP)
                        {
                            XmlNode oblastOPNode = stavkaOP.SelectSingleNode("OBLAST");
                            if (unetaOblast == oblastOPNode.InnerText)
                            {
                                postojiOblastOP = true;
                                break;
                            }

                        }
                        if (postojiOblastPP == true && postojiOblastOP == true)
                        {
                            dozvolaZaIspis = true;
                        }
                        else if (postojiOblastPP == false && postojiOblastOP == true)
                        {
                            Console.WriteLine("\nZa datu oblast ne postoje podaci o prognoziranoj potrošnji. Uvezite podatke u bazu kako biste ih ispisali.");
                        }
                        else if (postojiOblastPP == true && postojiOblastOP == false)
                        {
                            Console.WriteLine("\nZa datu oblast ne postoje podaci o ostvarenoj potrošnji. Uvezite podatke u bazu kako biste ih ispisali.");
                        }
                        else
                        {
                            Console.WriteLine("\nZa datu oblast ne postoje podaci ni o prognoziranoj ni o ostvarenoj potrošnji.\nUvezite podatke u bazu kako biste ih ispisali.");
                        }

                    }
                    else if (progPostoji == false && ostvPostoji == true)
                    {
                        Console.WriteLine("\nZa dati datum ne postoje podaci o prognoziranoj potrošnji.\nUvezite podatke u bazu kako biste ih ispisali.");
                    }
                    else if (progPostoji == true && ostvPostoji == false)
                    {
                        Console.WriteLine("\nZa dati datum ne postoje podaci o ostvarenoj potrošnji.\nUvezite podatke u bazu kako biste ih ispisali.");
                    }
                    else
                    {
                        Console.WriteLine("\nZa dati datum ne postoje podaci ni o prognoziranoj ni o ostvarenoj potrošnji.\nUvezite podatke u bazu kako biste ih ispisali.");
                    }   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nGreška prilikom čitanja XML fajla: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nNeispravan format datuma, datum mora biti u formatu dd.mm.yyyy.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            godinaInt = Convert.ToInt32(godinaStr);
            mesecInt = Convert.ToInt32(mesecStr);
            danInt = Convert.ToInt32(danStr);

            instance.Dan = danInt;
            instance.Mesec = mesecInt;
            instance.Godina = godinaInt;
            instance.UnetaOblast = unetaOblast;
            
            
            return instance;
        }
    }
}
