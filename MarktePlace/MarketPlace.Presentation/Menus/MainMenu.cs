using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories;
using MarktePlace.Data.Models;
namespace MarketPlace.Presentation.Menus
{
    
    public static class MainMenu
    {
       public static void DisplayMainMenu()
        {
            Marketplace marketPlace = new Marketplace();             

            UserRepository userRepository = new UserRepository(marketPlace);

            SignIn signInMenu = new SignIn(userRepository);
            LogIn logInMenu = new LogIn(userRepository);

            ProductRepository productRepository = new ProductRepository(marketPlace);
            CustomerMenu customerMenu = new CustomerMenu(productRepository, userRepository);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Dobrodošli na Marketpalce...\n1. Registracija korisnika\n2. Prijava korisnika\n0. Izlaz iz aplikacije");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        signInMenu.ChooseCustomerOrSeller();
                        break;
                    case "2":
                        var user = logInMenu.LogInUser();
                        if (user is Customer customer)
                            customerMenu.CustomerMenuDisplay(customer);
                        //else if (user is Seller)
                        break;
                    case "3":
                        Console.WriteLine(userRepository.GetAllUsers()); 
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos, pokušajte ponovno.");
                        Console.ReadKey();
                        continue;                        
                }
                Console.ReadKey();

            }
        }
        
    }
}
