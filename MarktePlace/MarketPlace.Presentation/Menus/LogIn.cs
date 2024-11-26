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
        private readonly UserRepository _userRepository;
        public LogIn(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User LogInUser()
        {
            User foundedUser;
            var name = "";
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Prijavite se...\n");
                Console.Write("Unesite ime: ");
                name = Console.ReadLine().Trim().ToLower();
                var user = _userRepository.FindUserByName(name);
                if (user is null)
                {
                    Console.WriteLine("Netočno korsiničko ime, pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Clear();                
                Console.Write("Unesite email: ");
                var mail = Console.ReadLine().Trim().ToLower();
                var user = _userRepository.FindUserByNameAndMail(mail, name);
                if (user is null)
                {
                    Console.WriteLine("Netočan email, pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine($"Uspješna prijava...\n\nDobrodošao {user.Name}");
                foundedUser = user;
                break;
            }
            return foundedUser;

        }

        
    }
}
