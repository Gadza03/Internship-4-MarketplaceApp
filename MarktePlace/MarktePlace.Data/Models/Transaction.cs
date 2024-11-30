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
        public TransacitonType TransacitonType { get; set; }

        public Transaction(Customer customer, Seller seller, Product productId, DateTime timeOfTransaction, TransacitonType transacitonType)
        {
            this.Id = Guid.NewGuid();
            this.Customer = customer;
            this.Seller = seller;
            this.Product = productId;
            this.DateTimeOfTransaction =timeOfTransaction;
            this.TransacitonType = transacitonType;
        }
        
    }
}
