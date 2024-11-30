using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class PromoCode
    {
        public string Code { get; private set; } 
        public ProductCategory Category { get; private set; } 
        public int DiscountPercentage { get; private set; } 
        public DateTime ExpiryDate { get; private set; } 

        public PromoCode(string code, ProductCategory category, int discountPercentage, DateTime expiryDate)
        {
            Code = code;
            Category = category;
            DiscountPercentage = discountPercentage;
            ExpiryDate = expiryDate;
        }
    }
}
