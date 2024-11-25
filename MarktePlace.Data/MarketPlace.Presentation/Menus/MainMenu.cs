using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Presentation.Menus
{
    public static class MainMenu
    {
       public static void DisplayMainMenu()
        {
            while (true)
            {
                
                Console.WriteLine("Dobrodošli na Marketpalce...\n1. Registracija korisnika\n2. Prijava korisnika\n0. Izlaz iz aplikacije");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        SignIn.ChooseCustomerOrSeller();
                        break;
                    case "2.":
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos, pokušajte ponovno.");
                        Console.ReadKey();
                        continue;                        
                }

            }
        }
    }
}
