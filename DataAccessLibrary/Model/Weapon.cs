using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public bool Availability { get; set; }

        public Weapon(string name, int power, string type, int price, bool availability)
        {
            Name = name;
            Power = power;
            Type = type;
            Price = price;
            Availability = availability;
        }
    }
}
