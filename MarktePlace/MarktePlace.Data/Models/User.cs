using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class User
    {
        private static int _idCounter = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Mail { get; set; }

        public User(string name, string email)
        {            
            this.Id = ++_idCounter;
            this.Name = name;      
            this.Mail = email;
        }

    }
}
