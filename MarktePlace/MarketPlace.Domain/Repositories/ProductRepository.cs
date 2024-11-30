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

            var displayProducts = "";
            foreach (var product in productsForSale)
            {
                displayProducts += $"\nID: {product.Id}\n\t- Ime: {product.Name} Cijena: {product.Price}$  Prodavač: {product.Seller.Name} \n\t" +
                    $"- Kategorija: {product.Category} Opis: {product.Description} Status: {product.Status}\n";
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
        public List<Product> GetSellersProductByCategory(string category, Seller seller)
        {
            ProductCategory parsedCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), category, true);
            return seller.SoldProducts.Where(prod => prod.Category == parsedCategory).ToList();
        }
        public List<Product> GetProductsByCategory(string category)
        {
            ProductCategory parsedCategory = (ProductCategory)Enum.Parse(typeof(ProductCategory), category, true);
            return _marketPlace.AllProducts.Where(prod => prod.Category == parsedCategory).ToList();
        }
      
        public string PrintSellersAllForSaleProducts(Seller seller)
        {
            var output = "Popis svih proizvoda:\n\n";
         
            foreach (var product in seller.ProductsForSale)
            {
                output+=$"\nID - {product.Id}\nNaziv: {product.Name} Cijena: {product.Price}$\n";
            }
            return output;
        }
       
        public PromoCode FindPromoCode(ProductCategory category, string promoCode)
        {
            return _marketPlace.AllPromoCodes.FirstOrDefault(code => code.Category == category && code.Code == promoCode.ToUpper());
        }
        public Product CalculateDiscount(Product product, double discount)
        {
            product.Price -= (product.Price * (discount / 100));
            return product;
        }
    }
} 
  
