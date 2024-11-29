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
        public Product Product { get; set; }       
        public DateTime DateTimeOfTransaction  { get; }

        public Transaction(Customer customer, Seller seller, Product productId)
        {
            this.Id = Guid.NewGuid();
            this.Customer = customer;
            this.Seller = seller;
            this.Product = productId;
            this.DateTimeOfTransaction = DateTime.Now;
        }
        
    }
}
