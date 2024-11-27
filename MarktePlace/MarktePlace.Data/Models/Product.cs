﻿using System;
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
        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;              
                Status = _amount == 0 ? ProductStatus.Sold : ProductStatus.ForSale;
            }
        }
        public Product(string name, string description,double price, ProductStatus status, Seller seller, ProductCategory category, int amount)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Status = status;
            this.Seller = seller;
            this.Category = category;
            this.Amount = amount;
        }
    }
}