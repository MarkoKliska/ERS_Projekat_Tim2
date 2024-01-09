using Projekat_Tim2.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_Tim2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Pocetak();
            }
        }

        private static void Pocetak()
        {
            while (true)
            {
                Console.WriteLine("~~~~~~~~~~~~~~ Odaberite operaciju: ~~~~~~~~~~~~~~");
                Console.WriteLine("  1. Uvoz podataka \n  2. Ispis podataka \n  3. Evidentiranje geografskih podrucja \n  4. Kraj");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                string odabranaOpcija;
                int selekcija;

                odabranaOpcija = Console.ReadLine();

                try
                {
                    selekcija = int.Parse(odabranaOpcija);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Neispravan unos. Molimo Vas da unesete broj.");
                    continue;
                }

                if (selekcija < 1 || selekcija > 4)
                {
                    Console.WriteLine("\nNeispravan unos, pokusajte ponovo\n");
                }

                switch (selekcija)
                {
                    case 1:
                        UvozPodataka();
                        break;

                    case 2:
                        IspisPodataka();
                        break;

                    case 3:
                        EvidentiranjePodataka();
                        break;

                    case 4:
                        Kraj();
                        break;

                    //default nam ne treba jer smo proverili da li selekcija moze da bude nesto sto nije ponudjeno
                }
            }
        }

        private static void UvozPodataka()
        {
            while (true)
            {
                Console.WriteLine("~~~~~~~~~~~ Odaberite jednu od opcija: ~~~~~~~~~~~");
                Console.WriteLine("  1. Prognozirana potrosnja\n  2. Ostvarena potrosnja\n  3. <-");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                string selekcijaUP;
                int selekcija_UP;

                selekcijaUP = Console.ReadLine();

                try
                {
                    selekcija_UP = int.Parse(selekcijaUP);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Neispravan unos. Molimo Vas da unesete broj.");
                    continue;
                }



                if (selekcija_UP < 1 || selekcija_UP > 3)
                {
                    Console.WriteLine("\nNeispravan unos, pokusajte ponovo\n");
                    UvozPodataka();
                }

                switch (selekcija_UP)
                {
                    case 1:
                        UvozPP uvozpp1 = new UvozPP();
                        uvozpp1.UveziXML_PP();
                        break;

                    case 2:
                        UvozOP uvozop1 = new UvozOP();
                        uvozop1.UveziXML_OP();
                        break;

                    case 3:
                        return;
                    default:
                        break;
                }
            }
        }

        private static void IspisPodataka()
        {
            while (true)
            {
                Console.WriteLine("~~~~~~~~~~~ Odaberite jednu od opcija: ~~~~~~~~~~~");
                Console.WriteLine("  1. Ispis podataka\n  2. Izvoz tabele sa relativnim odstupanjima\n  3. <-");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                string selekcijaISP;
                int selekcija_ISP;

                selekcijaISP = Console.ReadLine();

                try
                {
                    selekcija_ISP = int.Parse(selekcijaISP);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Neispravan unos. Molimo Vas da unesete broj.");
                    continue;
                }


                if (selekcija_ISP < 1 || selekcija_ISP > 3)
                {
                    Console.WriteLine("\nNeispravan unos, pokusajte ponovo\n");
                    UvozPodataka();
                }

                switch (selekcija_ISP)
                {
                    case 1:
                        Ispis ispis = new Ispis();
                        ispis.IspisLogika();
                        break;

                    case 2:

                        TabelaRO izvoz = new TabelaRO();
                        izvoz.IzveziUCsv();
                        break;
                    case 3:
                        return;
                    default:
                        break;
                }
            }
        }

        private static void EvidentiranjePodataka()
        {
            while (true)
            {
                Console.WriteLine("~~~~~~~~~~~ Odaberite jednu od opcija: ~~~~~~~~~~~");
                Console.WriteLine("  1. Prikaz imena oblasti\n  2. Izmena imena oblasti\n  3. <-");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                string opcijas;
                int opcijai;

                opcijas = Console.ReadLine();

                try
                {
                    opcijai = int.Parse(opcijas);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Neispravan unos. Molimo Vas da unesete broj.");
                    continue;
                }


                if (opcijai < 1 || opcijai > 3)
                {
                    Console.WriteLine("\nNeispravan unos, pokusajte ponovo\n");
                    UvozPodataka();
                }
                switch (opcijai)
                {
                    case 1:
                        EvidencijaGP evidencija1 = new EvidencijaGP();
                        evidencija1.EvidentirajIspis();
                        break;
                    case 2:

                        EvidencijaGP evidencija2 = new EvidencijaGP();
                        evidencija2.IzmeniImeOblasti();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Neispravan unos");
                        break;
                }
            }
        }

        private static void Kraj()
        {
            Console.WriteLine("\nExiting...\n");
            Environment.Exit(0);
        }
    }
}
