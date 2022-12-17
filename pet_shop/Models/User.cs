using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pet_shop.Models
{
    public class User
    {
        public User()
        { }
        public User(string login, string password, string name, string surname, string role)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.role = role;
        }

        public User(int id, string login, string password, string name, string surname, string role)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.role = role;
        }

        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string role { get; set; }// admin vendor customer

    }
}
