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
                Console.WriteLine("Račun pravite da bi bili\n1. Kupac\n2. Prodavač");
                var choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "1":
                        CustomerSignIn();
                        break;
                    case "2":
                        SellerSignIn();
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
            SellerSignIn();
            Console.WriteLine("Unesite početni balans: ");
            var balance = Console.ReadLine();
        }
        private static void SellerSignIn()
        {
            Console.Write("Unesite ime: ");
            var name = Console.ReadLine();
            Console.Write("Unesite mail: ");
            var mail = Console.ReadLine();
        }
    }
}
