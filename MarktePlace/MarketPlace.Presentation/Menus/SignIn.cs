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
    public class SignIn
    {
        private readonly UserRepository _userRepository;
        public SignIn(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void ChooseCustomerOrSeller()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Račun ćete koristiti kao\n\n1. Kupac\n2. Prodavač\n0. Izlaz");
                var choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "1":
                        var customer = RegisterUser("kupac");
                        _userRepository.AddUser(customer);
                        Console.WriteLine("Uspješno dodan korsnik, kupac!");
                        break;
                    case "2":
                        var seller = RegisterUser("prodavač");
                        _userRepository.AddUser(seller);
                        Console.WriteLine("Uspješno dodan korsnik, prodavač!");
                        break;
                    case "0":
                        Console.WriteLine("Povratak nazad.");
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos, pokušajte ponovno.");
                        Console.ReadKey();
                        continue;
                }
                Console.ReadKey();
            }
        }

        private User RegisterUser(string prompt)
        {
            var name = "";
            var mail = "";
            var balance = "";
            while (true)
            {
                Console.Clear();
                Console.Write($"Registrirate se kao {prompt}...\n\nUnesite ime: ");
                name = Console.ReadLine().ToLower().Trim();

                var nameValidation = _userRepository.GetValidUserName(name);
                if (nameValidation != ResponseResultType.Success)
                {
                    Console.WriteLine($"Greška: {GetErrorMessage(nameValidation)}");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Clear();
                Console.Write("Unesite mail: ");
                mail = Console.ReadLine().ToLower().Trim();

                var mailValidation = _userRepository.GetValidUserMail(mail);
                if (mailValidation != ResponseResultType.Success)
                {
                    Console.WriteLine($"Greška: {GetErrorMessage(mailValidation)}");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
            if (prompt.ToLower() == "kupac")
            {                
                while (true)
                {
                    Console.Clear();
                    Console.Write("Unesite početni balans ($): ");
                    balance = Console.ReadLine();

                    var balanceValidation = _userRepository.GetValidUserBalance(balance);
                    if (balanceValidation != ResponseResultType.Success)
                    {
                        Console.WriteLine($"Greška: {GetErrorMessage(balanceValidation)}");
                        Console.ReadKey();
                        continue;
                    }
                    break;
                }
            }
            if (prompt.ToLower() == "kupac")            
                return new Customer(name, mail, double.Parse(balance));                  
            else            
                return new Seller(name,mail);       
        }      
        private string GetErrorMessage(ResponseResultType resultType)
        {
            switch (resultType)
            {
                case ResponseResultType.BlankInput:
                    return "Polje ne može biti prazno.";
                case ResponseResultType.AlreadyExists:
                    return "Korisničko ime ili email već postoji.";
                case ResponseResultType.InvalidFormat:
                    return "Neispravan format.";
                case ResponseResultType.InvalidValue:
                    return "Vrijednost mora biti pozitivna.";
                case ResponseResultType.Success:
                    return "Uspješno!";
                default:
                    return "Nepoznata greška.";
            }
        }
    }
}
