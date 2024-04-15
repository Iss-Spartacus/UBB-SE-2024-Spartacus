using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Betting
    {
        public int Id { get; set; }
        public int AccountId { get; set; } // Foreign key reference
        public Account Account { get; set; } // Navigation property
        public float Amount { get; set; }
        public int BetOnId { get; set; } // Foreign key reference
        public Employee BetOn { get; set; } // Navigation property
        public int InitialOdd { get; set; }
        public int MatchId { get; set; } // Foreign key reference
        public Matches Match { get; set; } // Navigation property

        public Betting(int id, int accountId, float amount, int betOnId, int initialOdd, int matchId)
        {
            Id = id;
            AccountId = accountId;
            Amount = amount;
            BetOnId = betOnId;
            InitialOdd = initialOdd;
            MatchId = matchId;
        }
    }
}
