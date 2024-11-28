using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories;
using MarketPlace.Domain.Repositories.Enum;
using MarktePlace.Data.Models;

namespace MarketPlace.Presentation.Menus
{
    public class LogIn
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly ProductRepository _productRepository = new ProductRepository();

        public User LogInUser()
        {
            User foundedUser;                     
            while (true)
            {
                Console.Clear();
                //Console.WriteLine("Prijavite se...\n");
                //Console.Write("Unesite ime: ");
                //var name = Console.ReadLine().Trim().ToLower();
                //Console.Write("Unesite email: ");
                //var mail = Console.ReadLine().Trim().ToLower();
                var name = "John doe";
                var mail = "john@example.com";
                var user = _userRepository.FindUserByNameAndMail(mail.ToLower(), name.ToLower());
                if (user is null)
                {
                    Console.WriteLine("Netočno korsiničko ime ili email, pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
                }
                foundedUser = user;
                break;
            }          
            return foundedUser;

        }

        
    }
}
