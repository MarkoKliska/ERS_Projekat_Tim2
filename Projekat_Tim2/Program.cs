using Projekat_Tim2.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("Odaberite operaciju: ");
            Console.WriteLine(" 1. Uvoz podataka \n 2. Ispis podataka \n 3. Evidentiranje geografskih podrucja \n 4. Kraj\n");

            string odabranaOpcija;
                
            odabranaOpcija = Console.ReadLine();
            //Console.WriteLine(odabranaOpcija);
            try
            {
                int proveriOpciju = int.Parse(odabranaOpcija);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            int selekcija = int.Parse(odabranaOpcija);

            if(selekcija < 1 || selekcija > 4)
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

        private static void UvozPodataka()
        {
            Console.WriteLine("Odaberite jednu od opcija: \n 1. Prognozirana potrosnja \n 2. Ostvarena potrosnja \n ");
            string selekcijaUP;

            selekcijaUP = Console.ReadLine();
            
            try
            {
                int proveriOpcijuUP = int.Parse(selekcijaUP);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            int selekcija_UP = int.Parse(selekcijaUP);

            if (selekcija_UP < 1 || selekcija_UP > 2)
            {
                Console.WriteLine("\nNeispravan unos, pokusajte ponovo\n");
                UvozPodataka();
            }

            switch (selekcija_UP)
            {
                case 1:
                    UvozPP uvozpp1 = new UvozPP();
                    break;

                case 2:
                    UvozOP uvozop1 = new UvozOP();
                    break;
            }
        }

        private static void IspisPodataka()
        {

        }

        private static void EvidentiranjePodataka()
        {

        }

        private static void Kraj()
        {
            Console.WriteLine("\nExiting...\n");
            Environment.Exit(0);
        }
    }
}
