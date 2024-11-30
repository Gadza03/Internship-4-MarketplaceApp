using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories;
using MarktePlace.Data.Models;
using MarketPlace.Domain.Repositories.Enums;
namespace MarketPlace.Presentation.Menus
{
    public class CustomerMenu
    {
        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;

        public CustomerMenu(UserRepository userRepository, ProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public void CustomerMenuDisplay(Customer customer)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine($"Dobrodošli na Marketplace {customer.Name}\n\n1. Filtiranje proizvoda po kategorijama (marketplace)\n2. Kupi proizvode\n3. Povijest kupovine" +
                    "\n4. Dodaj omiljeni proizvodi u favourites\n5. Omiljeni proizvodi\n6. Varti proizvod\n0. Izlaz");
                Console.Write("Izaberi opciju: ");
                var choice = Console.ReadLine();


                Console.Clear();
                switch (choice)
                {                    
                    case "1":                        
                        PrintAllProductByCategory();
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
        private int EnterPromoCode(ProductCategory category)
        {
            PromoCode validPromocode;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Unesite kupon kod (Enter - ako nemate kod): ");
                var promoCode = Console.ReadLine();
                if (promoCode == "")
                    return 0;
                validPromocode = _productRepository.FindPromoCode(category, promoCode);
                if (validPromocode is null)
                {
                    Console.WriteLine("Uneseni promo kod ne postoji ili se ne koristi za ovu kategoriju.");
                    Console.ReadKey();
                    continue;
                }
                break;   
            }
            Console.WriteLine($"Uspješno je primjenjen popust od {validPromocode.DiscountPercentage}%");
            return validPromocode.DiscountPercentage;
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
                if (product.Status == ProductStatus.Sold)
                {
                    Console.WriteLine("Uneseni proizvod je raspordan.");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine();
                break;

            }
            var discount = EnterPromoCode(product.Category);
            if (discount == 0)
                return product;
            var oldPrice = product.Price;
            var productWithDiscount = _productRepository.CalculateDiscount(product, discount);
           
            Console.WriteLine($"\nNova cijena: {productWithDiscount.Price}$ - Stara cijena: {oldPrice}$\n");
            return productWithDiscount;

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
        private  void PrintAllCategories()
        {
            Console.WriteLine("Sve kategorije proizvoda:\n");
            foreach (var category in Enum.GetValues(typeof(ProductCategory)))
            {
                Console.WriteLine($"- {category}");
            }
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
        private void PrintAllProductByCategory()
        {
            PrintAllCategories();
            var category = EnterValidCategory();
            var productList = _productRepository.GetProductsByCategory(category);
            Console.Clear();
            Console.WriteLine($"\nPregled svih proizvoda sa kategorijom {category}\n");
            foreach (var product in productList)
            {
                Console.WriteLine($"\nID: {product.Id}\n\t - Ime: {product.Name} Cijena: {product.Price}$  Prodavač: {product.Seller.Name}\n " +
                    $"\t - Kategorija: {product.Category} Opis: {product.Description} Status: {product.Status}");
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
