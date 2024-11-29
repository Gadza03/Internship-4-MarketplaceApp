using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories;
using MarktePlace.Data.Models;
namespace MarketPlace.Presentation.Menus
{
    
    public class MainMenu
    {
       
       public void DisplayMainMenu()
        {
            var marketplace = new Marketplace();
            var userRepository = new UserRepository(marketplace);
            var productRepository = new ProductRepository(marketplace);
            var signInMenu = new SignIn(userRepository, productRepository);
            var logInMenu = new LogIn(userRepository, productRepository);            
            var customerMenu = new CustomerMenu(userRepository, productRepository);
            var sellerMenu = new SellerMenu(userRepository, productRepository);
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
                        else if (user is Seller seller)
                            sellerMenu.SellerMenuDisplay(seller);
                        break;
                    case "3":               
                        
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
