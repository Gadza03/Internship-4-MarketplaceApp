using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories;
using MarktePlace.Data.Models;
namespace MarketPlace.Presentation.Menus
{
    public class CustomerMenu
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly ProductRepository _productRepository = new ProductRepository();

        public void CustomerMenuDisplay(Customer customer)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("Dobrodošli na Marketplace\n\n1. Pregled proizvoda\n2. Kupi proizvode\n3. Povijest kupovine" +
                    "\n4. Omiljeni proizvodi\n5. Varti proizvod\n0. Izlaz");
                Console.Write("Izaberi opciju: ");
                var choice = Console.ReadLine();



                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Pregled svih proizvoda koji su na prodaju: \n\n" + _productRepository.ViewProductsForSale());
                        break;
                    case "2":
                        Console.Clear();                        
                        var product = ChooseProductToPurchase(customer);
                        var haveEnoughMoney = _userRepository.PurchaseProduct(customer, product);
                        PrintIsTransactionApproved(haveEnoughMoney);
                            
                        break;
                    case "3":

                        break;
                    case "4":

                        break;
                    case "5":

                        break;
                    case "0":
                        Console.WriteLine("Izlaz...");
                        return;
                    default:
                        Console.WriteLine("Neispravan unos. Pokušajte ponovno.");
                        Console.ReadKey();
                        continue; // Invalid input, go back to the main menu
                }
                Console.ReadKey();
            }
        }
        private void PrintIsTransactionApproved(bool haveEnoughMoney)
        {
            if (haveEnoughMoney)
                Console.WriteLine("Uspješno ste kupili proizvod!");
            else
                Console.WriteLine("Neuspjela kupovina proizvoda, nedovoljan iznos na računu!");
        }
        private Product ChooseProductToPurchase(Customer customer)
        {
            Product product;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Vaš trenutni balance je: {customer.Balance}\n");
                Console.WriteLine("Nabavka: \n"+ _productRepository.ViewProductsForSale());
                Console.Write("\nUnesite ime proizvoda kojeg želite kupiti: ");
                var productName = Console.ReadLine().ToLower().Trim();
                product = _productRepository.FindProductByName(productName);
                if (product is null)
                {
                    Console.WriteLine("Uneseni proizvod ne postoji, pokušajte ponovno!");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            return product;

        }
    }
}
