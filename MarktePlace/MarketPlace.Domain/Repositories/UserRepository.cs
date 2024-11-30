using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktePlace.Data.Models;
using MarketPlace.Domain.Repositories.Enums;
namespace MarketPlace.Domain.Repositories
{
    public class UserRepository
    {
        private readonly Marketplace _marketPlace;
        public UserRepository(Marketplace marketplace)
        {
            _marketPlace = marketplace;
        }
      
               
        public User FindUserByNameAndMail(string mail, string name)
        {
            return _marketPlace.AllUsers.FirstOrDefault(user => user.Mail.ToLower() == mail && user.Name.ToLower() == name);
        }        
        public ResponseResultType GetValidUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ResponseResultType.BlankInput;

            var findName = _marketPlace.AllUsers.FirstOrDefault(user => user.Name.ToLower() == name);
            if (findName != null)
                return ResponseResultType.AlreadyExists;            
            
            return ResponseResultType.Success;
        }
        public ResponseResultType GetValidUserMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                return ResponseResultType.BlankInput;

            var findMail = _marketPlace.AllUsers.FirstOrDefault(user => user.Mail.ToLower() == mail);
            if (findMail != null)
                return ResponseResultType.AlreadyExists;            

            if (!IsValidEmail(mail))
                return ResponseResultType.InvalidFormat;

            return ResponseResultType.Success;
        }
        private bool IsValidEmail(string email)
        {            
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
        }
        public ResponseResultType GetValidDouble(string balance)
        {

            if (string.IsNullOrWhiteSpace(balance))
                return ResponseResultType.BlankInput;

            if (!double.TryParse(balance, out double balanceDouble))
                return ResponseResultType.InvalidFormat;

            if (balanceDouble < 0)
                return ResponseResultType.InvalidValue;

            return ResponseResultType.Success;
        }
        public void AddCustomerToList(string name, string email, double balance)
        {
            _marketPlace.AllUsers.Add(new Customer(name,email,balance));
        }
        public void AddSellerToList(string name, string email)
        {
            _marketPlace.AllUsers.Add(new Seller(name, email));
        }
        private void AddCommissionToMarketplace(double price)
        {
            double commission = price * 0.05;
            _marketPlace.TotalTransactionFee += commission;           
        }
        public bool PurchaseProduct(Customer customer, Product product)
        {
            if (customer.Balance >= product.Price)
            {
                customer.Balance -= product.Price;
                _marketPlace.AllTransactions.Add(new Transaction(customer, product.Seller, product, DateTime.Now, TransacitonType.Purchase));   
                
                customer.PurchasedProducts.Add(product);                    
                product.Status = ProductStatus.Sold;
                product.Seller.SoldProducts.Add(product);
                product.Seller.ProductsForSale.Remove(product);

                AddCommissionToMarketplace(product.Price);
                return true;
            }
            else
                return false;
        }
        public void ReturnPorduct(Customer customer, Product product)
        {
            customer.Balance += product.Price * 0.80;

            var purchaseTransaction = _marketPlace.AllTransactions.FirstOrDefault(t => t.Product == product &&t.Customer == customer 
                                                                        && t.Product.Seller == product.Seller);
            _marketPlace.AllTransactions.Remove(purchaseTransaction);
            var returnTransaction = new Transaction(customer,product.Seller,product,DateTime.Now, TransacitonType.Return);
            _marketPlace.AllTransactions.Add(returnTransaction);

            product.Seller.SoldProducts.Remove(product);
            product.Seller.ReturnedProducts.Add(product);
            product.Seller.ProductsForSale.Add(product);

            product.Status = ProductStatus.ForSale;           
        }

        public void AddProductToFavourites(Customer customer, Product product)
        {
            customer.FavouriteProducts.Add(product);
        }
        public string PrintFavouriteProducts(Customer customer )
        {
            if (customer.FavouriteProducts.Count == 0)
                return "Korisnik nema favourite proizvoda.";
            var favouriteProducts = "Popis omiljenih proizvoda: \n";
            foreach (var product in customer.FavouriteProducts)
            {
                favouriteProducts += $"\nNaziv: {product.Name} Cijena: {product.Price}$ Kategorija: {product.Category} Status: {product.Status}";
            }
            return favouriteProducts;
        }
        
        public string PrintPurchasedProducts(Customer customer)
        {
            if (customer.PurchasedProducts.Count == 0)            
                return "Korisnik nema kupljenih proizvoda.";
            
            var purchasedProductsDisplay = "Popis svih kupljenih proizvoda\n";
            foreach (var product in customer.PurchasedProducts)
            {
                purchasedProductsDisplay += $"\nID: {product.Id}\nNaziv: {product.Name}\t Cijena: {product.Price}$\t Kategorija: {product.Category}\n";
            }
            return purchasedProductsDisplay;
        }
        public bool IsProductInFavourites(Customer customer, string id)
        {
            if (customer.FavouriteProducts.Any(p => p.Id.ToString() == id))
                return true;
            return false;            
        }

        public void AddProductForSale(Seller seller, string name, string description, double price, string category)
        {
            ProductCategory parsedCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), category, true);
            var newProduct = new Product(name, description, price, seller, parsedCategory);
            _marketPlace.AllProducts.Add(newProduct);
            seller.ProductsForSale.Add(newProduct);
        }
        public string GetAllSellersProducts(Seller seller)
        {
            var output = "Pregeled svih proizvoda: ";
            var productList = new List<Product>();
            productList.AddRange(seller.ProductsForSale);
            productList.AddRange(seller.SoldProducts);
            if (productList.Count == 0)
                return "Korisnik nema proizvoda.";            
            foreach (var product in productList)
            {
                output += $"\nID: {product.Id}\nNaziv: {product.Name} Cijena: {product.Price}$  Kategorija: {product.Category} Opis: {product.Description} Status: {product.Status}\n";
            }
            return output;
        }
        public double GetSellersIncome(Seller seller)
        {
            double income = 0;          
            foreach (var product in seller.SoldProducts)
            {
                double commission = product.Price * 0.05;
                income += product.Price - commission;
            }
            return income;
        }

        public double GetEarningsForPeriod(Seller seller, DateTime startDate, DateTime endDate)
        {           
            var filteredTransactions = _marketPlace.AllTransactions
                .Where(t => t.Seller == seller && t.DateTimeOfTransaction>= startDate && t.DateTimeOfTransaction <= endDate
                        && t.TransacitonType == TransacitonType.Purchase).ToList();
          
            double totalEarnings = filteredTransactions.Sum(t => t.Product.Price - (t.Product.Price*0.05));

            return totalEarnings;
        }

        public bool IsValidDateStart(DateTime date)
        {
            return date <= DateTime.Now;
        }
        public bool IsValidDateEnd(DateTime dateStart, DateTime dateEnd)
        {
            return dateEnd > dateStart;
        }
    }
}
