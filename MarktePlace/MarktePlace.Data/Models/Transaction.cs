using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class Transaction 
    {
        public Guid Id { get; }
        public Customer Customer { get; set; }
        public Seller Seller { get; set; }
        public Product ProductId { get; set; }
        public int Amount { get; set; }
        DateTime DateTimeOfTransaction  { get; }

        public Transaction(Customer customer, Seller seller, Product productId)
        {
            this.Id = Guid.NewGuid();
            this.Customer = customer;
            this.Seller = seller;
            this.ProductId = productId;
            this.DateTimeOfTransaction = DateTime.Now;
        }
        
    }
}
