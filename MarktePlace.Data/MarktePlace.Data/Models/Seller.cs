using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class Seller : User
    {
        public List<Product> ProductsForSale { get; set; }
        
        public Seller(string name, string email) : base(name,  email)
        {

        }
    }
}
