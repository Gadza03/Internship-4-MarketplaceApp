using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktePlace.Data.Models;
using MarketPlace.Domain.Repositories;
using MarketPlace.Domain.Repositories.Enums;
namespace MarketPlace.Domain.Repositories
{
    public class ProductRepository
    {
        private readonly Marketplace _marketPlace;
        public ProductRepository(Marketplace marketplace)
        {
            _marketPlace = marketplace;
        }

        public string ViewProductsForSale()
        {
            var productsForSale = _marketPlace.AllProducts.Where(product=>product.Status == ProductStatus.ForSale).ToList();

            if (productsForSale.Count == 0)
                return "Trenutno nema proizvoda koji se prodaju.";

            var displayProducts = "Pregled svih proizvoda koji su na prodaju: \n";
            foreach (var product in productsForSale)
            {
                displayProducts += $"\nID: {product.Id}\nIme: {product.Name} Cijena: {product.Price}$  Kategorija: {product.Category} Opis: {product.Description}" +
                                   $" Prodavac: {product.Seller.Name}\n";
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
        public Product FindProductByIdForEdit(string id, Seller seller)
        {
            return seller.ProductsForSale.FirstOrDefault(prod => prod.Id.ToString() == id);

        }
        public ResponseResultType GetValidProductInfo(string input)
        {
            if (string.IsNullOrEmpty(input))
                return ResponseResultType.BlankInput;
            return ResponseResultType.Success;
        }
        public ResponseResultType GetValidProductCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return ResponseResultType.BlankInput;
            if (!Enum.TryParse(category, true, out ProductCategory result))
                return ResponseResultType.InvalidFormat;
            return ResponseResultType.Success;
        }
        public List<Product> GetProductByCategory(string category, Seller seller)
        {
            ProductCategory parsedCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), category, true);
            return seller.SoldProducts.Where(prod => prod.Category == parsedCategory).ToList();

        }
        public string PrintSellersAllForSaleProducts(Seller seller)
        {
            var output = "Popis svih proizvoda:\n\n";
         
            foreach (var product in seller.ProductsForSale)
            {
                output+=$"ID - {product.Id}\nNaziv: {product.Name} Cijena: {product.Price}$\n";
            }
            return output;
        }
       
    }
} 
  
