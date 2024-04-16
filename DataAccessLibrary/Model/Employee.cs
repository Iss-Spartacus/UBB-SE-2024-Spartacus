using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Power { get; set; }
        public int Money { get; set; }
        public string PhotoFilePath { get; set; }
        public bool ReadyToFight { get; set; }
        public int AccountId { get; set; } // Foreign key reference
        public Account Account { get; set; } // Navigation property

        public Employee(string fullName, int power, int money, string photoFilePath, bool readyToFight, int accountId) {
            FullName = fullName;
            Power = power;
            Money = money;
            PhotoFilePath = photoFilePath;
            ReadyToFight = readyToFight;
            AccountId = accountId;
        }
    }
}
