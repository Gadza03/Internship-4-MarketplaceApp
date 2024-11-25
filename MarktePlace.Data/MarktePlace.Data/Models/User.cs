using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktePlace.Data.Models
{
    public class User
    {
        public int Id { get; } = 1;
        public string Name { get; set; }
        public string Mail { get; set; }

        public User(string name, string email)
        {            
            this.Id = this.Id++;
            this.Name = name;      
            this.Mail = email;
        }

    }
}
