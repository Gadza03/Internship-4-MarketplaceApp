using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Repositories;
using MarketPlace.Domain.Repositories.Enums;
using MarktePlace.Data.Models;

namespace MarketPlace.Presentation.Menus
{
    public class LogIn
    {
        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;
        public LogIn(UserRepository userRepository, ProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }
        public User LogInUser()
        {
            User foundedUser;                     
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Prijavite se...\n");
                Console.Write("Unesite ime: ");
                var name = Console.ReadLine().Trim().ToLower();
                Console.Write("Unesite email: ");
                var mail = Console.ReadLine().Trim().ToLower();
                
                var user = _userRepository.FindUserByNameAndMail(mail, name);
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
