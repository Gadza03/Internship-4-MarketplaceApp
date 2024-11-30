﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktePlace.Data.Models;
namespace MarktePlace.Data
{
    public static class Seed
    {
        public static (List<User> Users, List<Product> Products, List<Transaction> Transactions, List<PromoCode> PromoCodes) GetSeedData()
        {             
            
            var customer1 = new Customer("John Doe", "john@example.com", 100);
            var customer2 = new Customer("Alice Green", "alice@example.com", 200);
            var seller1 = new Seller("Jane Smith", "jane@example.com");
            var seller2 = new Seller("Bob Brown", "bob@example.com");          
            var users = new List<User> { customer1, customer2, seller1, seller2 }; 
            
            var product1 = new Product("Laptop", "Gaming laptop", 500, ProductStatus.ForSale, seller1, ProductCategory.Electronics);            
            var product2 = new Product("Book", "C# Programming Guide", 50, ProductStatus.Sold, seller1, ProductCategory.Books);
            seller1.ProductsForSale.Add(product1);            
            var product3 = new Product("Headphones", "Wireless headphones", 25, ProductStatus.ForSale, seller2, ProductCategory.Electronics);
            var product4 = new Product("Mobile", "Mobile phone", 150, ProductStatus.Sold, seller2, ProductCategory.Electronics);
            seller2.ProductsForSale.Add(product3);

            var products = new List<Product> { product1, product2, product3 };

            var transaction1 = new Transaction(customer1, seller1, product2, DateTime.Parse("1-20-2024"), TransacitonType.Purchase);
            seller1.SoldProducts.Add(product2);
            customer1.Balance -= product2.Price;
            customer1.PurchasedProducts.Add(product2);
            
            var transaction2 = new Transaction(customer2, seller2, product4, DateTime.Parse("3-12-2024"), TransacitonType.Purchase);
            seller2.SoldProducts.Add(product4);
            customer2.Balance -= product4.Price;
            customer2.PurchasedProducts.Add(product4);

            var transactions = new List<Transaction> { transaction1, transaction2 };

            var promoCodes = new List<PromoCode>
            {
            new PromoCode("ELECTRO10", ProductCategory.Electronics, 10, DateTime.Now.AddDays(30)),
            new PromoCode("BOOKS20", ProductCategory.Books, 20, DateTime.Now.AddDays(15))
             };
            return (users, products, transactions, promoCodes);
        }
    }
}
