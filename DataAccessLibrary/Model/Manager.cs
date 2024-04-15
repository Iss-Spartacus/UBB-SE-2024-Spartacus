using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; } // Foreign key reference
        public Account Account { get; set; } // Navigation property
        public Dictionary<string, int> TakenBribery { get; set; } = new Dictionary<string, int>();

    }
    public Manager(int id, string name, int accountId)
    {
        Id = id;
        Name = name;
        AccountId = accountId;
    }

    public void AddBribery(string employeeName, int amount)
    {
        if (!TakenBribery.ContainsKey(employeeName))
        {
            TakenBribery.Add(employeeName, amount);
        }
        else
        {
            // If the employee already exists in the dictionary, update the bribery amount
            TakenBribery[employeeName] += amount;
        }
    }

    // Method to remove bribery information from the dictionary
    public void RemoveBribery(string employeeName)
    {
        if (TakenBribery.ContainsKey(employeeName))
        {
            TakenBribery.Remove(employeeName);
        }
    }
}
