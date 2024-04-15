﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Arena
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

        public Arena(int id, int capacity, string location)
        {
            Id = id;
            Capacity = capacity;
            Location = location;
        }
    }
}
