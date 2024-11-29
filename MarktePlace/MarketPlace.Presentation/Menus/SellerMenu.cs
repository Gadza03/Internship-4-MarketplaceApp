using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories.Enum;
using MarketPlace.Domain.Repositories;
using MarktePlace.Data.Models;

namespace MarketPlace.Presentation.Menus
{
    public class SellerMenu
    {
        
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly ProductRepository _productRepository = new ProductRepository();

        public void SellerMenuDisplay(Seller seller)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine($"Dobrodošli na Marketplace {seller.Name}\n\n1. Dodaj proizvod\n2. Pregled proizvoda\n3. Promjeni cijenu proizvoda" +
                    "\n4. Pregled zarade\n0. Izlaz");
                Console.Write("Izaberi opciju: ");
                var choice = Console.ReadLine();


                Console.Clear();
                switch (choice)
                {
                    case "1":                       
                        break;
                    case "2":                       
                        break;
                    case "3":                        
                        break;
                    case "4":                        
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
        private string EnterProductInfo(string prompt)
        {
            string input = "";

            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {prompt} proizvoda: ");
                input = Console.ReadLine().ToLower().Trim();
                var inputValidation = _productRepository.GetValidProductInfo(input);
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"Greška: {GetErrorMessage(inputValidation)}");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            return input;
        }
        private void PrintAllCategories()
        {
            Console.WriteLine("Sve kategorije proizvoda:\n");
            foreach (var category in Enum.GetValues(typeof(ProductCategory)))
            {
                Console.WriteLine($"- {category}");
            }
        }
        private void AddProduct()
        {
            string name = EnterProductInfo("naziv");
            string opis = EnterProductInfo("opis");
            string price = "";
            string category = "";
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite cijenu proizvoda: ");
                price = Console.ReadLine().ToLower().Trim();
                var priceValidation = _userRepository.GetValidDouble(price);
                if (priceValidation != ResponseResultType.Success)
                {
                    Console.WriteLine($"Greška: {GetErrorMessage(priceValidation)}");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Clear();

                Console.Write($"Unesite kategoriju proizvoda: ");
                category = Console.ReadLine().ToLower().Trim();
                var categoryValidation = _productRepository.GetValidProductCatgory(category);
                if (priceValidation != ResponseResultType.Success)
                {
                    Console.WriteLine($"Greška: {GetErrorMessage(priceValidation)}");
                    Console.ReadKey();
                    continue;
                }
                break;
            }

        }


        private string GetErrorMessage(ResponseResultType resultType)
        {
            switch (resultType)
            {
                case ResponseResultType.BlankInput:
                    return "Polje ne može biti prazno.";
                case ResponseResultType.AlreadyExists:
                    return "Korisničko ime ili email već postoji.";
                case ResponseResultType.InvalidFormat:
                    return "Neispravan format.";
                case ResponseResultType.InvalidValue:
                    return "Vrijednost mora biti pozitivna.";
                case ResponseResultType.Success:
                    return "Uspješno!";
                default:
                    return "Nepoznata greška.";
            }
        }


    }
}
