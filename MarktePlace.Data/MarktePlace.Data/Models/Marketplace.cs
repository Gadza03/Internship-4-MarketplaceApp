using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class Marketplace
    {
        public List<User> AllUsers { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<Transaction> AllTransactions { get; set; }
        protected List<PromoCode> AllPromoCodes { get; set; }
        public Marketplace()
        {
            var (users, products, transactions) = Seed.GetSeedData();
            this.AllUsers = users;
            this.AllProducts = products;
            this.AllTransactions = transactions;
        }

        

    }
}
