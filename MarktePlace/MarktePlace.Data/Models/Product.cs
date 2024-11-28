using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ProductStatus Status { get; set; }
        public Seller Seller { get; set; }
        public ProductCategory Category { get; set; }
        public List<int> Rating { get; set; }
        public Product(string name, string description,double price, ProductStatus status, Seller seller, ProductCategory category)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Status = status;
            this.Seller = seller;
            this.Category = category;
           
        }
    }
}
