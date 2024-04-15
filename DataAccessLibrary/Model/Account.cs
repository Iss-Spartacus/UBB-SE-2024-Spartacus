using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Account
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Username { get; set; }
        public String Password { get; private set; }
        public bool IsAdult { get; set; }


        public Account(string email, string username, string password, bool isAdult)
        {
            Email = email;
            Username = username;
            Password = password;
            IsAdult = isAdult;
        }
    }
}
