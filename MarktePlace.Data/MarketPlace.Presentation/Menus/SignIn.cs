using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Presentation.Menus
{
    public static class SignIn
    {
        public static void ChooseCustomerOrSeller()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Račun ćete koristiti kao\n\n1. Kupac\n2. Prodavač");
                var choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "1":
                        CustomerSignIn();
                        break;
                    case "2":
                        SellerSignIn("prodavač");
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos, pokušajte ponovno.");
                        Console.ReadKey();
                        continue;
                }
            }
        }
        private static void CustomerSignIn()
        {
            SellerSignIn("kupac");
            Console.Write("Unesite početni balans ($): ");
            var balance = Console.ReadLine();
        }
        private static void SellerSignIn(string prompt)
        {
            Console.Clear();
            Console.Write($"Registrirate se kao {prompt}...\n\nUnesite ime: ");
            var name = Console.ReadLine();
            Console.Write("Unesite mail: ");
            var mail = Console.ReadLine();
        }
    }
}
