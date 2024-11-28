using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktePlace.Data.Models;
namespace MarketPlace.Domain.Repositories
{
    public class ProductRepository
    {
        private readonly Marketplace _marketPlace = new Marketplace();       

        public string ViewProductsForSale()
        {
            var productsForSale = _marketPlace.AllProducts.Where(product=>product.Status == ProductStatus.ForSale).ToList();

            if (productsForSale.Count == 0)
                return "Trenutno nema proizvoda koji se prodaju.";

            var displayProducts = "Pregled svih proizvoda koji su na prodaju: \n";
            foreach (var product in productsForSale)
            {
                displayProducts += $"\nID: {product.Id}\nIme: {product.Name} Cijena: {product.Price}$  Kategorija: {product.Category} Opis: {product.Description}" +
                                   $" Prodavac: {product.Seller.Name} Rating: {product.Rating}\n";
            }
            return displayProducts;
        }
        public Product FindProductByName(string name)
        {
            return _marketPlace.AllProducts.FirstOrDefault(prod => prod.Name.ToLower() == name);
        }   

        public Product FindProductById(string id)
        {
            return _marketPlace.AllProducts.FirstOrDefault(prod => prod.Id.ToString() == id);
        }



    }
} 
  
