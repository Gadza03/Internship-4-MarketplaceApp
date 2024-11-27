using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarktePlace.Data.Models;
using MarketPlace.Domain.Repositories.Enum;

namespace MarketPlace.Domain.Repositories
{
    public class UserRepository
    {
        private readonly Marketplace _marketPlace = new Marketplace();        
      
        public string GetAllUsers()
        {
            var displayUsers = "";
            foreach (var user in _marketPlace.AllUsers)
            {
                displayUsers += $"\n{user.Id} {user.Name} {user.Mail}";
            }
            return displayUsers;
        }              
        public User FindUserByNameAndMail(string mail, string name)
        {
            return _marketPlace.AllUsers.FirstOrDefault(user => user.Mail.ToLower() == mail && user.Name.ToLower() == name);
        }
        public ResponseResultType GetValidUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ResponseResultType.BlankInput;

            var findName = _marketPlace.AllUsers.FirstOrDefault(user => user.Name.ToLower() == name);
            if (findName != null)
                return ResponseResultType.AlreadyExists;            
            
            return ResponseResultType.Success;
        }
        public ResponseResultType GetValidUserMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                return ResponseResultType.BlankInput;

            var findMail = _marketPlace.AllUsers.FirstOrDefault(user => user.Mail.ToLower() == mail);
            if (findMail != null)
                return ResponseResultType.AlreadyExists;            

            if (!IsValidEmail(mail))
                return ResponseResultType.InvalidFormat;

            return ResponseResultType.Success;
        }
        private bool IsValidEmail(string email)
        {            
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
        }
        public ResponseResultType GetValidUserBalance(string balance)
        {

            if (string.IsNullOrWhiteSpace(balance))
                return ResponseResultType.BlankInput;

            if (!double.TryParse(balance, out double balanceDouble))
                return ResponseResultType.InvalidFormat;

            if (balanceDouble < 0)
                return ResponseResultType.InvalidValue;

            return ResponseResultType.Success;
        }
        public void AddUser(User user)
        {
            _marketPlace.AllUsers.Add(user);
        }
        public bool PurchaseProduct(Customer customer, Product product)
        {
            if (customer.Balance >= product.Price)
            {
                customer.Balance -= product.Price;
                _marketPlace.AllTransactions.Add(new Transaction(customer, product.Seller, product));
                customer.PurchasedProducts.Add(product);

                product.Amount -= 1;
                if (product.Amount == 0)
                    product.Status = ProductStatus.Sold;

                return true;
            }
            else
                return false;

        }
    }
}
