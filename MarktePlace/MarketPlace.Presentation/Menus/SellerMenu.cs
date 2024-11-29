using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories;
using MarketPlace.Domain.Repositories.Enums;
using MarktePlace.Data.Models;

namespace MarketPlace.Presentation.Menus
{
    public class SellerMenu
    {

        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;
        public SellerMenu(UserRepository userRepository, ProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public void SellerMenuDisplay(Seller seller)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine($"Dobrodošli na Marketplace {seller.Name}\n\n1. Dodaj proizvod\n2. Pregled proizvoda\n3. Pregled ukupne zarade" +
                    "\n4. Mijenjanje cijene\n5. Pregled prodanih po kategoriji\n6. Zarade u određenom vremenskom razdoblju\n0. Izlaz");
                Console.Write("Izaberi opciju: ");
                var choice = Console.ReadLine();


                Console.Clear();
                switch (choice)
                {
                    case "1":
                        AddProduct(seller);
                        break;
                    case "2":
                        Console.WriteLine(_userRepository.GetAllSellersProducts(seller)); 
                        break;
                    case "3":
                        PrintIncome(seller);
                        break;
                    case "4":
                        ChooseProductToEditPrice(seller);
                        break;
                    case "5":
                        PrintSoldProductByCategory(seller);
                        break;
                    case "6":
                        PrintEarningsForPeriod(seller);
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
        
        private void PrintEarningsForPeriod(Seller seller)
        {
            DateTime startDate;
            DateTime endDate;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Unesite početni datum: ");
                string startDateInput = Console.ReadLine();
                if (!DateTime.TryParse(startDateInput, out startDate) || !_userRepository.IsValidDateStart(startDate))
                {
                    Console.WriteLine("Pogrešan unos datuma, pokusajte ponovno!");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Unesite zavrsni datum: ");
                string startDateInput = Console.ReadLine();
                if (!DateTime.TryParse(startDateInput, out endDate) || !_userRepository.IsValidDateEnd(startDate, endDate))
                {
                    Console.WriteLine($"Pogrešan unos datuma (pazite da ne bude veci od pocetnog {startDate.ToShortDateString()}), pokusajte ponovno!");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            double earnings = _userRepository.GetEarningsForPeriod(seller, startDate, endDate);
            Console.WriteLine($"Zarada od {startDate.ToShortDateString()} do {endDate.ToShortDateString()} iznosi: {earnings}$");


        }        
        private void ChooseProductToEditPrice(Seller seller)
        {
           
            Product product;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_productRepository.PrintSellersAllForSaleProducts(seller));
                Console.Write("\nUnesite ID proizvoda kojeg želite vratiti (Copy-Paste) (Enter - korak nazad): ");
                var productId = Console.ReadLine().Trim();
                if (productId == "")
                    return;                
                product = _productRepository.FindProductByIdForEdit(productId, seller);
                if (product is null)
                {
                    Console.WriteLine("Uneseni proizvod ne postoji, pokušajte ponovno!");
                    Console.ReadKey();
                    continue;
                }

                break;
            }
            var price = "";
            while (true)
            {

                Console.Clear();
                Console.Write($"Stara cijena: {product.Price}$\n\nUnesite novu cijenu: ");                       
                price = Console.ReadLine().Trim();
                var priceValidation = _userRepository.GetValidDouble(price);
                if (priceValidation != ResponseResultType.Success)
                {
                    Console.WriteLine($"Greška: {GetErrorMessage(priceValidation)}");
                    Console.ReadKey();
                    continue;
                }
               
                break;
            }
            product.Price = double.Parse(price);
            Console.WriteLine("Uspjesno promjenjena cijena proizvoda.");

        }
        private void PrintSoldProductByCategory(Seller seller)
        {
            PrintAllCategories();
            var category = EnterValidCategory();
            var productList = _productRepository.GetProductByCategory(category, seller);
            Console.WriteLine($"Pregled svih proizvoda sa kategorijom {category}\n\n");
            foreach (var product in productList)
            {
                Console.WriteLine($"\nID: {product.Id}\nIme: {product.Name} Cijena: {product.Price}$  Kategorija: {product.Category} Opis: {product.Description}");
            }



        }
        private void PrintIncome(Seller seller)
        {
            var income = _userRepository.GetSellersIncome(seller);
           
            Console.WriteLine($"Ukupna zarada od prodaje (uracunat je commission po transakciji):\n\n- {income}$");

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
        private string EnterValidCategory()
        {
            string category = "";
            while (true)
            {
                Console.Clear();
                PrintAllCategories();
                Console.Write($"Unesite kategoriju proizvoda: ");
                category = Console.ReadLine().ToLower().Trim();
                var categoryValidation = _productRepository.GetValidProductCategory(category);
                if (categoryValidation != ResponseResultType.Success)
                {
                    Console.WriteLine($"Greška: {GetErrorMessage(categoryValidation)}");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            return category;
        }
       
        private void PrintAllCategories()
        {
            Console.WriteLine("Sve kategorije proizvoda:\n");
            foreach (var category in Enum.GetValues(typeof(ProductCategory)))
            {
                Console.WriteLine($"- {category}");
            }
        }
        private void AddProduct(Seller seller)
        {
            var name = EnterProductInfo("naziv");
            var opis = EnterProductInfo("opis");
            var price = "";
            var category = EnterValidCategory();
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
            
           
            _userRepository.AddProductForSale(seller,name, opis, double.Parse(price),category);
            Console.WriteLine("Uspješno dodan novi proizvod.");
            
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
