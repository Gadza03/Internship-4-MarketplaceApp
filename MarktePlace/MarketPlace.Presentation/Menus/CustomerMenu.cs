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
                Console.WriteLine($"Dobrodošli na Marketplace {customer.Name}\n\n1. Pregled proizvoda\n2. Kupi proizvode\n3. Povijest kupovine" +
                    "\n4. Dodaj omiljeni proizvodi u favourites\n5. Omiljeni proizvodi\n6. Varti proizvod\n0. Izlaz");
                Console.Write("Izaberi opciju: ");
                var choice = Console.ReadLine();


                Console.Clear();
                switch (choice)
                {                    
                    case "1":                        
                        Console.WriteLine(_productRepository.ViewProductsForSale());
                        break;
                    case "2":                                              
                        var productToPurchase = ChooseProductToPurchase(customer);
                        if (productToPurchase == null)
                            break;                        
                        var haveEnoughMoney = _userRepository.PurchaseProduct(customer, productToPurchase);
                        PrintIsTransactionApproved(haveEnoughMoney);                            
                        break;
                    case "3":                        
                        Console.WriteLine(_userRepository.PrintPurchasedProducts(customer));
                        break;
                    case "4":
                        var productToFavourites = ChooseProductToFavourites(customer);
                        if (productToFavourites == null)
                            break;
                        _userRepository.AddProductToFavourites(customer, productToFavourites);
                        Console.WriteLine("Uspješno dodan proizvod u favourites!");
                        break;
                    case "5":
                        Console.WriteLine(_userRepository.PrintFavouriteProducts(customer));
                        break;
                    case "6":
                        var productToReturn = ChooseProductToReturn(customer);
                        if (productToReturn == null)
                            break;
                        _userRepository.ReturnPorduct(customer, productToReturn);
                        Console.WriteLine("Uspješno ste vratili proizvod!");
                        break;
                    case "0":
                        Console.WriteLine("Izlaz...");
                        return;
                    default:
                        Console.WriteLine("Neispravan unos. Pokušajte ponovno.");
                        Console.ReadKey();
                        continue; 
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
                Console.WriteLine($"Vaš trenutni balance je: {customer.Balance}$\n");
                Console.WriteLine("Nabavka: \n"+ _productRepository.ViewProductsForSale());
                Console.Write("\nUnesite ID proizvoda kojeg želite kupiti (Copy-Paste) (Enter - korak nazad): ");
                var productId = Console.ReadLine().ToLower().Trim();
                if (productId == "")                
                    return null;
                
                product = _productRepository.FindProductById(productId);
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
        private Product ChooseProductToReturn(Customer customer)
        {
            Product product;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_userRepository.PrintPurchasedProducts(customer));               
                Console.Write("\nUnesite ID proizvoda kojeg želite vratiti (Copy-Paste) (Enter - korak nazad): ");                
                var productId = Console.ReadLine().Trim();
                if (productId == "")
                    return null;
                product = _productRepository.FindProductById(productId);
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
        private Product ChooseProductToFavourites(Customer customer)
        {
            Product product;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_productRepository.ViewProductsForSale());
                Console.Write("\nUnesite ID proizvoda kojeg želite dodati u favourites (Copy-Paste) (Enter - korak nazad): ");
                var productId = Console.ReadLine().Trim();
                if (productId == "")
                    return null;
                product = _productRepository.FindProductById(productId);
                if (product is null)
                {
                    Console.WriteLine("Uneseni proizvod ne postoji, pokušajte ponovno!");
                    Console.ReadKey();
                    continue;
                }
                if (_userRepository.IsProductInFavourites(customer,productId))
                {
                    Console.WriteLine("Uneseni proizvod je vec na listi omiljenih.");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            return product;


        }
    }
}
