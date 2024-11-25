﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class Customer : User
    {
        public double Balance { get; set; }
        public List<Product> PurchasedProducts { get; set; }
        public List<Product> FavouriteProducts { get; set; }

        public Customer(string name, string email, double balance) : base(name,email)
        {
            this.Balance = balance;
        }
    }
}
