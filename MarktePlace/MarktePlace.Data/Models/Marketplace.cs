using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MarktePlace.Data.Models
{
    public class Marketplace
    {
        public List<User> AllUsers { get; private set; }
        public List<Product> AllProducts { get; private set; }
        public List<Transaction> AllTransactions { get; private set; }
        public List<PromoCode> AllPromoCodes { get; private set; }
        public double TotalTransactionFee { get;  set; }
        public Marketplace()
        {
            var (users, products, transactions, promoCodes) = Seed.GetSeedData();
            this.AllUsers = users;
            this.AllProducts = products;
            this.AllTransactions = transactions;
            this.AllPromoCodes = promoCodes;
        }

        

        

    }
}
