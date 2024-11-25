using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktePlace.Data.Models;
namespace MarktePlace.Data
{
    public static class Seed
    {
        public static (List<User> Users, List<Product> Products, List<Transaction> Transactions) GetSeedData()
        {             
            var customer1 = new Customer("Joh Doe", "john@example.com", 100);
            var customer2 = new Customer("Alice Green", "alice@example.com", 200);
            var seller1 = new Seller("Jane Smith", "jane@example.com");
            var seller2 = new Seller("Bob Brown", "bob@example.com");          
            var users = new List<User> { customer1, customer2, seller1, seller2 }; 
            
            var product1 = new Product("Laptop", "Gaming laptop", 1000, ProductStatus.ForSale, seller1, ProductCategory.Electronics);
            var product2 = new Product("Book", "C# Programming Guide", 50, ProductStatus.ForSale, seller1, ProductCategory.Books);
            var product3 = new Product("Headphones", "Wireless headphones", 150, ProductStatus.ForSale, seller2, ProductCategory.Electronics);
            var products = new List<Product> { product1, product2, product3 };

            var transaction1 = new Transaction(customer1, seller1, product2, 10);
            var transaction2 = new Transaction(customer2, seller2, product3, 2);
            var transactions = new List<Transaction> { transaction1, transaction2 };

            return (users, products, transactions);
        }
    }
}
