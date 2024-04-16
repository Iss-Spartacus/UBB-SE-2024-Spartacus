using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int AccountId { get; set; } // Foreign key reference
        public Account Account { get; set; } // Navigation property

        public User(string userName, int accountId)
        {          
            UserName = userName;
            AccountId = accountId;
        }
    }
}
