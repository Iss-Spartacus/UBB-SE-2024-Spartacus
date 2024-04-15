using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class MatchObserver
    {
        public int UserId { get; set; } // Foreign key reference
        public Users User { get; set; } // Navigation property
        public int MatchId { get; set; } // Foreign key reference
        public Matches Match { get; set; } // Navigation property

        public MatchObserver(int userId, int matchId)
        {
            UserId = userId;
            MatchId = matchId;
        }
    }
}
